# RDB
RDB POC on .NET Core, Docker and MongoDB.

## Getting Started
Software requirements:
- Windows 10, macOS or Linux
- [Docker](https://www.docker.com/community-edition#/download)
- [.NET Core 2.0](https://www.microsoft.com/net/core/preview)
- Text editor or IDE of choice:
  - [Visual Studio 2017 Preview version 15.3](https://www.visualstudio.com/vs/preview/)
  - [Visual Studio for Mac](https://www.visualstudio.com/vs/visual-studio-mac/) *(Switch to [Beta channel](https://developer.xamarin.com/recipes/cross-platform/ide/change_updates_channel/#visualstudiomac))*
  - [Visual Studio Code](https://code.visualstudio.com/)
  - or whatever else you prefer

Add hosts:
```
127.0.0.1	rdb.api
```

### Visual Studio
- Docker: `Ctrl+F5` in docker-compose project. *Debug is not working :(*
- Local: `Ctrl+F5` in selected project or `F5` for debugging.

### Visual Studio for Mac
- Docker: not supported yet. Do manual steps below.
- Local: run or debug selected project.

### Manual
- Docker: `docker-compose up -d --build` (in the solution root directory)
- Local: `dotnet restore` then `dotnet run` (in selected project directory)

Browse http://rdb.api/api/values after container or server is started.