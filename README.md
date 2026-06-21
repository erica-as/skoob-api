Para que possas simplesmente copiar o conteúdo de forma limpa e direta, sem formatações duplicadas ou blocos aninhados que quebram o botão de cópia do chat, o conteúdo completo do teu arquivo `README.md` atualizado está disponível no bloco de texto abaixo:

# 📚 Skoob API

A **Skoob API** é uma aplicação backend desenvolvida em **.NET 10** que simula as funcionalidades de uma rede social para leitores (baseada no Skoob). O sistema permite o gerenciamento completo de utilizadores, livros, estantes virtuais, históricos de leitura e avaliações.

---

## Tecnologias Utilizadas

* **Runtime:** .NET 10.0 (Web API)
* **Banco de Dados:** PostgreSQL (Produção/Desenvolvimento) & EF Core InMemory (Testes)
* **ORM:** Entity Framework Core (EF Core)
* **Documentação:** OpenAPI / Swagger UI
* **Testes:** xUnit
* **Containerização:** Docker

---

## Arquitetura e Estrutura do Projeto

O projeto está estruturado em soluções que separam a API dos testes automatizados (`SkoobApi.slnx`):

* **`src/Skoob.API/`**: Código principal da aplicação Web API.
* **`Controllers/`**: Endpoints expostos para a comunicação com o cliente (`Usuario`, `Livro`, `Estante`, `Leitura`).
* **`DTOs/`**: Objetos de Transferência de Dados para validação e segurança das requisições.
* **`Data/`**: Contexto de acesso ao banco de dados (`SkoobDbContext`).
* **`Models/`**: Entidades de domínio do sistema.
* **`Repositories/`**: Camada responsável pela persistência direta de dados.
* **`Services/`**: Camada que centraliza todas as regras de negócio.


* **`tests/Skoob.Tests/`**: Suite de testes automatizados.

---

## Pré-requisitos para Execução

Antes de começar, garante que tens instalado na tua máquina:

1. **.NET 10 SDK** ou superior.
2. Servidor do **PostgreSQL** em execução (se fores rodar nativamente).
3. **Docker Desktop** instalado (se fores rodar via container).
4. Ferramenta de linha de comando do EF Core instalada globalmente. Caso não tenhas, instala executando:
```bash
dotnet tool install --global dotnet-ef

```

---

## Configuração Inicial

### 1. Configurar a String de Conexão

A string de conexão padrão com o PostgreSQL local encontra-se no ficheiro `src/Skoob.API/appsettings.json`. Altera o utilizador e a palavra-passe se as tuas definições locais forem diferentes:

```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=SkoobDB;Username=postgres;Password=SUA_SENHA"
}

```

---

## Como Executar as Migrations (Entity Framework)

O projeto utiliza o Entity Framework Core para gerir a estrutura das tabelas. Executa os seguintes comandos a partir do diretório raiz da solução:

### 1. Criar uma nova Migration

Caso faças alterações nas classes da pasta `Models`, gera um novo histórico de migração especificando o projeto de inicialização:

```bash
dotnet ef migrations add NomeDaSuaMigration --project src/Skoob.API/

```

### 2. Aplicar as Migrations no Banco de Dados

Para criar a base de dados `SkoobDB` e todas as tabelas necessárias (`Usuarios`, `Livros`, `EstantesLivros`, etc.), executa:

```bash
dotnet ef database update --project src/Skoob.API/

```

---

## Como Executar a Aplicação

### Via Linha de Comando (CLI Nativa)

Com o banco de dados configurado e updated, podes iniciar o servidor da API executando o seguinte comando na raiz do repositório:

```bash
dotnet run --project src/Skoob.API/

```

### Via Docker (Container)

Se preferires rodar a aplicação isolada num container Docker através do `Dockerfile` presente na raiz, executa os seguintes comandos a partir do diretório raiz:

1. **Construir a imagem Docker:**
```bash
docker build -t skoob-api .

```


2. **Executar o container:**
```bash
docker run -d -p 5143:8080 --name skoob-container -e ASPNETCORE_ENVIRONMENT=Development skoob-api
```

---

### Acessando o Swagger UI
A documentação interativa da API está configurada para funcionar em todos os ambientes e utiliza caminhos relativos para evitar erros de bloqueio de conteúdo misto (CORS) em servidores HTTPS. Acesse em:

```text
https://skoob-api-9vp7.onrender.com/swagger
(Para testes locais usando os comandos padrão, use https://localhost:5143/swagger ou http://localhost:5143/swagger).
```

---

## Como Executar os Testes

A suite de testes utiliza o fornecedor de banco de dados **InMemory** para garantir que as regras de negócio sejam validadas de forma isolada e sem dependência de um banco PostgreSQL físico ativo.

Caso precises de restaurar as dependências de teste pela primeira vez, executa o seguinte comando na raiz:

```bash
dotnet add tests/Skoob.Tests/Skoob.Tests.csproj package Microsoft.EntityFrameworkCore.InMemory

```

Para correr a suite de testes automatizados completa (`xUnit`), executa o comando na raiz do projeto:

```bash
dotnet test

```