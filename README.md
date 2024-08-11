# Teste Técnico Viceri-Seidor

## Instruções para inicializar os projetos

*BACKEND*
- Primeiro você deve rodar uma migration para carregar as tabelas.
- Para isso você precisa abrir o projeto e navegar até a pasta do projeto API (SuperHeroes.API)
- Rodar o seguinte comando `dotnet ef database update --no-build`

PS: A string de conexão está configurada para um container docker com a imagem do MSSQL 2019, estarei disponibilizando o docker-compose caso você não possua o banco MSSQL no computador. 
PS²: Pode ser que pelo terminal com o comando acima não funcione, caso isso ocorra tente pelo `Console do Gerenciador de pacotes` do próprio visual studio utilizando o comando `update-database`

OBS: Caso sinta dificuldade em fazer a migration estarei disponibilizando um arquivo(QueriesDB.txt) contendo queries para montar as tabelas do banco e já popular a tabela de superpoderes com alguns dados, basta rodar as queries no SSMS.
Para rodar o docker-compose basta ter o docker instalado no computador e rodar o comnado docker-compose up

*FRONTEND*
- Abrir a pasta do projeto frontend
- Rodar `npm i`
- Caso não possua a CLI do angular rode o comando `npm i @angular/cli@16.2.14`
- Rode o comando ng s
- A aplicação irá iniciar na seguinte url => http://localhost:4200

## INFORMAÇÕES ADICIONAIS

- O projeto está configurado para rodar utilizando EF core conectado ao MSSQL
- A aplicação .Net está configurada para no seguinte url => http://localhost:6037
- A aplicação Angular foi desenvolvida na versão 16
- A aplicação Angular está apontando para o backend utilizando essa url => http://localhost:6037
- O swagger está na url => http://localhost:6037/swagger/index.html
