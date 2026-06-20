FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /root/task
COPY TaskApi.csproj ./
RUN dotnet restore TaskApi.csproj
COPY . ./
RUN dotnet publish TaskApi.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /root/task
COPY --from=build /app/publish ./
EXPOSE 8080
ENTRYPOINT ["dotnet", "TaskApi.dll"]
