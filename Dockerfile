# Estágio 1: Build
# Usamos a imagem oficial do SDK do .NET 8 (LTS)
FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
WORKDIR /src

# Copia todos os arquivos .csproj e restaura as dependências da API
# (Assumindo que sua API está em "PimVIII.Api/PimVIII.Api.csproj")
COPY *.sln .
COPY PimVIII.Api/*.csproj PimVIII.Api/
RUN dotnet restore PimVIII.Api/PimVIII.Api.csproj

# Copia o restante do código-fonte e faz o publish
COPY . .
WORKDIR /src/PimVIII.Api
RUN dotnet publish -c Release -o /app/publish

# Estágio 2: Runtime
# Usamos a imagem de runtime do ASP.NET, que é menor
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS final
WORKDIR /app
COPY --from=build /app/publish .

# Expõe a porta interna do container.
# Vamos usar 8080 para não ter conflito com nada.
EXPOSE 8080

# Define a URL que o Kestrel (servidor .NET) vai ouvir
ENV ASPNETCORE_URLS=http://+:8080

# Ponto de entrada
ENTRYPOINT ["dotnet", "PimVIII.Api.dll"]