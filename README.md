# RDB
API-first RDB POC on .NET Core, Docker and MongoDB: http://rdbagents.northeurope.cloudapp.azure.com/api/values.

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

### Visual Studio
- Docker: `Ctrl+F5` in docker-compose project. *Debug is not working :(*
- Local: `Ctrl+F5` in selected project or `F5` for debugging.

### Visual Studio for Mac
- Docker: not supported yet. Do manual steps below.
- Local: run or debug selected project.

### Manual
- Docker: `docker-compose up -d --build` (in the solution root directory)
- Local: `dotnet restore` then `dotnet run` (in selected project directory)

Browse http://localhost:18420/api/values for API or http://localhost:18421 for admin after container or server is started.

## Work Guide
Full task journey from the backlog to production is described below.

### Ready for dev conditions
- Has concise description with exact specs
  - API: operations, fields, errors
  - Admin: screenshot or drawing, contents and behavior, link to required APIs
- Approved by somebody from the core team
- Moved to Todo Trello column

### Setup tasks
Setup tasks require investigation, communication and solution decisions. Here is the flow:
- Each task require at least three persons to be assigned
- When task group is ready - move to Doing column
- Learn, try, discuss together
- Align on selected tools and approaches
- Update the card with final decisions and more strict todos
- Continue with implementation

### Development guidelines
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

### Definition of Done
- Build and tests are green
- Code review\* completed
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
- Merged to master and deployed to Azure
- Moved to Done Trello column, nobody assigned

*\* (approves and code reviews can be done by any other competent teammate)*

### Deploy Guide
Continuous deployment is not configured yet. Follow steps below to deploy manually: 
1. Find SSH key in the solution root directory:
   - macOS/Linux: `azure.key`
   - Windows: `azure.ppk`
2. Create SSH tunnel:
   - macOS/Linux: `ssh -fNL 2375:localhost:2375 -p 2200 rdb@rdbmgmt.northeurope.cloudapp.azure.com -i azure.key`
   - Windows: use PuTTY with `azure.ppk` key, see [Azure docs](https://docs.microsoft.com/en-us/azure/container-service/container-service-connect#create-an-ssh-tunnel-on-windows)
3. Expose Azure SSH tunnel to Docker in the terminal:
   - macOS/Linux: `export DOCKER_HOST=:2375`
   - Windows: `$env:DOCKER_HOST=":2375"` in PowerShell or `set DOCKER_HOST=:2375` in cmd  
   - If connection was successful, `docker info` will now show Azure Swarm cluster information.
4. Deploy Docker cluster with Azure config:  
   `docker-compose -f docker-compose.yml -f docker-compose.azure.yml up -d --build`  
   If containers deployed successfully, `docker ps` will display running Azure Swarm cluster.

### DB Guide
- Data is stored in a local dockerized MongoDB instance. Data files are mapped to `data` directory in the solution root.
- Handling schema changes:
  - Any DB schema changes should not break existing code in `master` branch
  - Code should support both old and new schema versions until all documents are patched on production
  - Patch old documents to the new schema on writes
  - See more in [MongoDB .NET Driver docs](https://mongodb.github.io/mongo-csharp-driver/2.4/reference/bson/mapping/schema_changes/)
- Seeding data: skip this until we defined data import strategy from old-school RDB.