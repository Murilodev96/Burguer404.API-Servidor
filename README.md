API desenvolvida em .NET 8 com arquitetura hexagonal, Razor Pages e SQL Server para gerenciamento completo de uma hamburgueria. O sistema controla pedidos feitos por usuários cadastrados ou não, além de gerenciar produtos, clientes e pagamentos via QR Code.

Tecnologias utilizadas:

- ASP.NET Core 8 (Web API)
- Razor Pages (html, css, js)
- Entity Framework Core
- SQL Server
- Identity para autenticação
- Swagger
- Migrations
- Ajax (comunicação entre Frontend e Backend)
- Docker
- Testes unitários em xUnit



Principais funcionalidades:

- Cadastro e consulta de usuários
- Gestão de produtos
- Gestão de pedidos
- Realização de pedidos (anônimos ou autenticados)
- Geração de QR Code para pagamento (integração com mercado pago)
- Interface administrativa via Razor Pages



Estrutura do projeto:

Projeto feito na arquitetura hexagonal.
- Adapters
  - Entrada
    - Burguer404.Api (Web Api do projeto, gerencia as entradas e saídas das chamadas dos endpoints)
     Burguer404.Api.Testes (testes de unidade da Web Api do projeto)
  - Saida
    - Burguer404.Infrastructure.Data (Gerencia a entrada e saída de dados do projeto)
     Burguer404.Infrastructure.Pagamentos (Gerencia integrações externas relacionadas a pagamentos (mercado pago))
- Core
  - Burguer404.Application (Controla o fluxo do projeto entre api, domain, infrastructure)
   Burguer404.Application.Testes (Testes de unidade da camada de Application)
   Burguer404.Domain (Regras de negócio da aplicação, portas de integração de relacionamento entre as camadas (interfaces), entidades, enums, métodos estáticos de utilidade geral, classes de entrada e saída dos endpoints e validações)
   Burguer404.Configurations (Configurações da injeção de dependência e frameworks aplicados no projeto)
- Frontend
  - Burguer404.Frontend (Interface entre aplicação X usuário)



Pré requisitos para execução do projeto:

- .Net SDK 8
- SQL Server (necessário o SQL Server Configuration Manager para criação da instância no sql, IDE para consulta do banco pode ser a de escolha)
- Docker (caso queira rodar no container, para executar localmente é necessário apenas os dois acima)



Execução do projeto: 

- Execução com o docker compose:
    - Necessário estar com o docker desktop aberto.
    - Na raiz do projeto executar o comando "docker compose up --build"
    - O projeto será executado locamente pelo docker nas portas 5000 (Api), 5001 (Frontend), 1433 (Sql Server)


- Execução local (Ex: pelo Visual Studio):
    - No Sql Server Configuration Manager, fazer os passos abaixo:
        - Acessar configuração de rede do Sql Server
        - Acessar protocolos para MSSQLSERVER
        - Habilitar a opção TCP/IP
        - Reiniciar o serviço do Sql Server (MSSQLSERVER)
    - Abrir o projeto no VS e seguir os passos abaixo
        - Alterar no appsettings e appsettings.development a informação da connectionString para: 
            "ConnectionStrings": {
              "DefaultConnection": "Server=localhost;Database=Burguer404;User Id=burguer;Password=Burguer404@2025;TrustServerCertificate=True;"
            },
        - Abrir o console de gerenciador de pacotes e alterar o projeto padrão para "Burguer404.Infrastructure.Data"
        - Definir inicialmente somente o backend como projeto de inicialização
        - Executar no console o comando "update-database" (as migrations serão executas)
    - Após execução das migrations, ir até uma IDE para acessarmos o banco de dados (Ex: Dbeaver)
        - Com a connectionstring acima, criar uma nova conexão com o banco de dados
        - Caso queira criar um usuário específico para controle de acesso ao banco, executar na IDE os passos abaixo
            - Abrir um novo script sql no banco Burguer404 e executar os scripts abaixo em ordem
            - CREATE LOGIN burguer WITH PASSWORD = 'Burguer404@2025'; -- (essa senha é apenas um exemplo)
            - CREATE USER burguer FOR LOGIN burguer;
            - ALTER SERVER ROLE sysadmin ADD MEMBER burguer;
            - EXEC xp_instance_regwriteN'HKEY_LOCAL_MACHINE',N'Software\Microsoft\MSSQLServer\MSSQLServer',N'LoginMode',REG_DWORD,2;
            - Após execução destes scripts, reiniciar a instância do MSSQLSERVER no Sql Server Configuration Manager
            - Após reiniciar, alterar conexão com o banco Burguer404 para autenticação de usuário e senha 
            - Inserir usuário e senha criados nos scripts acima
            - No VS, alterar no appsettings e appsettings.development a informação da connectionString para: 
              "ConnectionStrings": {
                "DefaultConnection": "Server=localhost;Database=Burguer404;User Id=burguer;Password=Burguer404@2025;TrustServerCertificate=True;"
              },
    - Agora a API está pronto para ser executado localmente
      - Caso queria executar o frontend junto da API, alterar os projetos de inicialização para "vários projetos de inicialização"
      - Definir como projetos de inicialização a api (Burguer404.Api) e o frontend (Burguer404.Frontend)
      - Aplicar e salvar
