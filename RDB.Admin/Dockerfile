FROM microsoft/aspnetcore-build:2.0.0-preview2 AS builder
WORKDIR /app
COPY package.json package-lock.json ./
RUN npm install
COPY *.csproj .
RUN dotnet restore
COPY . .
RUN dotnet publish -c Release -o /app/bin

FROM microsoft/aspnetcore:2.0.0-preview2
WORKDIR /app
COPY --from=builder /app/bin .
ENTRYPOINT ["dotnet", "RDB.Admin.dll"]
