# PimVIII API

![C#](https://img.shields.io/badge/C%23-100%25-239120?logo=csharp)
![License](https://img.shields.io/badge/license-MIT-blue.svg)

API RESTful desenvolvida em ASP.NET Core 8 para gerenciar um ecossistema de streaming educacional. O sistema expõe CRUDs completos para usuários, criadores, conteúdos e playlists, além de tratar os relacionamentos entre eles via Entity Framework Core e PostgreSQL.

## Recursos principais

- CRUD completo para `Usuarios`, `Criadores`, `Conteudos` e `Playlists`
- Relacionamentos muitos-para-muitos entre playlists e conteúdos, e um-para-muitos entre usuários/playlists e criadores/conteúdos
- Documentação interativa com Swagger (`/swagger`)
- Camada de acesso a dados com padrão Repository e injeção de dependência
- Serialização JSON configurada para ignorar ciclos de referência

## Stack

- .NET 8 SDK
- ASP.NET Core Web API
- Entity Framework Core 8 + Npgsql Provider
- Swagger (Swashbuckle)
- PostgreSQL

## Pré-requisitos

- .NET 8 SDK instalado
- Servidor PostgreSQL acessível
- Ferramenta Entity Framework CLI (`dotnet tool install --global dotnet-ef`), opcional porém recomendada

## Configuração

1. Clone o repositório e entre na pasta `PimVIII.Api`.
2. Configure a string de conexão `DefaultConnection`. Recomenda-se usar [User Secrets](https://learn.microsoft.com/aspnet/core/security/app-secrets):

   ```powershell
   dotnet user-secrets init
   dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Host=localhost;Port=5432;Database=PimVIII_DB;Username=postgres;Password=senha"
   ```

   > Nunca exponha senhas válidas em `appsettings.json` no controle de versão.

3. Execute as migrações para criar o banco:

   ```powershell
   dotnet ef database update
   ```

### Produção / VPS

- Defina a variável de ambiente `ConnectionStrings__DefaultConnection` com a string real no servidor (PowerShell):

  ```powershell
  $Env:ConnectionStrings__DefaultConnection = "Host=seu_host;Port=5432;Database=sua_base;Username=usuario;Password=senha;SSL Mode=Require"
  ```

- Também é possível usar `appsettings.Production.json`, mantido fora do controle de versão, ou um gerenciador de segredos (ex.: Azure Key Vault, AWS Secrets Manager).

- A aplicação abortará o start se detectar o placeholder `YOUR_HOST`, garantindo que a credencial real foi carregada.

## Execução local

```powershell
dotnet build
dotnet run
```

A API iniciará na porta definida pelo perfil ativo (por padrão `https://localhost:7241`).

## Documentação e testes manuais

- Swagger: acesse `https://localhost:7241/swagger` para explorar e testar os endpoints.
- Arquivo `PimVIII.Api.http`: contém exemplos de requisições compatíveis com VS Code (extensão REST Client) ou Rider.

## Estrutura de diretórios

```text
PimVIII.Api/
  Controllers/         # Endpoints REST
  Data/                 # DbContext e configurações EF Core
  Entities/             # Modelos de domínio
  Interfaces/           # Contratos dos repositórios
  Repositories/         # Implementações de acesso a dados
  Migrations/           # Histórico de migrações EF Core
  Program.cs            # Configuração de pipeline e DI
```

## Endpoints principais

| Método | Rota                  | Descrição |
|--------|-----------------------|-----------|
| GET    | /api/Usuarios         | Lista todos os usuários |
| GET    | /api/Usuarios/{id}    | Busca um usuário pelo ID |
| POST   | /api/Usuarios         | Cria um novo usuário |
| PUT    | /api/Usuarios/{id}    | Atualiza um usuário existente |
| DELETE | /api/Usuarios/{id}    | Remove um usuário |
| GET    | /api/Criadores        | Lista todos os criadores |
| GET    | /api/Criadores/{id}   | Busca um criador pelo ID |
| POST   | /api/Criadores        | Cria um novo criador |
| PUT    | /api/Criadores/{id}   | Atualiza um criador existente |
| DELETE | /api/Criadores/{id}   | Remove um criador |
| GET    | /api/Conteudos        | Lista todos os conteúdos com seus criadores |
| GET    | /api/Conteudos/{id}   | Busca um conteúdo pelo ID |
| POST   | /api/Conteudos        | Cadastra um conteúdo |
| PUT    | /api/Conteudos/{id}   | Atualiza um conteúdo |
| DELETE | /api/Conteudos/{id}   | Remove um conteúdo |
| GET    | /api/Playlists        | Lista todas as playlists |
| GET    | /api/Playlists/{id}   | Busca uma playlist pelo ID (inclui conteúdos) |
| POST   | /api/Playlists        | Cria uma nova playlist |
| PUT    | /api/Playlists/{id}   | Atualiza uma playlist |
| DELETE | /api/Playlists/{id}   | Remove uma playlist |

## Modelos de domínio

- `Usuario`: `ID`, `Nome`, `Email`, coleção de `Playlists`
- `Criador`: `ID`, `Nome`, coleção de `Conteudos`
- `Conteudo`: `ID`, `Titulo`, `Tipo`, `CriadorID`, referência para `Criador`, playlists relacionadas
- `Playlist`: `ID`, `Nome`, `UsuarioID`, referência para `Usuario`, coleção de `Conteudos`

## Observações arquiteturais

- **Dependency Injection**: repositórios registrados como serviços `Scoped` em `Program.cs`.
- **EF Core**: mapeamentos personalizados no `AppDbContext` configuram relacionamentos, inclusive a tabela intermediária `ItemPlaylist`.
- **Serialização**: `ReferenceHandler.IgnoreCycles` evita loops ao retornar relacionamentos navegacionais.

## Contribuição

1. Crie um branch a partir de `main`.
2. Aplique as mudanças e adicione testes quando cabível.
3. Abra um Pull Request descrevendo o contexto e os passos de validação.

## Licença

MIT License

Copyright (c) 2025 PimVIII

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
