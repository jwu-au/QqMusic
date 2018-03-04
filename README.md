# QqMusic
QqMusic songs synchronization for IOS. It requires QqMusic <b>v6.x</b> and a web server (e.g. iFile) on app's document folder. It syncs all songs downloaded/cached in QqMusic app from IOS device into local hdd or network location (e.g. NAS via SMB protocol). It renames synced songs to <b>[singer] - [song].[extension]</b>.


# Settings - appsettings.json
```json
  "Music": {
    "ServerUrl": "http://192.168.9.139:10000/var/mobile/Applications/F16E8B42-BA8F-4E78-8CA4-E5C20C42EEEE/Documents", // QqMusic app url (via iFile)
    "DownloadBasePath": "\\\\192.168.9.8\\media\\QqMusic", // local base path for storing downloaded song db file
    "DownloadSongPath": "songs", // local path relative to DownloadBasePath for storing songs
    "UploadBaseUrl": "http://192.168.9.138:8000/", // upload base url (via WebDAV server such as nPlayer)
    "UploadSongUrl": "Downloads/songs" // upload url relative to UploadBaseUrl for storing songs
  }
```
* ServerUrl: QqMusic app url (via iFile)
* DownloadBasePath: local base path for storing downloaded song db file
* DownloadSongPath: local path relative to DownloadBasePath for storing songs
* UploadBaseUrl: upload base url (via WebDAV server such as nPlayer)
* UploadSongUrl: upload url relative to UploadBaseUrl for storing songs


# Usage:
* Bootup the server using <b>dotnet QqMusic.dll</b> (require .net core 2.0)
* Hit url: <b>http://server.ip:5005/api/sync</b>


# Install as windows service:
* Copy QqMusic.exe and QqMusic.xml to a folder
* Run: QqMusic.exe install: will install a windows service named QqMusic
* Run: QqMusic.exe start: to start the service
* For removing the service run: QqMusic.exe stop, and QqMusic.exe uninstall