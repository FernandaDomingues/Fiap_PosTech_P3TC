# Fiap_PosTech_P2TC
Projeto para a segunda fase da Pós Tech em Arquitetura de Sistemas. 


## Visão Geral

Este é um projeto para a segunda fase da Pós Tech. A proposta do projeto é que seja criado um blog de notícias, e que sejam utilizadas ferramentas vistas em aula, como o GitHub Actions e containers com ACR e ACI.
O projeto será uma web API, que tem acesso ao banco de dados SQL, onde as notícias serão armazenadas. A API fará o gerenciamento de notícias e de usuários. O projeto ficará alocado em um container no Azure, e terá a pipeline automatizada por meio do GitHub Actions.
Para mais informações, consultar a documentação do projeto.

## Pré-requisitos

- [.NET 6.0](https://dotnet.microsoft.com/download/dotnet/6.0)
- [Sql Server](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads)

## Banco de dados
A configuração do banco de dados é feita através do arquivo appsettings.json, que fica na raiz do projeto TechChallenge2.Api. O arquivo já está configurado para o banco de dados Sql Server local, mas caso queira utilizar outro banco de dados, basta alterar a string de conexão. Você pode configurar também a váriavel TechChallengeConnection que pode conter o endereço do banco remoto, no caso deste projeto ele será publicado no Azure.


```json

  "ConnectionStrings": {
    "TechChallengeConnection": "string_de_conexao",
  }
```

Após configurar a string de conexão, é necessário realizar os comandos para a criação das tabelas no banco de dados configurado.
No console do nuget executar o seguinte passo a passo

Selecione o projeto padrão TechChallenge2.Identity

```bash
# Adicione uma nova migration
PM> Add-Migration "nomeMigration" -Context IdentityDataContext

# Atualize a base de dados do identity
PM> Update-Database -verbose -Context IdentityDataContext

```
Selecione o projeto padrão TechChallenge2.Data

```bash
# Adicione uma nova migration
PM> Add-Migration "nomeMigration" -Context DataContext

# Atualize a base de dados de noticias
PM> Update-Database -verbose -Context DataContext

```

## 🚀 Como executar o projeto

```bash
# Clone este repositório
$ git clone https://github.com/FernandaDomingues/Fiap_PosTech_P2TC

# Acesse a pasta do projeto no terminal o projeto API /cmd
$ cd ./Fiap_PosTech_P2TC\src\TechChallenge2\TechChallenge2.Api

# Execute a aplicação em modo de desenvolvimento
$ dotnet run

```

## Tecnologias

Ferramentas utilizadas no projeto:

- [C#](https://docs.microsoft.com/pt-br/dotnet/csharp/) - Linguagem
- [.NET 6](https://docs.microsoft.com/pt-br/dotnet/) - Framework
- [Swagger](https://swagger.io/) - Documentação da API
- [AutoMapper](https://automapper.org/) - Documentação
- [JWT](https://jwt.io/) - Ferramenta
- [Identity](https://learn.microsoft.com/pt-br/aspnet/core/security/authentication/identity?view=aspnetcore-7.0&tabs=visual-studio) - Documentação
- [EntityFramework](https://learn.microsoft.com/pt-br/dotnet/framework/data/adonet/ef/overview) - Documentação

## Autora

- **Fernanda Domingues** - _Desenvolvedora_ 
