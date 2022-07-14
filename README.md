# PVD Totvs

## Execução
Para executar o software, basta executar o comando.
```shell
docker compose up --build
```
Após isso, basta acessar o swagger na URL
```
http://localhost:8000/swagger/index.html
```

## Testes

Para testar o projeto, basta executar o comando.
```shell
dotnet test
```

## Requisitos

- [X] .NET Core
- [X] EntityFrameworkCore
- [X] ORM Dapper
- [X] Testes Unitários
- [X] Docker
- [ ] Transação em banco noSQL
- [X] Swagger

## Decisões

### Tipo de dado da moeda
Para armazenar os valores monetários no banco de dados, foi usado o tipo de dado *bigint*. Ele foi escolhido por uma questão de performance e precisão. Como na tarefa nós usamos apenas duas casas decimais, é necessário que a entrada seja multiplicada por 100 ao ser armazenada e dividiva por 100 ao ser exibida para o usuário. Caso seja necessário números maiores ou com mais casas decimais, eu usaria o *numeric*.

### DTOs
O DTO foi divido em 2, *EntityRequest* e *EntityResponse*.   
Como o nome já diz, um é usado apenas para request, enquanto o outro é usado para response. No primeiro momento eu cheguei a usar o [AutoMapper](https://automapper.org/) para facilitar essa conversão, mas como a classe do projeto era pequena, acabei removendo. 

### Migração
Assim como o Spring, o .NET, a partir do EntityFrameworkCore consegue criar a primeira versão do banco de dados, não é recomendado em produção, mas bem últil em desenvolvimento. Acabei optando em adicionar isso no *Program.cs*, ao invés de criar migrações.

## Problemas

### Transação em banco noSQL
Apesar de estar listado como um requisito adicional e não obrigatório, não encontrei uma forma viável de usar um banco de dados noSQL no projeto, já que nós temos o Postgres como banco principal. O único uso seria para cache de alguma rota.
