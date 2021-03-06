# RDB
API-first RDB POC on .NET Core, Docker and MongoDB: http://rdb.northeurope.cloudapp.azure.com.

- [Getting Started](#getting-started)
- [Work Guide](#work-guide)








## Getting Started
Software requirements:
- Windows 10, macOS or Linux
- [Docker](https://www.docker.com/community-edition#/download) ([Docker Compose](https://github.com/docker/compose/releases) is installed separately on Linux)

> Docker requires virtualization to be enabled in the BIOS. Steps are vendor specific, but typically the BIOS option is called `Virtualization Technology` and located in `Advanced CPU Configuration` section.

Use `run` command from the solution root directory to build and start the app.  
Use `run --production` to run in production mode.

Browse http://localhost:8080/api/ for API or http://localhost:8080 for admin after all containers are started.

### Development Setup
Install additional tools for development:
- [.NET Core SDK 2.0](https://www.microsoft.com/net/core): to work with nuget packages and add .NET Core support to IDEs and text editors
- [Node.js 8+/npm 5+](https://nodejs.org/): to work with npm packages or run node tools like eslint
- Text editor or IDE of choice:
  - [Visual Studio 2017 (`15.3+`)](https://www.visualstudio.com/vs/)
  - [Visual Studio for Mac (`7.2+`)](https://www.visualstudio.com/vs/visual-studio-mac/) *(Switch to [Alpha channel](https://docs.microsoft.com/en-us/visualstudio/mac/update) for Docker support, unstable)*
  - [Rider 2017.2+](https://www.jetbrains.com/rider/eap/) *(Docker debug not supported [yet](https://youtrack.jetbrains.com/issue/RIDER-6410))*
  - [Visual Studio Code](https://code.visualstudio.com/)
  - or whatever else you prefer

> Make sure [EditorConfig](http://editorconfig.org/#download) is installed in your IDE or text editor to follow code indent guidelines.

Recommended extensions for Visual Studio Code or other text editors:
- [C#](https://marketplace.visualstudio.com/items?itemName=ms-vscode.csharp): .NET Core development and debugging support for C#
- [EditorConfig](https://marketplace.visualstudio.com/items?itemName=EditorConfig.EditorConfig): configures the editor with rules from `.editorconfig` file
- [ESLint](https://marketplace.visualstudio.com/items?itemName=dbaeumer.vscode-eslint): integrated JavaScript linting
- [Vetur](https://marketplace.visualstudio.com/items?itemName=octref.vetur): Vue.js development support

#### Command-line

Use `run` command from the solution root directory to start the app and webpack watch.  
Use `build` command from the project directory to rebuild and restart a single container for the active project.

> :warning: Docker containers don't share npm and NuGet packages with the host machine. Remember to run `npm install` and `dotnet restore` commands in the active project directory to resolve all references in the text editor or IDE.

All client-side changes in `RDB.Admin/ClientApp` directory are watched by webpack and update the browser using hot modules replacement. Other changes including C# and cshtml files require container rebuild.

#### Visual Studio 2017
Set `docker-compose` as startup project and press `Ctrl+F5` to build and run the apps or `F5` to start debugging.

By default Visual Studio launches Docker Compose with the modified project name, so all containers and volumes will be namespaced to that name. To reuse container names and volumes from the command-line mode, put [fixed Microsoft.Docker.dll](https://drive.google.com/open?id=0B6zzxGJlEvj6N3hqMFZEME5zVjA) to these locations: (*Replace `Community` if you have another Visual Studio edition, eg `Professional`*)
- `C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\MSBuild\Sdks\Microsoft.Docker.Sdk\tools`
- `C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\Common7\IDE\Extensions\k3mvm330.k1s`

#### Visual Studio for Mac
Set `docker-compose` as startup project and press `Opt+Cmd+Return` to build and run the apps or `Cmd+Return` to start debugging.

> Docker debug is in alpha release now and unstable. Admin container debug is not working and we're waiting for the release version to fix `Microsoft.Docker.dll` for Mac. Switch to Visual Studio Code if something is not working until Visual Studio for Mac 7.2 is released.

#### Visual Studio Code
Press `Ctrl+Shift+B` to build and run the apps or `Ctrl+P`, type 'task', 'Space' and the command name to run in specific modes.

To debug the containers, go to Debug view (`Ctrl+Shift+D`), select target project and press `F5`. Select another project and press `F5` to add it to the current debug session.

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
- Docs:
  - Add XML documents for all API controller actions and models
  - Use only `<summary>`, `<param>` and `<remarks>` XML documentation elements
  - Mark all API controllers with `[Produces("application/json")]` and `[Consumes("application/json")]` attributes
  - Specify all action responses with `[ProducesResponseType(type, statusCode)]` attributes
  - Verify docs in Swagger UI
- Admin:
  - Mobile first
  - Based on current RDB look
  - Material design using [Vuetify](https://vuetifyjs.com/) components
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
  - Works in latest Chrome, Safari, Edge, Firefox, iPhone and Android
  - Works the same in production mode
  - ESLint is green
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
   - Windows: `$env:DOCKER_HOST=":2375"` and `$env:COMPOSE_CONVERT_WINDOWS_PATHS=1` in PowerShell  
     or `set DOCKER_HOST=:2375` and `set COMPOSE_CONVERT_WINDOWS_PATHS=1` in cmd
   - If connection was successful, `docker info` will now show Azure Swarm cluster information.
4. Deploy Docker cluster with Azure config using command in the solution root directory:  
   `./run --azure`  
   If containers deployed successfully, `docker ps` will display running Azure Swarm cluster.

### DB Guide
- Data is handled by local dockerized MongoDB instance. Data files are stored in Docker Compose `data` volume.
- Handling schema changes:
  - Any DB schema changes should not break existing code in `master` branch
  - Code should support both old and new schema versions until all documents are patched on production
  - Patch old documents to the new schema on writes
  - See more in [MongoDB .NET Driver docs](https://mongodb.github.io/mongo-csharp-driver/2.4/reference/bson/mapping/schema_changes/)
- Seeding data: skip this until we defined data import strategy from old-school RDB.
