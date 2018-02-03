FROM microsoft/aspnetcore-build:2.0

WORKDIR /src/app

COPY core/core.csproj /src/core/core.csproj
COPY common/common.csproj /src/common/common.csproj
COPY worker/worker.csproj .
RUN dotnet restore

COPY core/ /src/core/
COPY common/ /src/common/
COPY worker/ ./
CMD dotnet run
