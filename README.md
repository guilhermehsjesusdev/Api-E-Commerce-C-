# ECommerce API

API RESTful desenvolvida em **C# .NET 10** com **Clean Architecture**, autenticação JWT, CQRS com MediatR e banco de dados PostgreSQL.

---

## Tecnologias

- .NET 10
- ASP.NET Core Web API
- Entity Framework Core + Npgsql (PostgreSQL)
- ASP.NET Identity
- JWT Bearer Authentication
- MediatR (CQRS)
- FluentValidation
- AutoMapper
- Swagger / OpenAPI
- Docker + Docker Compose

---

## Arquitetura

O projeto segue os princípios da **Clean Architecture**, dividido em 4 camadas:

```
ECommerce/
├── ECommerce.Domain/           # Entidades, regras de negócio, interfaces
├── ECommerce.Application/      # Use Cases, DTOs, Commands, Queries (CQRS)
├── ECommerce.Infrastructure/   # EF Core, repositórios, AppDbContext
└── ECommerce.API/              # Controllers, Middleware, configuração
```

A regra de dependência é sempre de fora para dentro — o `Domain` não conhece nenhuma outra camada.

---

## Pré-requisitos

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [Docker](https://www.docker.com/) e Docker Compose
- [dotnet-ef](https://learn.microsoft.com/en-us/ef/core/cli/dotnet) (ferramenta global)

```bash
dotnet tool install --global dotnet-ef
```

---

## Configuração e execução local

### 1. Clone o repositório

```bash
git clone https://github.com/seu-usuario/ecommerce-api.git
cd ecommerce-api
```

### 2. Suba o banco de dados com Docker

```bash
docker-compose up -d
```

O PostgreSQL ficará disponível em `localhost:9000`.

### 3. Configure o appsettings.json

```json
{
  "ConnectionStrings": {
    "Default": "Host=localhost;Port=9000;Database=ecommercedb;Username=postgres;Password=postgres"
  },
  "Jwt": {
    "Key": "minha-chave-super-secreta-com-32-chars!!",
    "Issuer": "ECommerceAPI",
    "Audience": "ECommerceClient"
  }
}
```

### 4. Rode as migrations

```bash
dotnet ef database update \
  --project ECommerce.Infrastructure/ECommerce.Infrastructure.csproj \
  --startup-project ECommerce.API/ECommerce.API.csproj
```

### 5. Execute a API

```bash
dotnet run --project ECommerce.API/ECommerce.API.csproj
```

A API ficará disponível em `http://localhost:PORTA/swagger`.

---

## Endpoints

### Auth

| Método | Rota | Autenticação | Descrição |
|--------|------|-------------|-----------|
| POST | `/api/auth/register` | Não | Registra um novo usuário |
| POST | `/api/auth/login` | Não | Realiza login e retorna o token JWT |

**Exemplo de registro:**
```json
POST /api/auth/register
{
  "email": "usuario@email.com",
  "password": "Senha@123"
}
```

**Exemplo de login:**
```json
POST /api/auth/login
{
  "email": "usuario@email.com",
  "password": "Senha@123"
}
```

---

### Products

| Método | Rota | Autenticação | Descrição |
|--------|------|-------------|-----------|
| GET | `/api/products` | Não | Lista todos os produtos |
| GET | `/api/products/{id}` | Não | Busca produto por ID |
| POST | `/api/products` | Sim | Cria um novo produto |
| PUT | `/api/products/{id}` | Sim | Atualiza um produto |
| DELETE | `/api/products/{id}` | Sim | Remove um produto |

**Exemplo de criação:**
```json
POST /api/products
Authorization: Bearer {token}
{
  "name": "Camiseta Básica",
  "description": "Camiseta 100% algodão",
  "price": 49.90,
  "stock": 100,
  "categoryId": "guid-da-categoria"
}
```

---

### Categories

| Método | Rota | Autenticação | Descrição |
|--------|------|-------------|-----------|
| GET | `/api/categories` | Não | Lista todas as categorias |
| POST | `/api/categories` | Sim | Cria uma nova categoria |

**Exemplo de criação:**
```json
POST /api/categories
Authorization: Bearer {token}
{
  "name": "Roupas"
}
```

---

### Orders

| Método | Rota | Autenticação | Descrição |
|--------|------|-------------|-----------|
| GET | `/api/orders` | Sim | Lista pedidos do usuário autenticado |
| POST | `/api/orders` | Sim | Cria um novo pedido |
| PATCH | `/api/orders/{id}/status` | Sim | Atualiza o status do pedido |

**Exemplo de criação de pedido:**
```json
POST /api/orders
Authorization: Bearer {token}
{
  "items": [
    {
      "productId": "guid-do-produto",
      "quantity": 2
    }
  ]
}
```

**Status disponíveis:**
- `0` — Pending
- `1` — Confirmed
- `2` — Shipped
- `3` — Delivered
- `4` — Cancelled

---

## Autenticação

A API utiliza **JWT Bearer**. Após o login, inclua o token no header de todas as requisições protegidas:

```
Authorization: Bearer {seu-token-aqui}
```

---

## Docker Compose

```yaml
version: '3.8'

services:
  postgres:
    image: postgres:16
    container_name: ecommerce-db
    environment:
      POSTGRES_DB: ecommercedb
      POSTGRES_USER: postgres
      POSTGRES_PASSWORD: postgres
    ports:
      - "9000:5432"
    volumes:
      - pgdata:/var/lib/postgresql/data

volumes:
  pgdata:
```

---

## Deploy

### Backend → Render

1. Faça o push do projeto para o GitHub
2. Crie um novo **Web Service** no [Render](https://render.com)
3. Selecione o repositório e o Render detectará o `Dockerfile` automaticamente
4. Configure as variáveis de ambiente:

```
ConnectionStrings__Default=Host=seu-host;Port=5432;Database=ecommercedb;Username=user;Password=senha
Jwt__Key=sua-chave-secreta-com-32-chars
Jwt__Issuer=ECommerceAPI
Jwt__Audience=ECommerceClient
ASPNETCORE_ENVIRONMENT=Production
```

### Banco de dados → Render PostgreSQL

1. Crie um **PostgreSQL** gratuito no Render
2. Copie a connection string gerada e cole na variável `ConnectionStrings__Default`

### Frontend → Vercel

Configure a URL do frontend no `Program.cs` dentro da política de CORS:

```csharp
policy.WithOrigins("https://seu-app.vercel.app")
```

---

## Estrutura de pastas completa

```
ECommerce/
├── ECommerce.Domain/
│   ├── Entities/
│   │   ├── Product.cs
│   │   ├── Category.cs
│   │   ├── Order.cs
│   │   └── OrderItem.cs
│   ├── Enums/
│   │   └── OrderStatus.cs
│   ├── Exceptions/
│   │   └── DomainException.cs
│   └── Interfaces/
│       ├── IProductRepository.cs
│       ├── ICategoryRepository.cs
│       └── IOrderRepository.cs
├── ECommerce.Application/
│   ├── Common/Exceptions/
│   │   └── NotFoundException.cs
│   ├── Products/Commands/ (Create, Update, Delete)
│   ├── Products/Queries/ (GetAll, GetById)
│   ├── Products/Dtos/
│   ├── Orders/Commands/ (CreateOrder, UpdateOrderStatus)
│   ├── Orders/Queries/ (GetOrdersByUser)
│   ├── Orders/Dtos/
│   ├── Categories/Commands/ (CreateCategory)
│   └── Categories/Queries/ (GetAllCategories)
├── ECommerce.Infrastructure/
│   ├── Data/
│   │   ├── AppDbContext.cs
│   │   └── Configurations/
│   └── Repositories/
├── ECommerce.API/
│   ├── Controllers/
│   ├── Middleware/
│   ├── Program.cs
│   └── appsettings.json
├── docker-compose.yml
├── Dockerfile
└── README.md
```

---

## Licença

Este projeto foi desenvolvido para fins de estudos e portfólio.