---
tags: [modelagem, api, servidor, backend]
tipo: documento
status: rascunho
atualizado: 2026-08-27
---

# API e Servidor

> [!info] Funcionamento do servidor
> Define como o servidor de Agora funciona: tecnologia, autenticação, contratos de endpoints, segurança e estrutura interna. Decisões em [[03 Decisões/ADR-005 API do Servidor|ADR-005]].

## 1. Visão geral

| Item | Decisão |
|---|---|
| Framework | ASP.NET Core Web API (.NET 10) |
| Estilo | REST + JSON + Swagger/OpenAPI |
| Autenticação | JWT Bearer (access + refresh token) |
| Persistência | EF Core 10 — servidor em **Npgsql/PostgreSQL** ([[03 Decisões/ADR-007 Banco do Servidor (Npgsql)|ADR-007]]); client em SQLite (configs/rascunho/cache) |
| Deploy | 3 ambientes ([[03 Decisões/ADR-004 Ambientes|ADR-004]]) |
| TLS | Obrigatório (RNF-07) |

## 2. Autenticação e autorização

- Login local: `POST /auth/login` → valida e-mail+senha (hash RNF-06), gera tokens
- Login OAuth: `POST /auth/github` · `POST /auth/google` (callback com `code`)
- **Access token** (JWT, curta duração ~15–60 min) enviado como `Authorization: Bearer <token>`
- **Refresh token** (longa duração) para renovar sem relogar; revogável
- Logout: invalida refresh token; access token expira naturalmente
- Autorização por claims: `id`, `provider`, papéis futuros

> [!note] Persistência do refresh token no client (desktop)
> O refresh token é guardado **criptografado via DPAPI** (escopo `CurrentUser`) em `%LOCALAPPDATA%\Agora\secrets.dat`; o access token fica **somente em memória** — [[03 Decisões/ADR-008 Segurança de Sessão (DPAPI)|ADR-008]].

## 3. Contratos de endpoints (MVP)

> [!note] Convenção
> Autenticado = requer `Authorization: Bearer`. Erros usam códigos HTTP padrão (4xx/5xx) + corpo JSON estruturado.

### Auth
| Método | Rota | Autenticado | Descrição |
|---|---|---|---|
| POST | `/auth/login` | não | Envia e-mail+senha → retorna tokens + perfil |
| POST | `/auth/github` | não | Recebe `code` OAuth → troca por tokens (UC-09) |
| POST | `/auth/google` | não | Idem Google (Fase 3) |
| POST | `/auth/refresh` | refresh | Renova access token |
| POST | `/auth/logout` | sim | Revoga refresh token |
| POST | `/auth/password/reset` | não | Solicita recuperação de senha (RF-002) |

> [!note] E-mail transacional (RF-002)
> Recuperação de senha exige um canal de e-mail — **SMTP configurável por ambiente** (ADR-004). Setup entra na Fase 1 via B-45.

### Usuários e perfil
| Método | Rota | Autenticado | Descrição |
|---|---|---|---|
| GET | `/usuarios/{id}` | sim | Perfil público (RFC-003) |
| PATCH | `/usuarios/me` | sim | Edita próprio perfil (nome, @, avatar, bio) |
| GET | `/usuarios/me` | sim | Próprio perfil |
| GET | `/usuarios?q=` | sim | Busca por @/nome (RF-010) |
| GET | `/usuarios/sugestoes` | sim | Sugestões de usuários p/ feed vazio (UC-05) |
| DELETE | `/usuarios/me` | sim | Excluir própria conta (RF-032, RN-04) |
| GET | `/usuarios/me/exportar` | sim | Exporta dados pessoais (RF-033, RNF-09) |

### Seguir
| Método | Rota | Autenticado | Descrição |
|---|---|---|---|
| POST | `/usuarios/{id}/seguidores` | sim | Seguir (RF-007) |
| DELETE | `/usuarios/{id}/seguidores` | sim | Deixar de seguir |
| GET | `/usuarios/{id}/seguidores` | sim | Lista seguidores |
| GET | `/usuarios/{id}/seguindo` | sim | Lista seguindo |

### Posts e feed
| Método | Rota | Autenticado | Descrição |
|---|---|---|---|
| GET | `/feed` | sim | Feed cronológico paginado (RF-005, RF-018) `?cursor=&tags=` |
| POST | `/posts` | sim | Publicar post (RF-004, texto+markdown+tags) |
| GET | `/posts/{id}` | sim | Detalhe do post |
| PATCH | `/posts/{id}` | sim | Editar próprio post (RF-006) |
| DELETE | `/posts/{id}` | sim | Excluir próprio post |
| GET | `/posts?q=&tag=` | sim | Busca posts por palavra-chave/tag (RF-010, RF-017) |

### Interações
| Método | Rota | Autenticado | Descrição |
|---|---|---|---|
| PUT | `/posts/{id}/curtida` | sim | Curtir (toggle, RN-02) |
| DELETE | `/posts/{id}/curtida` | sim | Descurtir |
| GET | `/posts/{id}/curtidas` | sim | Quem curtiu |
| POST | `/posts/{id}/comentarios` | sim | Comentar (RF-009) |
| DELETE | `/posts/{id}/comentarios/{cid}` | sim | Excluir comentário |
| GET | `/posts/{id}/comentarios` | sim | Lista comentários |

### Tags
| Método | Rota | Autenticado | Descrição |
|---|---|---|---|
| GET | `/tags?categoria=` | sim | Listar/buscar tags (RF-016, RF-023) |
| GET | `/tags/populares` | sim | Tags mais usadas (feed popular, RF-018) |
| POST | `/tags` | sim | Cria tag manualmente (RN-08) — também criada implicitamente no `POST /posts` (upsert) |

> Fase 2+: `/notificacoes`, `/mensagens`, `/midias`, `/seguindo-tags` (RF-011/012/013/019)

## 4. Paginação e consistência do feed

- Feed usa **cursor/token** (não offset) para consistência sob novos posts
- Ordenação cronológica desc. (RN-07), sem algoritmo
- Feed montado via join de posts dos seguidos + próprios; filtrável por tag
- Busca textual de posts (RF-010/RF-017) via **FTS do PostgreSQL** (`tsvector` + índice GIN); cache local usa `LIKE` (escopo pequeno)
- Resposta paginada: `{ data: [...], nextCursor: "..." }`

## 5. Segurança da API

| Medida | Base |
|---|---|
| TLS 1.2+ para todas as chamadas | RNF-07 |
| Hash adaptativo de senha (bcrypt/argon2) | RNF-06 |
| Rate limiting no login (força bruta) | RNF-08 |
| Validação de input em todos os endpoints | — |
| Sanitização/conteúdo e limites (RN-06) | RN-06 |
| Sem dados sensíveis em logs (RNF-16) | RNF-16 |
| Segredos (JWT signing key, connection string) via config por ambiente | ADR-004 |

## 6. Estrutura interna do servidor

```
Servidor/
└── Application (casos de uso, DTOs, interfaces)
└── Domain (entidades, RNs)          ← compartilhado/espelho do client
└── Infra (EF Core, repositórios, providers OAuth)
└── API (controllers, auth, middleware, OpenAPI)
```

- Controllers REST thin; regras de negócio no Domínio (RNF-12)
- DTOs próprios da API (não expor entidades)
- Middleware de erro → resposta JSON padronizada
- Swagger/OpenAPI para documentação e testes de integração

## 7. Funcionamento (resumo)

1. App desktop faz requisição REST com Bearer token
2. Servidor valida token → executa caso de uso (camada Application)
3. Regras de negócio aplicadas no Domínio; dados via EF Core
4. Responde JSON + códigos HTTP
5. Para rascunho offline, app usa cache local (RNF-15) e reenvia quando online

## 8. Áreas ainda em aberto (propostas)

- **P-001**: publicação na Microsoft Store ([[03 Decisões/Propostas Pendentes]])
- Notificações em tempo real (polling vs push) — Fase 2
- Upload de mídia (validação, armazenamento) — Fase 2

---

Links: [[03 Decisões/ADR-005 API do Servidor|ADR-005]] · [[02 Modelagem/Fluxos Principais|Fluxos]] · [[02 Modelagem/Arquitetura do Sistema|Arquitetura]] · [[04 Gestão/Operações e Deploy|Operações e Deploy]]
