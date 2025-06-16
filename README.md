# Mármore e Granito API

## Equipe de Desenvolvimento
- Gabriel Palmeiras
- Matheus Anholetti
- Raphael Picoli
- Allan Scherrer
- Maxuel Peçanha
- Davi Bresinski
- Juliano Jean Pierre

## Sobre o Projeto
A API de Mármore e Granito é um sistema de gerenciamento para indústrias de processamento de rochas ornamentais. O sistema permite o controle de blocos, chapas e processos de serragem, oferecendo uma solução completa para o gerenciamento do fluxo de produção.

## Configuração e Execução

### Pré-requisitos
- .NET 8.0 SDK
- PostgreSQL
- IDE de sua preferência (Visual Studio, VS Code, etc.)

### Passos para Execução

1. Clone o repositório:
```bash
git clone [url-do-repositorio]
```

2. Configure o banco de dados:
   - Crie um banco de dados PostgreSQL
   - Atualize a string de conexão no arquivo `appsettings.json`

3. Execute as migrações do banco de dados:
```bash
dotnet ef database update
```

4. Execute o projeto:
```bash
dotnet run
```

## Arquitetura do Projeto

O projeto segue uma arquitetura em camadas, utilizando padrões modernos de desenvolvimento:

### Estrutura de Diretórios
- `/Controllers`: Endpoints da API REST
- `/Models`: Entidades e modelos de domínio
- `/Data`: Contexto do Entity Framework e configurações do banco de dados
- `/Services`: Serviços de negócio, incluindo autenticação e hash de senhas
- `/Configurations`: Configurações da aplicação
- `/Migrations`: Scripts de migração do banco de dados

### Tecnologias Principais
- ASP.NET Core 8.0
- Entity Framework Core
- PostgreSQL
- JWT para autenticação
- Swagger/OpenAPI para documentação

### Padrões de Projeto
- Repository Pattern (via Entity Framework)
- Dependency Injection
- RESTful API
- Code-First Database Design

## Funcionalidades Principais
- Gerenciamento de usuários e autenticação
- Controle de blocos de mármore/granito
- Gestão de chapas
- Acompanhamento de processos de serragem
- Controle de estoque

## Documentação da API
A documentação completa da API estará disponível em `http://localhost:8080/swagger/index.html` através do Swagger quando a aplicação estiver em execução. 