FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["core/core.csproj", "src/core/"]
COPY ["common/common.csproj", " src/common/"]
COPY ["services/services.csproj", " src/services/"]
COPY ["worker/worker.csproj", " src/worker/"]
COPY . .
WORKDIR "/src/worker"
RUN dotnet build "worker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "worker.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "worker.dll"]
