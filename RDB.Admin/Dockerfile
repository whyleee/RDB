FROM microsoft/aspnetcore-build:2.0.0-preview2 AS build-env
WORKDIR /app
COPY *.csproj ./
RUN dotnet restore
COPY . ./
RUN dotnet publish -c Release -o out \
 && rm -rf Pages/
ENTRYPOINT ["dotnet", "out/RDB.Admin.dll"]
