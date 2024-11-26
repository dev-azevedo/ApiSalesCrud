# 📚 Documentação da API

Explore a documentação:

🏁 [Introdução (README.md)](../README.md)  | ⚙️ [Instalação](INSTALL.md) | 📓 [Guia de Uso](USAGE.md) | 🖥️ [API](API.md)


## Bem-vindo à documentação detalhada da API. Aqui você encontrará informações sobre os endpoints disponíveis, seus parâmetros, respostas e exemplos de uso.
#### Caso tenha alguma dúvida sobre o funcionamento do projeto, acesse o [Guia de Uso](USAGE.md) para mais informações.

---
## 🖥️ API

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

---

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

---

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
---

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
---

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
---

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
---

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
---

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