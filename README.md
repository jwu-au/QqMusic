# QqMusic
QqMusic songs synchronization for IOS using iFile web server. The project is built using netcoreapp2.0. It requires QqMusic v6.x and a web server (e.g. iFile) on app's document folder.

# Settings - appsettings.json
```json
  "Music": {
    "BasePath": "\\\\192.168.9.8\\media\\QqMusic",
    "BaseUrl": "http://192.168.9.243:10000/var/mobile/Applications/F16E8B42-BA8F-4E78-8CA4-E5C20C42EEEE/Documents"
  } 
```
* BasePath: the location where to store songs.
* BaseUrl: the remote url of QqMusic IOS app's document folder.

# Usage:
* Bootup the server using <b>dotnet QqMusic.dll</b>
* Hit url: <b>http://server.ip:5005/api/sync</b>
