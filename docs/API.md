# 📚 Documentação da API - 🖥️ API

Explore a documentação:

🏁 [Introdução (README.md)](../README.md)  | ⚙️ [Instalação](INSTALL.md) | 📓 [Guia de Uso](USAGE.md) | 🖥️ [API](API.md)


### Nesta seção, você encontrará informações sobre os endpoints disponíveis, seus parâmetros, respostas e exemplos de uso.
#### Caso tenha alguma dúvida sobre o funcionamento do projeto, acesse o [Guia de Uso](USAGE.md) para mais informações.

---

### 🤝 Clientes
<!-- GET clients -->
<details>
<summary>1. [GET]/api/Clients</summary>

### Descrição:
Retorna a lista de recursos disponíveis.

#### Parâmetros:
- **Query Parameters (opcional):**
  - `pageNumber` (integer): Número da página. Exemplo: `1`.
  - `pageSize` (integer): Quantidade de itens por página. Exemplo: `10`.

#### Exemplo de Requisição:
```bash
curl -X GET "http://localhost:7198/api/Client?pageNumber=1&pageSize=10" -H "Accept: application/json"
```

#### **Exemplo de Retorno[200]**:
```json
{
  "pageNumber": 1,
  "pageSize": 2,
  "totalItems": 1005,
  "totalPages": 503,
  "items": [
    {
      "id": "guid-id",
      "name": "name-client",
      "email": "email-client",
      "city": "city-client",
      "pathImage": null
    },
    {
      "id": "guid-id",
      "name": "name-client",
      "email": "email-client",
      "city": "city-client",
      "pathImage": null
    },
  ]
}
```
</details>
<!-- End GET clients -->

<!-- GET client ID -->
<details>
<summary>2. [GET]/api/Clients/{id}</summary>

#### Descrição:
Retorna o cliente por id.

#### Parâmetros:
- **Path Parameters (obrigatório):**
  - `id` (guid): id do cliente. Exemplo: `3fa85f64-5717-4562-b3fc-2c963f66afa6`.

#### Exemplo de Requisição:
```bash
curl -X GET "http://localhost:7198/api/Clients/3fa85f64-5717-4562-b3fc-2c963f66afa6" -H "Accept: application/json"
```

#### **Exemplo de Retorno[200]:**
```json
{
  "id": "guid-id",
  "name": "name-client",
  "email": "email-client",
  "city": "city-client",
  "pathImage": null
}
```
</details>
<!-- End GET client ID -->



<!-- Get client by name -->
<details>
<summary>3. [GET]/api/Clients/{name}</summary>

#### Descrição:
Retorna o cliente por nome. Busca cliente com nome relacionado ao parâmetro.

#### Parâmetros:
- **Path Parameters (obrigatório):**
  - `name` (string): nome do cliente. Exemplo: `Jose`.

#### Exemplo de Requisição:
```bash
curl -X GET "http://localhost:7198/api/Clients/Jose" -H "Accept: application/json"
```

#### **Exemplo de Retorno[200]:**
```json
[
    {
    "id": "guid-id",
    "name": "name-client",
    "email": "email-client",
    "city": "city-client",
    "pathImage": null
    }
]
```
</details>
<!-- End Get client by name -->

<!-- Post client -->
<details>
<summary>4. [Post]/api/Clients</summary>

#### Descrição:
Cadastro de novo cliente.

#### Parâmetros:
- **Json Data (obrigatório):**
  - `name` (string): nome do cliente. Exemplo: `Jose`.
  - `email` (string): email do cliente (valor unico). Exemplo: `jose@email.com`.
  - `city` (string): cidade do cliente. Exemplo: `Curitiba`.

#### Exemplo de Requisição:
```bash
curl -X POST "http://localhost:7198/api/Clients" -H "Accept: application/json"
```

#### Exemplo de json data:
```json
{
  "name": "Jose",
  "email": "jose@email.com",
  "city": "Curitiba"
}
```

#### **Exemplo de Retorno[200]:**
```json
{
"id": "guid-id",
"name": "name-client",
"email": "email-client",
"city": "city-client",
"pathImage": null
}
```
</details>
<!-- End Post client -->


<!-- Put client -->
<details>
<summary>5. [Put]/api/Clients</summary>

#### Descrição:
Atualizar dados do cliente.

#### Parâmetros:
- **Json Data (obrigatório):**
  - `id` (guid): id do cliente. Exemplo: `3fa85f64-5717-4562-b3fc-2c963f66afa6`.
  - `name` (string): nome do cliente. Exemplo: `Jose`.
  - `email` (string): email do cliente (valor unico). Exemplo: `jose@email.com`.
  - `city` (string): cidade do cliente. Exemplo: `Curitiba`.
  - `pathImage` (string): cidade do cliente. Exemplo: `null`.

#### Exemplo de Requisição:
```bash
curl -X PUT "http://localhost:7198/api/Clients" -H "Accept: application/json"
```

#### Exemplo de json data:
```json
{
    "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "name": "Jose",
    "email": "jose@email.com",
    "city": "Curitiba",
    "pathImage": null
}
```

#### **Exemplo de Retorno[200]:**
```json
{
"id": "guid-id",
"name": "name-client",
"email": "email-client",
"city": "city-client",
"pathImage": null
}
```
</details>
<!-- End Put client -->


<!-- Delete client -->
<details>
<summary>6. [Delete]/api/Clients/{id}</summary>

#### Descrição:
Deletar cliente.

#### Parâmetros:
- **Path Parameters (obrigatório):**
  - `id` (guid): id do cliente. Exemplo: `3fa85f64-5717-4562-b3fc-2c963f66afa6`

#### Exemplo de Requisição:
```bash
curl -X DELETE "http://localhost:7198/api/Clients/3fa85f64-5717-4562-b3fc-2c963f66afa6" -H "Accept: application/json"
```

#### **Retorno[204]**

</details>
<!-- End Delete client -->


<!-- Post client file -->
<details>
<summary>7. [POST]/api/Clients/file</summary>

#### Descrição:
Adicionar imagem ao cliente.

#### Parâmetros:
- **Form Data (obrigatório):**
  - `id` (guid): id do cliente. Exemplo: `3fa85f64-5717-4562-b3fc-2c963f66afa6`
  - `pathImage` (file): imagem do cliente.

#### Exemplo de Requisição:
```bash
curl -X POST "http://localhost:7198/api/Clients/" -H "Content-Type: multipart/form-data"
```

#### **Retorno[200]**
```json	
{
  "name": "3fa85f64-5717-4562-b3fc-2c963f66afa6.png",
  "type": ".png",
  "url": "http://localhost:7198/api/files/2/3fa85f64-5717-4562-b3fc-2c963f66afa6"
}
```
</details>
<!-- End Post client file -->


<!-- Post client file -->
<details>
<summary>8. [DELETE]/api/Clients/file</summary>

#### Descrição:
Deletar imagem do cliente.

#### Parâmetros:
- **Path Parameters (obrigatório):**
  - `id` (guid): id do cliente. Exemplo: `3fa85f64-5717-4562-b3fc-2c963f66afa6`

#### Exemplo de Requisição:
```bash
curl -X DELETE "http://localhost:7198/api/Clients/7cf63a1b-4317-4df1-b401-265f0be742df" -H "Accept: application/json"
```

#### **Retorno[204]**
</details>
<!-- End Post client file -->

---


# Documentação da API SalesCrud

  

## Visão Geral

A API SalesCrud é uma solução completa para gerenciamento de vendas, produtos, clientes e autenticação, projetada para atender às necessidades de pequenas e médias empresas.

  

## Informações Básicas

-  **Versão da API:** 1.0

-  **Autenticação:** Token JWT Bearer

-  **Formato de Requisições:** JSON

-  **Segurança:** Todas as rotas requerem autenticação

  
# 🔐 Autenticação

#### Visão Geral

O módulo de autenticação é responsável pelo gerenciamento de usuários, incluindo registro, login e validação de credenciais.

---

### 1. Registro de Usuário


-  **Endpoint:**  `POST /api/Auth/signup`

-  **Descrição:** Permite o cadastro de novos usuários no sistema

  

#### Modelo de Requisição

```json
{
	"email": "usuario@exemplo.com",
	"password": "SenhaForte123!",
	"confirmPassword": "SenhaForte123!",
	"fullName": "Nome Completo do Usuário",
	"dateOfBirth": "1990-01-01",
	"userRole": 1
}
```

  

#### Campos Obrigatórios

### 1. email

- Tipo: String

- Formato: E-mail válido

- Restrições:

    - Deve ser único

    - Mínimo de 1 caractere

    - Formato de e-mail válido

  

### 2. password

- Tipo: String

- Restrições:

    - Mínimo de 1 caractere

    - Recomendações de segurança:

    - Mínimo de 8 caracteres

    - Combinação de maiúsculos, minúsculos, números e caracteres especiais

  

### 3. confirmPassword

- Deve ser idêntico ao campo password


### 4. fullName

- Tipo: String

- Mínimo de 1 caractere

  

### 5. dateOfBirth

- Tipo: Data

- Formato: YYYY-MM-DD

- Validações: 
    - Usuário deve ser maior de idade
    - Data válida


### 6. userRole

- Tipo: Inteiro

- Valores possíveis:

    -  `1`: Administrador

    -  `2`: Usuário Padrão

  

#### Respostas Possíveis

status | descrição
------------ | -------------
**200 OK** | Usuário criado com sucesso
**400 Bad Request** | Dados inválidos, E-mail já cadastrado, Senhas não conferem
**500 Internal Server Error**  | Erro no servidor

---

### 2. Login de Usuário

-  **Endpoint:**  `POST /api/Auth/signin`

-  **Descrição:** Autenticação de usuário e geração de token de acesso

  

#### Modelo de Requisição

```json
{
    "email": "usuario@exemplo.com",
    "password": "SenhaForte123!"
}
```

  
#### Campos Obrigatórios

### 1. email

- Tipo: String

- Formato: E-mail válido

  

### 2.  password

- Tipo: String

- Senha de acesso

  

#### Resposta de Sucesso
```json
{
    "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
    "expiration": "2024-01-01T23:59:59Z",
    "userRole": 1
}

```

  

#### Respostas Possíveis

status | descrição
------------ | -------------
**200 OK** | Login bem-sucedido
**401 Unauthorized** | Credenciais inválidas, Usuário bloqueado
**400 Bad Request**  | Dados de login incorretos
**500 Internal Server Error**  | Erro no servidor

---

### 3. Validação de Token

-  **Endpoint:**  `GET /api/Auth/validate`

-  **Descrição:** Verifica a validade do token de autenticação atual

  

#### Cabeçalho Necessário
```bash
    Authorization: Bearer <token>
```

  

#### Respostas Possíveis

status | descrição
------------ | -------------
**200 OK** | Token válido
**401 Unauthorized** | Token inválido ou expirado
**500 Internal Server Error**  | Erro no servidor


## Boas Práticas de Segurança
- Utilize sempre HTTPS
- Mantenha a senha confidencial
- Troque a senha periodicamente
- Utilize autenticação de dois fatores quando possível

  

## Política de Senhas

- Mínimo de 8 caracteres
- Combinação de:
    - Letras maiúsculas
    - Letras minúsculas
    - Números
    - Caracteres especiais
- Evite senhas comuns ou facilmente identificáveis

  

## Funções de Usuário

-  **Administrador (1)**:
    - Acesso completo ao sistema
    - Pode gerenciar usuários
    - Permissões máximas

  

-  **Usuário Padrão (2)**:
    - Acesso limitado
    - Permissões restritas
    - Sem acesso a configurações administrativas

  

## Tratamento de Erros

Códigos de erro detalhados são retornados para auxiliar na identificação de problemas durante a autenticação.

  

## Considerações Finais

- O token de autenticação tem tempo de expiração

- Renove o token antes da expiração

- Mantenha o token em segurança
  
---
---
---


# 👥 Clientes

#### Visão Geral
O módulo de Clientes gerencia o cadastro, consulta, atualização e exclusão de informações de clientes na plataforma.

---


### 1. Cadastro de Cliente
- **Endpoint:** `POST /api/Clients`
- **Descrição:** Adiciona um novo cliente ao sistema

#### Modelo de Requisição
```json
{
  "name": "Maria Aparecida Silva",
  "email": "maria.silva@exemplo.com",
  "city": "São Paulo"
}
```

#### Campos Obrigatórios
### name
   - Tipo: String
   - Restrições:
     - Mínimo de 1 caractere
     - Não pode ser nulo
     - Aceita nome completo

### email
   - Tipo: String
   - Restrições:
     - Formato de e-mail válido
     - Deve ser único no sistema
     - Mínimo de 1 caractere

### city
   - Tipo: String
   - Restrições:
     - Mínimo de 1 caractere
     - Nome da cidade do cliente

#### Respostas Possíveis
status | descrição
------------ | -------------
**200 OK** | Cliente cadastrado com sucesso
**400 Bad Request** | E-mail já cadastrado, Dados inválidos
**500 Internal Server Error**  | Erro no servidor

---

### 2. Listagem de Clientes
- **Endpoint:** `GET /api/Clients`
- **Descrição:** Recupera lista de clientes com paginação

#### Parâmetros de Consulta
- `pageNumber` (opcional)
  - Tipo: Inteiro
  - Padrão: 1
  - Descrição: Número da página de resultados

- `pageSize` (opcional)
  - Tipo: Inteiro
  - Padrão: 10
  - Descrição: Quantidade de registros por página

#### Exemplo de Resposta
```json
{
  "totalClients": 150,
  "currentPage": 1,
  "pageSize": 10,
  "clients": [
    {
      "id": "123e4567-e89b-12d3-a456-426614174000",
      "name": "Maria Aparecida Silva",
      "email": "maria.silva@exemplo.com",
      "city": "São Paulo"
    },
    // ... outros clientes
  ]
}
```

#### Respostas Possíveis
status | descrição
------------ | -------------
**200 OK** | Cliente cadastrado com sucesso
**500 Internal Server Error**  | Erro no servidor

---

### 3. Atualização de Cliente
- **Endpoint:** `PUT /api/Clients`
- **Descrição:** Atualiza informações de um cliente existente

#### Modelo de Requisição
```json
{
  "id": "123e4567-e89b-12d3-a456-426614174000",
  "name": "Maria Aparecida Silva Santos",
  "email": "maria.santos@exemplo.com",
  "city": "Rio de Janeiro",
  "pathImage": "caminho/para/imagem.jpg"
}
```

#### Campos Obrigatórios
### id
   - Tipo: UUID
   - Descrição: Identificador único do cliente
   - Restrição: Não pode ser o UUID nulo

### name
   - Tipo: String
   - Restrições: Mínimo de 1 caractere

### email
   - Tipo: String
   - Restrições: Formato de e-mail válido

### city
   - Tipo: String
   - Restrições: Mínimo de 1 caractere

#### Campos Opcionais
- **pathImage**
  - Tipo: String (nullable)
  - Descrição: Caminho para imagem do cliente

#### Respostas Possíveis
status | descrição
------------ | -------------
**200 OK** | Cliente cadastrado com sucesso
**400 Bad Request** | Dados inválidos
**500 Internal Server Error**  | Erro no servidor

---

### 4. Consulta de Cliente por ID
- **Endpoint:** `GET /api/Clients/{id}`
- **Descrição:** Recupera informações de um cliente específico

#### Parâmetros de Rota
- `id`: UUID do cliente

#### Exemplo de Resposta
```json
{
  "id": "123e4567-e89b-12d3-a456-426614174000",
  "name": "Maria Aparecida Silva",
  "email": "maria.silva@exemplo.com",
  "city": "São Paulo"
}
```

### 5. Consulta de Cliente por Nome
- **Endpoint:** `GET /api/Clients/{name}`
- **Descrição:** Busca clientes pelo nome

#### Parâmetros de Rota
- `name`: Nome do cliente (ou parte do nome)

#### Exemplo de Resposta
```json
{
  "clients": [
    {
      "id": "123e4567-e89b-12d3-a456-426614174000",
      "name": "Maria Aparecida Silva",
      "email": "maria.silva@exemplo.com",
      "city": "São Paulo"
    }
    // ... outros clientes com nome similar
  ]
}
```

#### Respostas Possíveis
status | descrição
------------ | -------------
**200 OK** | Cliente cadastrado com sucesso
**500 Internal Server Error**  | Erro no servidor

---

### 6. Exclusão de Cliente
- **Endpoint:** `DELETE /api/Clients/{id}`
- **Descrição:** Remove um cliente do sistema

#### Parâmetros de Rota
- `id`: UUID do cliente a ser excluído

#### Respostas Possíveis
status | descrição
------------ | -------------
**200 OK** | Cliente excluído com sucesso
**400 Bad Request** | Erro na exclusão
**404 Not Found** | Cliente não encontrado
**500 Internal Server Error**  | Erro no servidor

---

### 7. Upload de Arquivo para Cliente
- **Endpoint:** `POST /api/Clients/file`
- **Descrição:** Permite associar um arquivo a um cliente

#### Formato da Requisição
- Multipart Form Data
  - `Id`: UUID do cliente
  - `File`: Arquivo a ser enviado

#### Respostas Possíveis
status | descrição
------------ | -------------
**200 OK** | Cliente cadastrado com sucesso
**500 Internal Server Error**  | Erro no servidor

---

### 8. Exclusão de Arquivo de Cliente
- **Endpoint:** `DELETE /api/Clients/file/{id}`
- **Descrição:** Remove arquivo associado a um cliente

#### Parâmetros de Rota
- `id`: UUID do arquivo a ser excluído

#### Respostas Possíveis
status | descrição
------------ | -------------
**200 OK** | Cliente cadastrado com sucesso
**500 Internal Server Error**  | Erro no servidor

---

## Regras de Negócio
- Cada cliente deve ter um e-mail único
- Não é permitido cadastrar cliente com dados incompletos
- Clientes excluídos não podem ter vendas ativas

## Boas Práticas
- Mantenha os dados dos clientes atualizados
- Utilize validação de e-mail
- Gerencie permissões de acesso

## Considerações de Segurança
- Todas as operações requerem autenticação
- Somente usuários autorizados podem manipular dados de clientes
  

## 📦 Módulo de Produtos

  

### Cadastro de Produto

-  **Endpoint:**  `POST /api/Products`

-  **Descrição:** Adiciona novos produtos ao catálogo

  

#### Campos Obrigatórios

-  `description`: Descrição do produto

-  `unitaryValue`: Valor unitário (mínimo R$ 0,01)

  

#### Exemplo de Requisição

```json

{

"description": "Smartphone Samsung Galaxy A54",

"unitaryValue": 2499.90

}

```

  

### Upload de Imagem do Produto

-  **Endpoint:**  `POST /api/Products/file`

-  **Descrição:** Permite associar imagem a um produto

-  **Formato:** Multipart Form Data

-  `Id`: ID do produto

-  `File`: Arquivo de imagem

  

## 💰 Módulo de Vendas

  

### Registro de Venda

-  **Endpoint:**  `POST /api/Sales`

-  **Descrição:** Registra uma nova venda no sistema

  

#### Campos Obrigatórios

-  `clientId`: ID do cliente

-  `productId`: ID do produto

-  `productQuantity`: Quantidade vendida

-  `userId`: ID do usuário realizando a venda

  

#### Exemplo de Requisição

```json

{

"clientId": "123e4567-e89b-12d3-a456-426614174000",

"productId": "223e4567-e89b-12d3-a456-426614174000",

"productQuantity": 2,

"userId": "323e4567-e89b-12d3-a456-426614174000"

}

```

  

## 🔒 Segurança e Autenticação

  

### Autenticação JWT

- Todas as rotas, exceto login e registro, requerem token

- Token deve ser enviado no cabeçalho:

```

Authorization: Bearer <token>

```

  

### Funções de Usuário

-  `1`: Administrador (acesso total)

-  `2`: Usuário Padrão (acesso limitado)

  

## 📋 Considerações Finais

  

### Códigos de Resposta

-  `200 OK`: Sucesso na operação

-  `400 Bad Request`: Erro de validação

-  `401 Unauthorized`: Falha na autenticação

-  `403 Forbidden`: Sem permissão

-  `404 Not Found`: Recurso não encontrado

  

### Boas Práticas

- Sempre valide dados antes do envio

- Mantenha o token de autenticação seguro

- Utilize HTTPS em todas as comunicações

- Renove o token periodicamente

  

## Suporte

Para dúvidas ou suporte, entre em contato com nossa equipe técnica.