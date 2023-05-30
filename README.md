# CRUD_asp_net_core
## Sobre a aplicação
O projeto se trata de um CRUD simples de produtos e categorias usando migrations, relação entre as tabelas e EntityFramework.

## Como executar a aplicação
Crie um arquivo `.env` no mesmo diretório do arquivo `Program.cs` com os seguintes atributos:

```bash
APP_SERVERNAME=
APP_PORT=
APP_USERNAME=
APP_PASSWORD=
APP_DATABASE=

JWT_KEY=
AUDIENCE_TOKEN=
ISSUER_TOKEN=
EXPIRATION_TOKEN=
```

preencha todos os campos logo após o sinal de igualdade, sem espaços. Para esse projeto foi usado o banco `PostgreSQL`. O `EXPIRATION_TOKEN` está em horas.
