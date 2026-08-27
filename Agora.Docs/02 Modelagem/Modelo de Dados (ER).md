---
tags: [modelagem, banco-de-dados, er]
tipo: documento
status: rascunho
atualizado: 2026-08-25
---

# Modelo de Dados (ER)

> [!info] Relação com o domínio
> Derivado de [[02 Modelagem/Modelo de Domínio]]. Nomes de tabelas em `snake_case`; PK/FK explícitas. SGBD: SQLite local (client) / PostgreSQL ou SqlServer (servidor) — definido em [[03 Decisões/ADR-003 Persistência|ADR-003]]. Inclui suporte a OAuth (RF-024, RF-025).

## Diagrama

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

> [!note] Convenção
> Diagrama ER via Mermaid `erDiagram`. `||--||` = 1:1, `||--o{` = 1:N, `}o--o{` = N:N. PK/FK explícitas; `UK` = unique key. Constraints detalhados abaixo do diagrama.

## Índices essenciais

| Tabela | Índice | Motivo |
|---|---|---|
| USUARIO | `(email)` | Login por e-mail (RF-002) |
| USUARIO | `(apelido)` | Busca por @ (RF-010) |
| USUARIO | `(provider, provider_id)` | Login OAuth — busca por provedor (RF-024) |
| POST | `(autor_id, status, publicado_em DESC)` | Feed e perfil |
| POST | `GIN/conteudo` ou FTS | RF-010 busca textual ⚠️ conforme SGBD |
| COMENTARIO | `(post_id, criado_em)` | Listar comentários de um post em ordem cronológica |
| CURTIDA | já coberta pela PK composta | RN-02 |
| SEGUIDA | `(seguido_id)` adicional | Listar seguidores |
| NOTIFICACAO | `(usuario_destino, lida, criado_em DESC)` | Badge não lidas |
| TAG | `(categoria)` | Busca por categoria |
| POST_TAG | já coberta pela PK composta | RN-09 |

## Constraints

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

> [!note] Soft delete em COMENTARIO
> No MVP, exclusão de comentário é hard delete. Se RF-015 (moderação) for implementada, adicionar coluna `status` em COMENTARIO seguindo o mesmo padrão de POST.

## Migrações
- Ferramenta candidata: **EF Core Migrations** ([[03 Decisões/ADR-003 Persistência|ADR-003]])
- Regra: toda mudança de schema nasce aqui como migração versionada

---
Links: [[02 Modelagem/Arquitetura do Sistema|Arquitetura]] · [[04 Gestão/Glossário|Glossário]]
