FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release

WORKDIR /app

COPY . .

RUN dotnet restore "tests/Ambev.DeveloperEvaluation.Unit/Ambev.DeveloperEvaluation.Unit.csproj"

RUN dotnet build "tests/Ambev.DeveloperEvaluation.Unit/Ambev.DeveloperEvaluation.Unit.csproj" -c $BUILD_CONFIGURATION --no-restore

ENTRYPOINT ["dotnet","test","tests/Ambev.DeveloperEvaluation.Unit/Ambev.DeveloperEvaluation.Unit.csproj","-c","Release","--no-build","--verbosity","normal"]
