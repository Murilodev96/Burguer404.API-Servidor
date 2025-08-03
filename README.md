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
- Kubernets



Principais funcionalidades:

- Cadastro e consulta de usuários
- Gestão de produtos
- Gestão de pedidos
- Realização de pedidos (anônimos ou autenticados)
- Geração de QR Code para pagamento (integração com mercado pago)
- Consulta de pagamentos (integração com mercado pago)
- Interface administrativa via Razor Pages
- Webhook com fluxo mercado pago



Estrutura do projeto:

Projeto feito na Arquitetura Limpa.
- Backend
    - Burguer404.Api (Web Api do projeto, responsável por receber as chamadas e direcionar para as controllers)
    - Burguer404.Api.Testes (Testes de unidade da Web Api e UseCases do projeto)
    - Burguer404.Application (Controla o fluxo do projeto entre as camadas de Api(Handlers), Gateway, Presenters e UseCases)
    - Burguer404.Configurations (Configurações da injeção de dependência e frameworks aplicados no projeto)
    - Burguer404.Domain (Regras de negócio da aplicação, portas de integração de relacionamento entre as camadas (interfaces), entidades, enums, métodos estáticos de utilidade geral, classes de entrada, saída dos endpoints e validações)
    - Burguer404.Infrastructure.Data (Gerencia a entrada e saída de dados do projeto)
    - Burguer404.Infrastructure.Pagamentos (Gerencia integrações externas relacionadas a pagamentos (mercado pago))
    - * Burguer404.Application.Testes (Testes de unidade da camada de Application)    
- Frontend
  - Burguer404.Frontend (Interface entre aplicação X usuário)
- k8s
  - Localizado todos os arquivos relacionados ao Kubernets  



Pré requisitos para execução do projeto:

- .Net SDK 8
- SQL Server (necessário o SQL Server Configuration Manager para criação da instância no sql, IDE para consulta do banco pode ser a de escolha)
- Docker (caso queira rodar no container, para executar localmente é necessário apenas os dois acima)
- Kubernets
- Ngrok (ou outra ferramenta de exposição de servidores locais)
- Conta criada no Docker Hub
- "Opcional" K6 (Utilizado para testes de carga e desempenho de aplicações web e APIs)
- "Opcional" Chocolatey (Gerenciador de pacotes do Windowns)


Execução do projeto: 

- Execução com Docker Compose:
    - Necessário estar com o docker desktop aberto.
    - Na raiz do projeto executar o comando "docker compose up --build"
    - O projeto será executado locamente pelo docker nas portas 5000 (Api), 5001 (Frontend), 1433 (Sql Server)
 
- Execução com Kubernets:
    - Abrir o prompt de comando
    - Acessar a pasta raiz do projeto
      - cd caminhoDaPasta"
    - Efetuar autenticação no docker hub
      - Docker Login
    - Buildar o projeto do backend localmente utilizando as configurações do Docker Hub
      - docker build -f ./Burguer404.Api/Dockerfile -t murilodev96/burguer404api:latest .
    - Efetuar o push do build backend para o Docker Hub
      - docker push murilodev96/burguer404api:latest
    - Buildar o projeto do frontend localmente utilizando as configurações do docker hub
      - docker build -f ./Burguer404.Frontend/Dockerfile -t murilodev96/burguer404front:latest .
    - Efetuar o push do build backend para o Docker Hub
      - docker pull murilodev96/burguer404front:latest
    - Aplicar o arquivo de Configuração
      - kubectl apply -f configmap.yaml
    - Aplicar o arquivo de Secrets
      - kubectl apply -f secret.yaml
    - Aplicar o arquivo de Metricas para liberar os permissionamentos
      - kubectl apply -f metrics.yaml
    - Aplicar os arquivos de HPA (Horizontal Pod Autoscaler)
      - kubectl apply -f hpa-backend.yaml
      - kubectl apply -f hpa-frontend.yaml
      - kubectl apply -f hpa-db.yaml
    - Aplicar os arquivos de Deployment
      - kubectl apply -f frontend-deployment.yaml
      - kubectl apply -f db-deployment.yaml
      - kubectl apply -f backend-deployment.yaml
    - Aplicar os arquivos de Service
      - kubectl apply -f frontend-service.yaml
      - kubectl apply -f db-service.yaml
      - kubectl apply -f backend-service.yaml
    - Executar comando para avaliar status das aplicações
      - kubectl get pod,svc
    - O projeto estára disponivel localmente via navegador através dos links e portas abaixo:
      - Backend
        - http://localhost:30081/swagger/index.html
      - Frontend
        - http://localhost:30080     


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




   
