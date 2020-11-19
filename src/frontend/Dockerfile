FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["core/core.csproj", "src/core/"]
COPY ["common/common.csproj", " src/common/"]
COPY ["services/services.csproj", " src/services/"]
COPY ["frontend/frontend.csproj", " src/frontend/"]
COPY . .
WORKDIR "/src/frontend"
RUN dotnet build "frontend.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "frontend.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "frontend.dll"]
