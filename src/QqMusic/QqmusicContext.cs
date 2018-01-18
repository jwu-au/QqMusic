using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace QqMusic
{
    public partial class QqmusicContext : DbContext
    {
        public virtual DbSet<Songs> Songs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite($"data source='{Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "db.sqlite")}'");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Songs>(entity =>
            {
                entity.HasKey(e => new {e.Id, e.Type});

                entity.ToTable("SONGS");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("BIGINT")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Album)
                    .IsRequired()
                    .HasColumnName("album")
                    .HasDefaultValueSql("\"\"");

                entity.Property(e => e.Albumdesc)
                    .IsRequired()
                    .HasColumnName("albumdesc")
                    .HasDefaultValueSql("\"\"");

                entity.Property(e => e.Albumindex)
                    .IsRequired()
                    .HasColumnName("albumindex")
                    .HasDefaultValueSql("\"\"");

                entity.Property(e => e.AlertId)
                    .HasColumnName("AlertID")
                    .HasColumnType("INT UNSIGNED")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.DownloadFailedTimes).HasDefaultValueSql("0");

                entity.Property(e => e.Err)
                    .HasColumnName("err")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.File)
                    .IsRequired()
                    .HasColumnName("file")
                    .HasDefaultValueSql("\"\"");

                entity.Property(e => e.Filesize)
                    .HasColumnName("filesize")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Ipodurl)
                    .IsRequired()
                    .HasColumnName("ipodurl")
                    .HasDefaultValueSql("\"\"");

                entity.Property(e => e.KSongErrmsgid)
                    .HasColumnName("K_SONG_ERRMSGID")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.KSongExclusive)
                    .HasColumnName("K_SONG_EXCLUSIVE")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.KSongFlacsize)
                    .HasColumnName("K_SONG_FLACSIZE")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.KSongKsongNew)
                    .IsRequired()
                    .HasColumnName("K_SONG_KSONG_NEW")
                    .HasDefaultValueSql("\"\"");

                entity.Property(e => e.KSongReserve1)
                    .IsRequired()
                    .HasColumnName("K_SONG_RESERVE1")
                    .HasDefaultValueSql("\"\"");

                entity.Property(e => e.KSongReserve10)
                    .HasColumnName("K_SONG_RESERVE10")
                    .HasColumnType("INT UNSIGNED")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.KSongReserve11)
                    .HasColumnName("K_SONG_RESERVE11")
                    .HasColumnType("INT UNSIGNED")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.KSongReserve12)
                    .HasColumnName("K_SONG_RESERVE12")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.KSongReserve13)
                    .HasColumnName("K_SONG_RESERVE13")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.KSongReserve14)
                    .HasColumnName("K_SONG_RESERVE14")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.KSongReserve15)
                    .HasColumnName("K_SONG_RESERVE15")
                    .HasColumnType("INT UNSIGNED")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.KSongReserve16)
                    .HasColumnName("K_SONG_RESERVE16")
                    .HasColumnType("INT UNSIGNED")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.KSongReserve17)
                    .HasColumnName("K_SONG_RESERVE17")
                    .HasColumnType("BIGINT")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.KSongReserve18)
                    .HasColumnName("K_SONG_RESERVE18")
                    .HasColumnType("BIGINT")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.KSongReserve19)
                    .IsRequired()
                    .HasColumnName("K_SONG_RESERVE19")
                    .HasDefaultValueSql("\"\"");

                entity.Property(e => e.KSongReserve2)
                    .IsRequired()
                    .HasColumnName("K_SONG_RESERVE2")
                    .HasDefaultValueSql("\"\"");

                entity.Property(e => e.KSongReserve20)
                    .IsRequired()
                    .HasColumnName("K_SONG_RESERVE20")
                    .HasDefaultValueSql("\"\"");

                entity.Property(e => e.KSongReserve21)
                    .IsRequired()
                    .HasColumnName("K_SONG_RESERVE21")
                    .HasDefaultValueSql("\"\"");

                entity.Property(e => e.KSongReserve22)
                    .IsRequired()
                    .HasColumnName("K_SONG_RESERVE22")
                    .HasDefaultValueSql("\"\"");

                entity.Property(e => e.KSongReserve23)
                    .IsRequired()
                    .HasColumnName("K_SONG_RESERVE23")
                    .HasDefaultValueSql("\"\"");

                entity.Property(e => e.KSongReserve24)
                    .IsRequired()
                    .HasColumnName("K_SONG_RESERVE24")
                    .HasDefaultValueSql("\"\"");

                entity.Property(e => e.KSongReserve3)
                    .IsRequired()
                    .HasColumnName("K_SONG_RESERVE3")
                    .HasDefaultValueSql("\"\"");

                entity.Property(e => e.KSongReserve4)
                    .HasColumnName("K_SONG_RESERVE4")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.KSongReserve5)
                    .HasColumnName("K_SONG_RESERVE5")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.KSongReserve6)
                    .HasColumnName("K_SONG_RESERVE6")
                    .HasColumnType("INT UNSIGNED")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.KSongReserve7)
                    .IsRequired()
                    .HasColumnName("K_SONG_RESERVE7")
                    .HasDefaultValueSql("\"\"");

                entity.Property(e => e.KSongReserve8)
                    .IsRequired()
                    .HasColumnName("K_SONG_RESERVE8")
                    .HasDefaultValueSql("\"\"");

                entity.Property(e => e.KSongReserve9)
                    .IsRequired()
                    .HasColumnName("K_SONG_RESERVE9")
                    .HasDefaultValueSql("\"\"");

                entity.Property(e => e.LastPlayTime)
                    .HasColumnType("INT UNSIGNED")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Nameindex)
                    .IsRequired()
                    .HasColumnName("nameindex")
                    .HasDefaultValueSql("\"\"");

                entity.Property(e => e.OrgAlbumName)
                    .IsRequired()
                    .HasDefaultValueSql("\"\"");

                entity.Property(e => e.OrgName)
                    .IsRequired()
                    .HasDefaultValueSql("\"\"");

                entity.Property(e => e.OrgSingerName)
                    .IsRequired()
                    .HasDefaultValueSql("\"\"");

                entity.Property(e => e.PlayCount)
                    .HasColumnType("INT UNSIGNED")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Singer)
                    .IsRequired()
                    .HasColumnName("singer")
                    .HasDefaultValueSql("\"\"");

                entity.Property(e => e.Singerindex)
                    .IsRequired()
                    .HasColumnName("singerindex")
                    .HasDefaultValueSql("\"\"");

                entity.Property(e => e.Topsize)
                    .HasColumnName("topsize")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.TryBegin)
                    .HasColumnType("INT UNSIGNED")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.TryEnd)
                    .HasColumnType("INT UNSIGNED")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.TrySize)
                    .HasColumnType("INT UNSIGNED")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Wapdownloadurl)
                    .IsRequired()
                    .HasColumnName("wapdownloadurl")
                    .HasDefaultValueSql("\"\"");

                entity.Property(e => e.Wapliveurl)
                    .IsRequired()
                    .HasColumnName("wapliveurl")
                    .HasDefaultValueSql("\"\"");

                entity.Property(e => e.Wifiurl)
                    .IsRequired()
                    .HasColumnName("wifiurl")
                    .HasDefaultValueSql("\"\"");
            });
        }
    }
}