FROM microsoft/aspnetcore-build:2.0.0 AS builder
WORKDIR /app
COPY package.json package-lock.json ./
RUN npm config set @creuna:registry https://www.myget.org/F/creuna/npm/ \
  && npm install
COPY *.csproj .
RUN dotnet restore
COPY . .
RUN dotnet publish -c Release -o /app/bin --no-restore

FROM microsoft/aspnetcore:2.0.0
WORKDIR /app
COPY --from=builder /app/bin .
ENTRYPOINT ["dotnet", "RDB.Admin.dll"]
