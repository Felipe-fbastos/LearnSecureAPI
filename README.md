# LearnSecureAPI

API desenvolvida com foco em autenticação, autorização e segurança utilizando JWT em ASP.NET Core.

---

# Sobre o Projeto

O LearnSecureAPI é um projeto de estudo voltado para autenticação segura de usuários em APIs REST utilizando JWT (JSON Web Token), criptografia de senha com BCrypt e controle de acesso baseado em Roles.

O objetivo do projeto foi praticar conceitos fundamentais de backend moderno, incluindo:

- Autenticação JWT
- Hash de senha com BCrypt
- Autorização baseada em Roles
- Proteção de rotas
- Entity Framework Core
- DTOs
- Mapster
- SQL Server
- Injeção de Dependência
- Boas práticas de segurança em APIs

---

# Tecnologias Utilizadas

- ASP.NET Core
- Entity Framework Core
- SQL Server
- JWT Bearer Authentication
- BCrypt.Net
- Mapster
- Swagger/OpenAPI

---

# Funcionalidades

## Autenticação

- Cadastro de usuários
- Login com JWT
- Criptografia segura de senhas com BCrypt
- Geração de Token JWT
- Validação de Token

---

## Autorização

- Rotas protegidas
- Controle de acesso por Role
- Roles:
  - Administrator
  - User

---

## Usuários

- Buscar todos os usuários
- Buscar usuário autenticado
- Acesso restrito baseado em permissões

---

# Estrutura do Projeto

```bash
LearnSecureAPI/
│
├── Controllers/
├── Data/
├── DTO/
├── Mapper/
├── Model/
├── Services/
├── Migrations/
├── Program.cs
└── appsettings.json

Segurança Implementada
BCrypt Password Hashing

As senhas não são armazenadas em texto puro.

O projeto utiliza BCrypt para:

Gerar hash seguro
Comparar senha digitada com hash salvo
Proteger credenciais dos usuários
JWT Authentication

O projeto utiliza JWT para autenticação.

O token contém:

Id do usuário
Username
Role
Tempo de expiração

As rotas protegidas validam:

Assinatura do token
Expiração
Issuer
Audience
Endpoints
Authentication
SignUp
POST /User/SignUp
Login
POST /User/Login
Users
Get Logged User
GET /User/Single
Get All Users
GET /User/GetAll

Acesso permitido apenas para:

Administrator
Exemplo de Login
Request
{
  "email": "admin@gmail.com",
  "password": "123456"
}
Response
{
  "token": "eyJhbGciOiJIUzI1NiIs..."
}
Como Utilizar o Token

No Swagger ou Postman:

Authorization: Bearer SEU_TOKEN
Como Executar o Projeto
1. Clone o repositório
git clone https://github.com/Felipe-fbastos/LearnSecureAPI.git
2. Acesse a pasta do projeto
cd LearnSecureAPI
3. Configure a Connection String

No arquivo appsettings.json:

"ConnectionStrings": {
  "DefaultConnection": "SUA_CONNECTION_STRING"
}
4. Configure a Secret Key JWT

Utilize User Secrets:

dotnet user-secrets init
dotnet user-secrets set "Jwt:Key" "SUA_SECRET_KEY"
5. Execute as Migrations
dotnet ef database update
6. Execute a aplicação
dotnet run
Conceitos Praticados
JWT Authentication
Authorization Roles
BCrypt Password Hashing
Entity Framework Core
DTO Pattern
Dependency Injection
API Security
REST API
Claims
Protected Routes
ASP.NET Core Middleware
🚀 CI/CD

Este projeto utiliza um pipeline de CI/CD automatizado com GitHub Actions.

✔ Continuous Integration (CI)

A cada push ou pull request na branch principal, o pipeline executa automaticamente:

Restore das dependências
Build da aplicação
Execução de testes automatizados

Isso garante que qualquer alteração seja validada antes de ser integrada ao projeto.

☁️ Continuous Deployment (CD) (opcional)

O pipeline pode ser estendido para realizar deploy automático da aplicação em ambientes como Azure App Service.

O processo automatiza:

Build em modo Release
Publicação da aplicação
Deploy em ambiente de nuvem
⚙️ Benefícios do CI/CD
Detecção rápida de erros
Garantia de qualidade no código
Automação do processo de validação
Redução de falhas em produção
🛠️ Ferramentas utilizadas
GitHub Actions
.NET 8
Azure App Service (opcional para deploy)
Melhorias Futuras
Refresh Token
Clean Architecture
FluentValidation
Global Exception Middleware
Docker
Unit Tests
Repository Pattern
Identity
Autor

Desenvolvido por Felipe Bastos.

GitHub:

https://github.com/Felipe-fbastos
Projeto para Estudos

Este projeto foi desenvolvido com foco em aprendizado de autenticação segura em APIs modernas utilizando ASP.NET Core.
