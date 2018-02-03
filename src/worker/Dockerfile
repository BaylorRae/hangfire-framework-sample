FROM microsoft/aspnetcore-build:2.0

WORKDIR /src

COPY worker.csproj .
RUN dotnet restore

COPY . .
CMD dotnet run
