# Agora

Rede social desktop para hobbies nerd, desenvolvida em C#. O Agora foi pensado para programadores, gamers de RPG e leitores que querem compartilhar conteúdo em uma plataforma leve, cronológica e orientada por interesses.

## Estado do projeto

O projeto está na **Fase 1 (Fechamento)**: a fundação documental está concluída e a conexão com o banco PostgreSQL em nuvem já foi implementada e validada. Ainda não há entidades nem schema — as migrations entram na branch de cadastro.

O MVP (Fase 1) terá:

- cadastro, autenticação, recuperação de senha e perfil editável;
- posts em Markdown com blocos de código e syntax highlighting;
- feed cronológico paginado, incluindo uma visão por tags populares;
- seguir usuários, curtidas com toggle e comentários;
- busca por usuários, posts e tags;
- tags categorizadas para organizar e descobrir conteúdo;
- exclusão de conta e exportação de dados (LGPD);
- splash screen animada e empacotamento do aplicativo.

## Stack definida

- **Cliente:** Avalonia UI
- **Runtime:** .NET 10 LTS e C#
- **Arquitetura de apresentação:** MVVM com CommunityToolkit.Mvvm
- **Servidor:** ASP.NET Core Web API
- **Autenticação:** JWT Bearer
- **Persistência:** Entity Framework Core 10, com SQLite no cliente (configs, rascunho e cache) e **PostgreSQL (Npgsql)** no servidor
- **Implantação:** cliente-servidor, com ambientes separados de desenvolvimento, staging e produção

Arquitetura prevista: `UI -> Aplicação -> Domínio <- Infra`, mantendo as dependências apontadas para o núcleo do domínio.

## Documentação

A documentação completa está em [Agora.Docs](Agora.Docs/Home.md) e foi criada para ser visualizada no **Obsidian**, que interpreta corretamente os links internos, o frontmatter e os diagramas da documentação. Os principais pontos de entrada são:

- [Resumo dos requisitos](Agora.Docs/Resumo%20dos%20Requisitos.md)
- [Visão do produto](Agora.Docs/01%20Requisitos/Visão%20do%20Produto.md)
- [Definição do MVP](Agora.Docs/04%20Gestão/Definição%20do%20MVP.md)
- [Arquitetura do sistema](Agora.Docs/02%20Modelagem/Arquitetura%20do%20Sistema.md)
- [Backlog do produto](Agora.Docs/04%20Gestão/Backlog%20do%20Produto.md)
- [Decisões arquiteturais (ADRs)](Agora.Docs/03%20Decisões/)

As instruções para manutenção da documentação e do repositório estão em [Agora.Docs/AGENTS.md](Agora.Docs/AGENTS.md).

## Desenvolvimento

```powershell
dotnet build
dotnet test
dotnet run --project src\Agora.Api
```

Com a API no ar, o health check confirma o banco em tempo real:

```powershell
curl http://localhost:5000/health
# {"status":"online","database":"ok"}
```

### Conexão com o banco

A configuração fica em `src/Agora.Api/appsettings.json`, na seção `DatabaseCloud`/`DatabaseLocal`:

```json
  "DatabaseCloud": {
    "Host": "ep-dawn-pine-b6ps76km-pooler.c-2.sa-east-1.aws.neon.tech",
    "Port": 5432,
    "Database": "neondb",
    "Username": "neondb_owner",
    "Password": "",
    "SslMode": "Require",
    "MaximumPoolSize": 10
  },
  "DatabaseLocal": {
    "Host": "localhost",
    "Port": 5432,
    "Database": "agora",
    "Username": "agora",
    "Password": "",
    "SslMode": "Disable",
    "MaximumPoolSize": 10
  }
```

A senha **não** fica versionada. Grave a sua localmente com user-secrets (o valor sobrescreve o `appsettings.json`):

```powershell
dotnet user-secrets set "DatabaseCloud:Password" "<sua senha>" --project src\Agora.Api
dotnet user-secrets set "DatabaseLocal:Password" "<sua senha>" --project src\Agora.Api
```

O `dotnet test` lê a mesma configuração da API, então esse é o único passo necessário: os testes passam a conectar no banco de verdade. Para conferir o que está gravado:

```powershell
dotnet user-secrets list --project src\Agora.Api
```

> A senha pode ser obtida em [console.neon.tech](https://console.neon.tech), no projeto do Agora.
>
> Atenção: o console entrega a conexão no formato URI (`postgresql://...`). O Npgsql **não** aceita esse formato — use os campos discretos da seção `DatabaseCloud` ou `DatabaseLocal`, que a aplicação converte para o formato keyword/value.

Metas importantes do MVP incluem feed com carregamento de até 2 s no P95, feedback de ações em até 200 ms, cobertura mínima de 60% no núcleo de domínio e pipeline de CI com build e testes.

## Equipe

Projeto acadêmico da UNA - Contagem, na disciplina de Garantia da Qualidade de Software.

- Athur Marquez Diniz
- Bernardo Luiz Monteverde Gonçalves
- Luiz Filipe Pimenta Correa
- Patrick Oliveira Rabelo de Brito

## Licença

Este projeto está disponível sob a [licença MIT](LICENCE).
