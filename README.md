# LearnSecureAPI

API desenvolvida com foco em autenticação, autorização e segurança utilizando JWT em ASP.NET Core.

---

# Sobre o Projeto

O LearnSecureAPI é um projeto de estudo voltado para autenticação segura de usuários em APIs REST utilizando JWT (JSON Web Token), criptografia de senha com BCrypt e controle de acesso baseado em Roles.

O objetivo do projeto foi praticar conceitos fundamentais de backend moderno, incluindo:

* Autenticação JWT
* Hash de senha com BCrypt
* Autorização baseada em Roles
* Proteção de rotas
* Entity Framework Core
* DTOs
* Mapster
* SQL Server
* Injeção de Dependência
* Boas práticas de segurança em APIs
* Implementação de esteira CI/CD profissional

---

# Tecnologias Utilizadas

* **Linguagem & Framework:** ASP.NET Core 8
* **Banco de Dados:** SQL Server
* **ORM:** Entity Framework Core
* **Segurança:** JWT Bearer Authentication & BCrypt.Net
* **Mapeamento:** Mapster
* **Documentação:** Swagger/OpenAPI
* **CI/CD:** GitHub Actions
* **Cloud:** Microsoft Azure (App Service)

---

# 🚀 CI/CD & DevOps

Este projeto utiliza um pipeline de **CI/CD** automatizado com **GitHub Actions** para garantir a qualidade e a agilidade no desenvolvimento:

### ✔ Continuous Integration (CI)
A cada `push` ou `pull request` na branch principal, o pipeline executa automaticamente:
* **Restore:** Restauração das dependências do projeto.
* **Build:** Compilação da aplicação para verificar erros de sintaxe.
* **Test:** Execução de testes automatizados para validar a integridade do código.

### ☁️ Continuous Deployment (CD)
O projeto está configurado para realizar o deploy automático no **Azure App Service**:
* O processo automatiza o build em modo Release e a publicação da aplicação.
* O deploy é realizado no ambiente de nuvem (região France Central) utilizando perfis de publicação seguros via GitHub Secrets.

---

# Funcionalidades

## Autenticação e Autorização
* Cadastro de usuários e login com geração de Token JWT.
* Criptografia segura de senhas com BCrypt.
* Controle de acesso baseado em **Roles**:
  * `Administrator`: Acesso total, incluindo gestão de usuários.
  * `User`: Acesso limitado a funcionalidades básicas.

## Usuários
* Buscar todos os usuários (apenas para Administrator).
* Buscar dados do usuário autenticado.
* Proteção de rotas via Middleware e validação de Claims.

---

# Estrutura do Projeto

```bash
LearnSecureAPI/
│
├── .github/workflows/   # Pipeline de automação CI/CD
├── Controllers/         # Endpoints da API
├── Data/                # Contexto do banco e configurações
├── DTO/                 # Data Transfer Objects
├── Mapper/              # Configurações do Mapster
├── Model/               # Entidades do Banco de Dados
├── Services/            # Lógica de negócio e serviços JWT
├── Migrations/          # Versões do banco de dados
├── Program.cs           # Configuração da aplicação
└── appsettings.json     # Configurações de ambiente

# Segurança Implementada

## BCrypt Password Hashing

As senhas não são armazenadas em texto puro. O projeto utiliza o algoritmo BCrypt para gerar hashes seguros, dificultando ataques de força bruta e protegendo as credenciais dos usuários.

---

## JWT Authentication

O token JWT carrega Claims de identidade (ID, Username e Role). A API valida a assinatura, expiração, Issuer (emissor) e Audience (público) em cada requisição protegida.

---

# Endpoints Principais

## Authentication

- `POST /User/SignUp` → Cadastro de novos usuários  
- `POST /User/Login` → Autenticação e recebimento do Token  

---

## Users

- `GET /User/Single` → Retorna os dados do usuário autenticado  
- `GET /User/GetAll` → Lista todos os usuários (Acesso restrito ao Administrator)  

---
