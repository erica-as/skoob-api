# 📚 Skoob API

A **Skoob API** é uma aplicação backend desenvolvida em **.NET 10** que simula as funcionalidades de uma rede social para leitores (baseada no Skoob). O sistema permite o gerenciamento completo de utilizadores, livros, estantes virtuais, históricos de leitura e avaliações.

---

## Tecnologias Utilizadas

* **Runtime:** .NET 10.0 (Web API)
* **Banco de Dados:** PostgreSQL
* **ORM:** Entity Framework Core (EF Core)
* **Documentação:** OpenAPI / Swagger UI
* **Testes:** xUnit

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
2. Servidor do **PostgreSQL** em execução.
3. Ferramenta de linha de comando do EF Core instalada globalmente. Caso não tenhas, instala executando:
   ```bash
   dotnet tool install --global dotnet-ef
   ```

---

## Configuração Inicial

### 1. Configurar a String de Conexão
A string de conexão padrão com o PostgreSQL local encontra-se no ficheiro `src/Skoob.API/appsettings.json`. Altera o utilizador e a palavra-passe se as tuas definições locais forem diferentes:

```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=SkoobDB;Username=postgres;Password=Puc@2026"
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

Com o banco de dados configurado e atualizado, podes iniciar o servidor da API.

### Via Linha de Comando (CLI)
Na raiz do repositório, executa o seguinte comando:
```bash
dotnet run --project src/Skoob.API/
```

### Acessando o Swagger UI
Por padrão, em ambiente de desenvolvimento, a aplicação ativa o middleware do Swagger.
1. Abre o teu navegador.
2. Acede ao endereço local gerado pela aplicação (exemplo padrão do HostAddress) adicionando `/swagger`:
   ```text
   http://localhost:5143/swagger
   ```

---

## Como Executar os Testes

Para validar as regras do sistema utilizando a suite de testes configurada em xUnit, corre o comando na raiz do projeto:

```bash
dotnet test
```