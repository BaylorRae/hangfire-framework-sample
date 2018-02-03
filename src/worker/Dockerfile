FROM microsoft/aspnetcore-build:2.0

WORKDIR /src/app

COPY core/core.csproj /src/core/core.csproj
COPY common/common.csproj /src/common/common.csproj
COPY services/services.csproj /src/services/services.csproj
COPY worker/worker.csproj .
RUN dotnet restore

COPY core/ /src/core/
COPY common/ /src/common/
COPY services/ /src/services/
COPY worker/ ./
CMD dotnet run
