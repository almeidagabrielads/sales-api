# Dockerfile.tests
# Este Dockerfile orquestra a execução dos testes funcionais em um container, aguardando a API estar disponível.

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS test
WORKDIR /src

# 1) Copia e restaura+builda a WebApi e seus projetos referenciados
COPY src/Ambev.DeveloperEvaluation.WebApi/       src/Ambev.DeveloperEvaluation.WebApi/
COPY src/Ambev.DeveloperEvaluation.IoC/          src/Ambev.DeveloperEvaluation.IoC/
COPY src/Ambev.DeveloperEvaluation.Domain/       src/Ambev.DeveloperEvaluation.Domain/
COPY src/Ambev.DeveloperEvaluation.Common/       src/Ambev.DeveloperEvaluation.Common/
COPY src/Ambev.DeveloperEvaluation.Application/  src/Ambev.DeveloperEvaluation.Application/
COPY src/Ambev.DeveloperEvaluation.ORM/          src/Ambev.DeveloperEvaluation.ORM/

RUN dotnet restore "src/Ambev.DeveloperEvaluation.WebApi/Ambev.DeveloperEvaluation.WebApi.csproj"
RUN dotnet build   "src/Ambev.DeveloperEvaluation.WebApi/Ambev.DeveloperEvaluation.WebApi.csproj" -c Release

# 2) Copia e restaura+builda os testes funcionais
COPY tests/Ambev.DeveloperEvaluation.Functional/  tests/Ambev.DeveloperEvaluation.Functional/
RUN dotnet restore "tests/Ambev.DeveloperEvaluation.Functional/Ambev.DeveloperEvaluation.Functional.csproj"
RUN dotnet build   "tests/Ambev.DeveloperEvaluation.Functional/Ambev.DeveloperEvaluation.Functional.csproj" -c Release

# 3) Copia script de espera e dá permissão
COPY wait-for.sh /usr/local/bin/wait-for.sh
RUN chmod +x /usr/local/bin/wait-for.sh

# 4) Entry point: aguarda a API e executa os testes
ENTRYPOINT ["bash", "-lc", "\
  /usr/local/bin/wait-for.sh webapi:8080 -- \
  dotnet test tests/Ambev.DeveloperEvaluation.Functional/Ambev.DeveloperEvaluation.Functional.csproj \
    -c Release --logger \"console;verbosity=detailed\" \
"]
