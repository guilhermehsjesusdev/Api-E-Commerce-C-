# ECommerce Front

Frontend do projeto E-Commerce desenvolvido com **Angular 18** e **Tailwind CSS**, consumindo a [ECommerce API](https://github.com/seu-usuario/ecommerce-api).

---

## Tecnologias

- Angular 18 (Standalone Components)
- Tailwind CSS
- TypeScript
- Angular Signals
- Angular Router (Lazy Loading)
- HttpClient com Interceptor JWT

---

## Funcionalidades

### Loja
- Vitrine de produtos com listagem e busca
- Página de detalhe do produto
- Carrinho de compras (gerenciado com Signals)
- Checkout e finalização de pedido

### Autenticação
- Cadastro de usuário
- Login com JWT
- Redirecionamento automático por role (Admin/Customer)
- Guard de rotas protegidas

### Painel Admin
- Gerenciamento de produtos (criar, listar, excluir)
- Gerenciamento de categorias
- Visualização e atualização de status de pedidos
- Gerenciamento de usuários (promover/rebaixar roles)

---

## Pré-requisitos

- [Node.js 18+](https://nodejs.org/)
- [Angular CLI 18](https://angular.dev/tools/cli)

```bash
npm install -g @angular/cli@18
```

---

## Configuração e execução local

### 1. Clone o repositório

```bash
git clone https://github.com/seu-usuario/ecommerce-front.git
cd ecommerce-front
```

### 2. Instale as dependências

```bash
npm install
```

### 3. Configure o environment

Edite o arquivo `src/environments/environment.development.ts`:

```ts
export const environment = {
  production: false,
  apiUrl: 'http://localhost:5103/api'
};
```

### 4. Rode o servidor de desenvolvimento

```bash
ng serve
```

Acesse em `http://localhost:4200`.

> O backend precisa estar rodando localmente. Veja as instruções em [ecommerce-api](https://github.com/seu-usuario/ecommerce-api).

---

## Estrutura de pastas

```
src/
├── app/
│   ├── core/
│   │   ├── guards/
│   │   │   ├── auth.guard.ts         # Protege rotas autenticadas
│   │   │   └── admin.guard.ts        # Protege rotas de admin
│   │   ├── interceptors/
│   │   │   └── auth.interceptor.ts   # Injeta token JWT nas requisições
│   │   ├── models/
│   │   │   ├── product.model.ts
│   │   │   ├── category.model.ts
│   │   │   ├── order.model.ts
│   │   │   └── user.model.ts
│   │   └── services/
│   │       ├── auth.service.ts
│   │       ├── product.service.ts
│   │       ├── category.service.ts
│   │       ├── order.service.ts
│   │       ├── cart.service.ts
│   │       └── user.service.ts
│   ├── features/
│   │   ├── auth/
│   │   │   ├── login/
│   │   │   └── register/
│   │   ├── shop/
│   │   │   ├── product-list/
│   │   │   ├── product-detail/
│   │   │   └── cart/
│   │   ├── checkout/
│   │   └── admin/
│   │       ├── products/
│   │       ├── categories/
│   │       ├── orders/
│   │       └── users/
│   ├── shared/
│   │   └── components/
│   │       └── navbar/
│   ├── app.routes.ts
│   └── app.config.ts
├── environments/
│   ├── environment.ts
│   ├── environment.development.ts
│   └── environment.prod.ts
└── styles.css
```

---

## Rotas

| Rota | Acesso | Descrição |
|------|--------|-----------|
| `/shop` | Público | Vitrine de produtos |
| `/shop/:id` | Público | Detalhe do produto |
| `/cart` | Público | Carrinho de compras |
| `/checkout` | Autenticado | Finalizar pedido |
| `/login` | Público | Login |
| `/register` | Público | Cadastro |
| `/admin/products` | Admin | Gerenciar produtos |
| `/admin/categories` | Admin | Gerenciar categorias |
| `/admin/orders` | Admin | Gerenciar pedidos |
| `/admin/users` | Admin | Gerenciar usuários |

---

## Autenticação e Roles

O sistema possui dois tipos de usuário:

| Role | Acesso |
|------|--------|
| **Customer** | Vitrine, carrinho, checkout, meus pedidos |
| **Admin** | Tudo + painel administrativo |

Após o login, o token JWT é armazenado no `localStorage` e injetado automaticamente em todas as requisições via interceptor.

O usuário admin padrão é criado automaticamente pelo backend:
```
Email: admin@ecommerce.com
Senha: Admin@123
```

---

## Build para produção

```bash
ng build
```

Os arquivos serão gerados em `dist/ecommerce-front`.

---

## Deploy no Vercel

1. Faça o push do projeto para o GitHub
2. Acesse [vercel.com](https://vercel.com) e importe o repositório
3. O Vercel detecta o Angular automaticamente
4. Configure a variável de ambiente ou atualize `environment.prod.ts`:

```ts
export const environment = {
  production: true,
  apiUrl: 'https://sua-api.onrender.com/api'
};
```

5. Crie um arquivo `vercel.json` na raiz para suporte ao Angular Router:

```json
{
  "rewrites": [
    { "source": "/(.*)", "destination": "/index.html" }
  ]
}
```

---

## Licença

Este projeto foi desenvolvido para fins de portfólio.