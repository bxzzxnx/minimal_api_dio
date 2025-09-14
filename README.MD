# MinimalAPI - Gerenciamento de Veículos

##  Descrição
API desenvolvida em .NET 9.0 utilizando a abordagem Minimal APIs para gerenciamento de veículos e administradores, baseada no bootcamp da DIO.

Repositório original: https://github.com/digitalinnovationone/minimal-api/

## Melhorias que eu adicionei
- Containerização com Docker para o banco de dados
- Implementação de BCrypt para hash e verificação de senhas
- Refatoração do código com melhor organização de serviços
- Separação da regra de negócio das rotas
- Testes unitários usando Moq para simulação de serviços
- Implementação de InMemoryDatabase para testes de integração

## Tecnologias Utilizadas
- .NET 9.0
- Entity Framework Core
- MySQL 
- BCrypt.NET
- JWT Authentication
- MSTest e Moq (para testes)
- Docker

##  Estrutura do Projeto

```
MinimalApi/
├── src/                     
│   ├── Application/         
│   │   ├── Services/       
│   │   └── Validators/     
│   ├── Domain/             
│   │   ├── DTO/           
│   │   ├── Entities/      
│   │   ├── Enums/         
│   │   └── Interfaces/    
│   ├── Extensions/
│   │   ├── Endpoints 
│   │   └── Services      
│   ├── Migrations/       
│   └── Infra/            
│       ├── Data/         
│       └── Security/     
└── tests/                  
    ├── Application/      
    ├── Domain/           
    └── Integration/      
```

##  Endpoints

### Documentação
- `GET api/swagger` - Swagger

### Administradores
- `GET api/admin/admins` - Listar todos administradores
- `GET api/admin/{id}` - Buscar administrador por ID
- `POST api/admin/register` - Cadastro de administrador
- `POST api/admin/login` - Login de administrador

### Veículos


- `GET api/vehicle/` - Listar todos os veículos
- `POST api/vehicle/register` - Cadastro de veículo
- `GET api/vehicle/{id}` - Buscar veículo  por ID
- `PUT api/vehicle/{id}` - Atualizar veículo
- `DELETE api/vehicle/{id}` - Remover veículo

## Veículos padrão
```json
{
  "Id": 1,
  "Brand": "Test Brand",
  "Model": "Test Model",
  "Year": 2018,
}
```

##  Usuários Padrão

O sistema já vem com dois usuários pré-configurados:

### Administrador
```json
{
    "email": "admin@admin.com",
    "password": "admin"
}
```

### Editor
```json
{
    "email": "ed@ed.com",
    "password": "editor"
}
```

Acesso via: `http://localhost:5030/api/admin/login`

##  Configuração do Banco de Dados
- Por padrão o banco de dados vai rodar com essas credenciais, se desejar mudar, configure os seguintes arquivos `docker-compose.yml`
```yml
services:
  db:
    image: mysql
    restart: always
    environment:
      - MYSQL_USER=user
      - MYSQL_PASSWORD=secretpass
      - MYSQL_DATABASE=minimalapidb
      - MYSQL_ROOT_PASSWORD=rootpass
    ports:
      - "3306:3306"
    volumes:
      - db-volume:/var/lib/mysql
volumes:
  db-volume:
```
- E no arquivo `appsettings.Development.json`
```json
"ConnectionStrings": {
    "db": "Server=localhost;Port=3306;Database=minimalapi;User=user;Password=secretpass;"
}
```

##  Iniciando o Projeto

1. Clone o repositório:
```bash
git clone https://github.com/bxzzxnx/minimal_api_dio
cd minimal_api_dio
```

2. Restaure as dependências:
```bash
dotnet restore
```
3. Inicie o container do banco de dados:
```bash
docker-compose up -d
```

4. Navegue até a pasta e execute as migrações:
```bash
cd src/api
dotnet ef database update
```

5. Execute o projeto:
```bash
dotnet run
```

## Testes
Para executar os testes rode
```bash
cd minimal_api_dio/Tests
dotnet test
```


