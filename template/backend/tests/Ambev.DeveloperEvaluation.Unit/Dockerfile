FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY . .

RUN dotnet restore

RUN dotnet build --no-restore

WORKDIR /app/tests/Ambev.DeveloperEvaluation.Unit

ENTRYPOINT ["dotnet", "test", "--no-build", "--verbosity:normal"]
