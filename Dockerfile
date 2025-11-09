# Estágio 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
WORKDIR /src

# Copia os arquivos de projeto (.sln e .csproj) da raiz
COPY *.sln .
COPY *.csproj .

# Restaura as dependências do projeto da API
# (O arquivo .csproj está na raiz /src)
RUN dotnet restore PimVIII.Api.csproj

# Copia todo o resto do código-fonte
COPY . .

# Publica o projeto da API (o .csproj está na raiz /src)
RUN dotnet publish PimVIII.Api.csproj -c Release -o /app/publish

# Estágio 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS final
WORKDIR /app
COPY --from=build /app/publish .

# A porta e a URL são definidas no docker-compose.yml,
# então não precisamos defini-las aqui.
ENTRYPOINT ["dotnet", "PimVIII.Api.dll"]