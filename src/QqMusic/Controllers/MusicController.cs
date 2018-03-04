using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flurl.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebDav;

namespace QqMusic.Controllers
{
    [Route("api")]
    public class MusicController : Controller
    {
        private readonly MusicOptions _options;
        private readonly ILogger<MusicController> _logger;

        public MusicController(IOptions<MusicOptions> options, ILogger<MusicController> logger)
        {
            _options = options.Value;
            _logger = logger;
        }

        [HttpGet("renew")]
        public async Task<string> Renew()
        {
            if (!Directory.Exists(_options.DownloadBasePath)) Directory.CreateDirectory(_options.DownloadBasePath);
            var dbFile = Path.Combine(_options.DownloadBasePath, "db.sqlite");
            if (System.IO.File.Exists(dbFile)) System.IO.File.Delete(dbFile);

            var dbUrl = $"{_options.ServerUrl}/qqmusic.sqlite";
            using (var dbStream = await dbUrl.GetStreamAsync().ConfigureAwait(false))
            using (var fileStream = System.IO.File.Create(dbFile))
            {
                await dbStream.CopyToAsync(fileStream).ConfigureAwait(false);
                _logger.LogInformation("renewed db file {0}", dbFile);
            }

            // make a copy of db file to local file system for db context to read
            var localDbFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "db.sqlite");
            System.IO.File.Copy(dbFile, localDbFile, true);
            _logger.LogInformation("copied db file to {0}", localDbFile);

            return $"renewed db file in {dbFile}\r\n";
        }

        [HttpGet("download")]
        public async Task<string> Download()
        {
            var output = new StringBuilder();

            using (var dbContext = new QqmusicContext())
            {
                var exts = new[] {".tm0", ".tm3", ".flac"}; // allowed extensions
                var songs = dbContext.Songs
                    .Where(x => !string.IsNullOrEmpty(x.File))
                    .Where(x => exts.Contains(Path.GetExtension(x.File).ToLower()))
                    .ToList();

                if (songs.Any())
                {
                    var songsPath = Path.Combine(_options.DownloadBasePath, "songs");
                    if (!Directory.Exists(songsPath)) Directory.CreateDirectory(songsPath);
                }

                _logger.LogInformation("found {0} songs to downbload", songs.Count);
                foreach (var song in songs)
                {
                    var ext = Path.GetExtension(song.File);
                    if (ext.Contains("tm")) ext = ".mp3"; // mp3 extensions: tm0, tm3
                    var fileName = $"{song.Singer} - {song.Name}{ext}";
                    fileName = string.Join("_", fileName.Split(Path.GetInvalidFileNameChars()));
                    fileName = Path.Combine(_options.DownloadBasePath, _options.DownloadSongPath, fileName);
                    if (System.IO.File.Exists(fileName))
                    {
                        _logger.LogInformation("skipping {0}", fileName);
                        continue;
                    }

                    var fullUrl = $"{_options.ServerUrl}{song.File}";
                    _logger.LogInformation("downloading {0}", fullUrl);
                    using (var flurlClient = new FlurlClient(fullUrl))
                    {
                        var response = await flurlClient.AllowAnyHttpStatus().Request().GetAsync();
                        if (!response.IsSuccessStatusCode)
                        {
                            _logger.LogWarning("failed to download {0}", fullUrl);
                            continue;
                        }

                        using (var songStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                        {
                            // check header
                            var buffer = new byte[4];
                            await songStream.ReadAsync(buffer, 0, 4).ConfigureAwait(false);

                            if (buffer[0] == 0x37 && buffer[1] == 0x1d && buffer[2] == 0x2c && buffer[3] == 0x36)
                            {
                                // buffered flac file: fix to normal flac header "fLaC"
                                _logger.LogInformation("replacing header for {0}", fileName);
                                buffer = new byte[] {0x66, 0x4c, 0x61, 0x43};
                            }

                            using (var fileStream = System.IO.File.Create(fileName))
                            {
                                _logger.LogInformation("saving {0}", fileName);
                                await fileStream.WriteAsync(buffer, 0, 4).ConfigureAwait(false);
                                await songStream.CopyToAsync(fileStream).ConfigureAwait(false);

                                output.AppendLine("saved file: " + fileName);
                            }
                        }
                    }
                }
            }

            if (output.Length == 0)
            {
                output.AppendLine("downloaded none");
            }

            return output.ToString();
        }

        [HttpGet("upload")]
        public async Task<string> Upload()
        {
            var clientParams = new WebDavClientParams {BaseAddress = new Uri(_options.UploadBaseUrl)};
            using (var webDavClient = new WebDavClient(clientParams))
            {
                var songResponse = await webDavClient.Propfind(_options.UploadSongUrl).ConfigureAwait(false);
                if (!songResponse.IsSuccessful) return "upload propfind failed\r\n";

                var existingSongs = songResponse.Resources.Select(x => new {x.Uri, FileName = Path.GetFileName(Uri.UnescapeDataString(x.Uri)).Normalize()}).Where(x => !string.IsNullOrEmpty(x.FileName)).ToList();
                var downloadPath = Path.Combine(_options.DownloadBasePath, _options.DownloadSongPath);
                var downloadedSongs = Directory.GetFiles(downloadPath);

                var output = new StringBuilder();
                foreach (var downloadedSong in downloadedSongs)
                {
                    var songFileName = Path.GetFileName(downloadedSong);
                    if (existingSongs.Any(x => x.FileName == songFileName)) continue;

                    // upload
                    _logger.LogInformation("uploading {0}", songFileName);
                    output.AppendLine($"uploading {songFileName}");
                    using (var fileStream = System.IO.File.OpenRead(downloadedSong))
                    {
                        await webDavClient.PutFile($"{_options.UploadSongUrl}/{Uri.EscapeDataString(songFileName)}", fileStream).ConfigureAwait(false);
                    }
                }

                if (output.Length == 0)
                {
                    output.AppendLine("uploaded none");
                }

                return output.ToString();
            }
        }

        [HttpGet("sync")]
        public async Task<string> Sync()
        {
            var output = new StringBuilder();
            output.AppendLine(await Renew());
            output.AppendLine(await Download());
            output.AppendLine(await Upload());
            output.AppendLine("all synced at: " + DateTime.Now.ToString(CultureInfo.InvariantCulture));
            return output.ToString();
        }
    }
}