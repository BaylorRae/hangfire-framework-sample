FROM microsoft/aspnetcore-build:2.0

WORKDIR /src/app

COPY core/core.csproj /src/core/core.csproj
COPY common/common.csproj /src/common/common.csproj
COPY frontend/frontend.csproj .
RUN dotnet restore

COPY core/ /src/core/
COPY common/ /src/common/
COPY frontend/ ./
CMD dotnet run
