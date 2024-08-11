# Teste Técnico Viceri-Seidor
*Sobre a aplicação*
- Optei por desenvolver o backend utilizando arquitetura limpa, imagino que para uma vaga de júnior conhecimentos de arquitetura são um plus.
- Desenvolvi também um HUB websocket utilizando SignalR, decidi criar o HUB para que a tabela de Heróis ficasse mais flúida conforme era realizado algumas ações, ex: Editar Herói, excluir Herói ou adicionar novo Herói.
- Em relação ao EF core, optei por utilizar ele conectado ao banco SQLServer, fiz essa escolha porque armazenamento em cache não é seguro, e já que precisava persistir, porque não em um banco robusto como o SQLServer?
- Sobre a aplicação Angular, utilizei Angular Material somado ao Bootstrap para facilitar e ganhar um boost no desenvolvimento das interfaces e componentes.
- E por fim, para manter a aplicação ainda mais robusta implementei o design Pattern `UnitOfWork` para manter o banco o mais seguro o possivel em situações que era preciso manipular muitos dados ou multiplas tabelas.
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
