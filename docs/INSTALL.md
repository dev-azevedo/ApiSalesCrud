# 📚 Documentação da API - ⚙️ Instalação

Explore a documentação:

🏁 [Introdução (README.md)](../README.md)  | ⚙️ [Instalação](INSTALL.md) | 📓 [Guia de Uso](USAGE.md) | 🖥️ [API](API.md)

### Nesta seção, você encontrará todas as informações necessárias para configurar e rodar o projeto em seu ambiente local.

---

## 🔍 O que usei para desenvolver este projeto?

- [.Net 8](https://dotnet.microsoft.com/pt-br/download/dotnet/8.0)

- [EntityFrameworkCore](https://www-1.nuget.org/packages/Microsoft.EntityFrameworkCore.SqlServer/8.0.0)

- [Sqlite](https://sqlitebrowser.org/)

- [AutoMapper](https://automapper.org/)

- [JWT](https://jwt.io/)



## ⚙ Clone o repositório
Para clonar e rodar este projeto, você precisará instalar [Git](https://git-scm.com/downloads) e [.Net SDK](https://dotnet.microsoft.com/pt-br/download/dotnet/8.0) no seu computador.
Após execute os seguintes comando no terminal:

```bash
  git clone https://github.com/dev-azevedo/api-sales-crud.git
```

Acesso o projeto

```bash
  cd api-sales-crud
```

Atualize seu banco de dados com as migrations

```bash
dotnet ef database update
```

Rodando a aplicação

```bash
dotnet run
```