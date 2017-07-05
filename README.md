# RDB
API-first RDB POC on .NET Core, Docker and MongoDB.

- [Getting Started](#getting-started)
- [Work Guide](#work-guide)





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

## Work Guide
The task is ready for dev:
- Has concise description with exact specs
  - API: operations, fields, errors
  - Admin: screenshot or drawing, contents and behavior, link to required APIs
- Approved by somebody from the team
- Moved to Todo Trello column

Setup tasks require investigation, communication and solution decisions. Here is the flow:
- Each task require at least three persons to be assigned
- When task group is ready - move to Doing column
- Learn, try, discuss together
- Align on selected tools and approaches
- Update the card with final decisions and more strict todos
- Continue with implementation

Task development guidelines:
- Move Trello card to Doing column, assign yourself
- Start a new git branch, eg `setup-db`, `api-recipe-crud` or `admin-account-selector`
- Make the code
- API:
  - Seed data in DB
  - Add tests
  - Add docs
  - Add client
- Admin:
  - Mobile first
  - Based on current RDB look
  - Make it nice
- Review the code and refactor
- Create a pull request

The task is done:
*(approves and code reviews can be done by any other teammate)*
- Build and tests are green
- Code review completed
- Setup:
  - Approved by all task group members
  - All setup or work requirements documented in readme
  - Works on Windows 10, macOS and Ubuntu
- API:
  - Tests approved
  - Docs approved
  - Client approved
  - Version bumped (semver)
- Admin:
  - UI and behavior approved
  - Works in Chrome, Safari, Firefox, Edge, iPhone and Android
- Merged to master and deployed to test
- Moved to Done Trello column, nobody assigned