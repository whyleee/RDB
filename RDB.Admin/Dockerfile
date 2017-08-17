FROM microsoft/aspnetcore-build:2.0.0-preview2 AS builder
WORKDIR /source
COPY *.csproj .
RUN dotnet restore
COPY package.json package-lock.json ./
RUN npm install
COPY . .
RUN dotnet publish -c Release -o /app

FROM microsoft/aspnetcore:2.0.0-preview2
WORKDIR /app
COPY --from=builder /app .
ENTRYPOINT ["dotnet", "RDB.Admin.dll"]
