---
tags: [modelagem, banco-de-dados, er]
tipo: documento
status: rascunho
atualizado: 2026-08-29
---

# Modelo de Dados (ER)

> [!info] Relação com o domínio
> Derivado de [[02 Modelagem/Modelo de Domínio]]. Nomes de tabelas em `snake_case`; PK/FK explícitas. SGBD: **SQLite** no client (configs locais, rascunho RNF-15, cache — [[03 Decisões/ADR-003 Persistência|ADR-003]]) / **PostgreSQL via Npgsql** no servidor ([[03 Decisões/ADR-007 Banco do Servidor (Npgsql)|ADR-007]]). Inclui suporte a OAuth (RF-024, RF-025).

## 1. MER — Modelo Entidade-Relacionamento (conceitual)

> Modelo conceitual: entidades, atributos e relacionamentos, sem detalhes físicos de implementação. A derivação das tabelas (índices, constraints, migrações) está na seção **DER** abaixo.

### 1.1 Núcleo — Fase 1 (MVP)

```mermaid
erDiagram
    USUARIO ||--|| PERFIL : "1:1"
    USUARIO ||--o{ POST : "autor"
    USUARIO ||--o{ COMENTARIO : "escreve"
    USUARIO ||--o{ CURTIDA : "registra"
    POST   ||--o{ COMENTARIO : "recebe"
    POST   ||--o{ CURTIDA : "recebe"
    USUARIO ||--o{ SEGUIDA : "segue"
    USUARIO ||--o{ NOTIFICACAO : "recebe"
    POST   ||--o{ POST_TAG : "tem"
    TAG    ||--o{ POST_TAG : "classifica"

    USUARIO {
        uuid id PK
        varchar email UK "único - RN-03"
        varchar apelido UK "único - RN-03"
        varchar nome_exibicao
        bytea hash_senha "nullable - RN-10 (OAuth não tem senha)"
        varchar provider "local|github|google"
        varchar provider_id "nullable - ID do provedor OAuth"
        bool ativo
        timestamptz criado_em
    }
    PERFIL {
        uuid usuario_id PK, FK
        text bio
        varchar avatar_url
    }
    POST {
        uuid id PK
        uuid autor_id FK
        varchar conteudo "≤5000 chars - RN-06"
        varchar status "rascunho|publicado|editado|arquivado|excluido"
        timestamptz publicado_em
        timestamptz editado_em
    }
    COMENTARIO {
        uuid id PK
        uuid post_id FK
        uuid autor_id FK
        text texto
        timestamptz criado_em
    }
    CURTIDA {
        uuid usuario_id PK, FK
        uuid post_id PK, FK
        timestamptz criado_em
    }
    SEGUIDA {
        uuid seguidor_id PK, FK
        uuid seguido_id PK, FK
        timestamptz desde
    }
    NOTIFICACAO {
        uuid id PK
        uuid usuario_destino FK
        varchar tipo "curtida|comentario|seguidor"
        uuid referencia_id "id do post/usuário origem"
        bool lida
        timestamptz criado_em
    }
    TAG {
        uuid id PK
        varchar nome UK "único - RN-08"
        varchar slug UK "auto-gerado"
        varchar categoria "linguagem|tema|genero|sistema"
        int usos_count
    }
    POST_TAG {
        uuid post_id PK, FK
        uuid tag_id PK, FK
    }
```

### 1.2 Módulos futuros — Fases 2 e 3

```mermaid
erDiagram
    USUARIO ||--o{ SEGUE_TAG : "segue"
    TAG    ||--o{ SEGUE_TAG : "seguida por"
    USUARIO ||--o{ USUARIO_FLAIR : "possui"
    FLAIR  ||--o{ USUARIO_FLAIR : "atribuída a"
    USUARIO ||--o{ USUARIO_LIVRO : "registra"
    LIVRO  ||--o{ USUARIO_LIVRO : "na biblioteca de"
    USUARIO ||--o{ USUARIO_JOGO : "joga"
    JOGO   ||--o{ USUARIO_JOGO : "jogado por"
    USUARIO ||--o{ REPOSITORIO : "possui"
    USUARIO ||--o{ CAMPANHA : "narra"
    CAMPANHA ||--o{ MESA : "tem"
    MESA   ||--o{ SESSAO : "tem"
    USUARIO ||--o{ FICHA : "interpreta"
    CAMPANHA ||--o{ FICHA : "contém"
    MESA   ||--o{ FICHA : "joga na"

    SEGUE_TAG {
        uuid usuario_id PK, FK
        uuid tag_id PK, FK
        timestamptz desde "RF-019"
    }
    FLAIR {
        uuid id PK
        varchar nome UK "ex: C# Dev, Mestre D&D, Leitor - RF-020"
        varchar icone "nome do ícone/badge"
        varchar cor "cor do badge"
    }
    USUARIO_FLAIR {
        uuid usuario_id PK, FK
        uuid flair_id PK, FK
    }
    LIVRO {
        uuid id PK
        varchar titulo
        varchar autor
        varchar genero "nullable"
        varchar isbn UK "nullable - exclusão de duplicatas"
        varchar capa_url "nullable"
    }
    USUARIO_LIVRO {
        uuid usuario_id PK, FK
        uuid livro_id PK, FK
        varchar status "lido|lendo|queroler"
        int nota "0-5 - nullable"
        text resenha "nullable"
        timestamptz criado_em
    }
    JOGO {
        uuid id PK
        varchar titulo
        varchar genero "nullable"
        varchar desenvolvedor "nullable"
        varchar capa_url "nullable"
    }
    USUARIO_JOGO {
        uuid usuario_id PK, FK
        uuid jogo_id PK, FK
        numeric horas_jogadas
        text review "nullable"
        varchar plataforma "ex: PC, Xbox, PS5"
        timestamptz criado_em
    }
    REPOSITORIO {
        uuid id PK
        uuid usuario_id FK "dono do perfil integrado"
        varchar nome
        varchar url UK "URL no GitHub"
        text descricao "nullable"
        varchar linguagem_principal "nullable"
        timestamptz atualizado_em "sync com GitHub - RF-028"
    }
    CAMPANHA {
        uuid id PK
        uuid dono_id FK "narrador/mestre"
        varchar nome
        varchar sistema "ex: D&D 5e, Vampiro"
        text descricao "nullable"
        timestamptz criado_em
    }
    MESA {
        uuid id PK
        uuid campanha_id FK
        varchar nome
        text descricao "nullable"
    }
    SESSAO {
        uuid id PK
        uuid mesa_id FK
        varchar titulo "nullable"
        text resumo "nullable"
        bool one_shot "sessão avulsa - RF-030"
        timestamptz realizada_em
    }
    FICHA {
        uuid id PK
        uuid usuario_id FK "jogador"
        uuid campanha_id FK
        uuid mesa_id FK "nullable - mesa atual"
        varchar nome_personagem
        text atributos "estrutura aberta"
        text inventario "nullable"
    }
```

> [!note] Extensão do PERFIL (Fase 2 — RF-021)
> `PERFIL` ganha campos opcionais (opt-in, D-5): `stack_tech` (ex: "C#, .NET, Avalonia"), `jogos_favoritos` e `autores_favoritos` — conteúdo livre, sem tabelas novas. Flair/lista de livros/jogos vêm das relações F3 acima.

> [!note] Convenção
> Diagrama ER via Mermaid `erDiagram`. `||--||` = 1:1, `||--o{` = 1:N, `}o--o{` = N:N. PK/FK explícitas; `UK` = unique key. Constraints detalhados na seção **DER** (2.2).

## 2. DER — Diagrama Entidade-Relacionamento (físico)

> Detalhamento de implementação do **MER** acima: índices, constraints e critérios de migração sobre o SGBD (SQLite client / PostgreSQL server).

### 2.1 Índices essenciais

| Tabela | Índice | Motivo |
|---|---|---|
| USUARIO | `(email)` | Login por e-mail (RF-002) |
| USUARIO | `(apelido)` | Busca por @ (RF-010) |
| USUARIO | `(provider, provider_id)` | Login OAuth — busca por provedor (RF-024) |
| POST | `(autor_id, status, publicado_em DESC)` | Feed e perfil |
| POST | `GIN/conteudo` (tsvector) | RF-010 busca textual — FTS Postgres (ADR-007) · cache local usa LIKE |
| COMENTARIO | `(post_id, criado_em)` | Listar comentários de um post em ordem cronológica |
| CURTIDA | já coberta pela PK composta | RN-02 |
| SEGUIDA | `(seguido_id)` adicional | Listar seguidores |
| NOTIFICACAO | `(usuario_destino, lida, criado_em DESC)` | Badge não lidas |
| TAG | `(categoria)` | Busca por categoria |
| POST_TAG | já coberta pela PK composta | RN-09 |
| SEGUE_TAG | `(tag_id)` | Feed por interesses (RF-018 F2) |
| USUARIO_FLAIR | `(flair_id)` | Listar usuários de um badge |
| USUARIO_LIVRO | `(livro_id)`, `(status)` | Biblioteca e filtros por status |
| USUARIO_JOGO | `(jogo_id)` | Lista de jogos e ordenação |
| REPOSITORIO | `(usuario_id)` | Perfil GitHub (RF-028) |
| SESSAO | `(mesa_id, realizada_em DESC)` | Histórico de sessões |
| FICHA | `(campanha_id)`, `(usuario_id)` | Personagens por campanha/jogador |

### 2.2 Constraints

| Tabela | Constraint | Expressão | Regra |
|---|---|---|---|
| USUARIO | `UK_email` | `email UNIQUE` | RN-03 |
| USUARIO | `UK_apelido` | `apelido UNIQUE` | RN-03 |
| POST | `CHK_status` | `status IN ('rascunho','publicado','editado','arquivado','excluido')` | Valores válidos |
| SEGUIDA | `CHK_autoseguimento` | `seguidor_id <> seguido_id` | RN-05 |
| USUARIO | `CHK_provider` | `provider IN ('local','github','google')` | Valores válidos |
| USUARIO | `CHK_provider_senha` | `(provider = 'local' AND hash_senha IS NOT NULL) OR (provider <> 'local' AND hash_senha IS NULL)` | RN-10: local precisa senha, OAuth não |
| TAG | `UK_nome` | `nome UNIQUE` | RN-08 |
| TAG | `UK_slug` | `slug UNIQUE` | RN-08 |
| FLAIR | `UK_nome` | `nome UNIQUE` | Badge único (RF-020) |
| USUARIO_LIVRO | `CHK_status_leitura` | `status IN ('lido','lendo','queroler')` | Valores válidos (RF-027) |
| USUARIO_LIVRO | `CHK_nota` | `nota IS NULL OR (nota BETWEEN 0 AND 5)` | Escala 0–5 |
| REPOSITORIO | `UK_url` | `url UNIQUE` | Sem repo duplicado (RF-028) |

> [!note] Soft delete em COMENTARIO
> No MVP, exclusão de comentário é hard delete. Se RF-015 (moderação) for implementada, adicionar coluna `status` em COMENTARIO seguindo o mesmo padrão de POST.

> [!note] Entidades das Fases 2/3 — modeladas
> `SEGUE_TAG`, `FLAIR`, `USUARIO_FLAIR` (Fase 2) e `LIVRO`, `USUARIO_LIVRO`, `JOGO`, `USUARIO_JOGO`, `REPOSITORIO`, `CAMPANHA`, `MESA`, `SESSAO`, `FICHA` (Fase 3) foram modeladas em **2026-08-29** (acima, na seção MER 1.2). Origem conceitual em [[VAULT/Plano - Rede Social de Nicho]] e consolidadas pelo [[VAULT/Checklist - Correções do Plano|Checklist (F.2)]].

### 2.3 Migrações
- Ferramenta candidata: **EF Core Migrations** ([[03 Decisões/ADR-003 Persistência|ADR-003]])
- Regra: toda mudança de schema nasce no DER como migração versionada

---
Links: [[02 Modelagem/Arquitetura do Sistema|Arquitetura]] · [[04 Gestão/Glossário|Glossário]]
