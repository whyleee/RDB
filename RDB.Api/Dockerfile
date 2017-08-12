FROM microsoft/aspnetcore-build:2.0.0-preview2 AS build-env
WORKDIR /app
COPY *.csproj ./
RUN dotnet restore
COPY . ./
RUN dotnet publish -c Release -o out

FROM microsoft/aspnetcore:2.0.0-preview2
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "RDB.Api.dll"]
