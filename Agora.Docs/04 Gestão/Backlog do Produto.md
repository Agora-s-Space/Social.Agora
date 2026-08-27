---
tags: [gestao, backlog]
tipo: documento
status: ativo
atualizado: 2026-08-25
---

# Backlog do Produto

> [!tip] Convenção
> Itens de implementação vinculados a RFs/RNFs. Cada item representa uma unidade de trabalho entregável. RFs vivem em [[01 Requisitos/Requisitos Funcionais|RF]].
>
> **Colunas:** Esforço (S/M/L/XL) · Status (todo / em andamento / done / bloqueado) · Depende de

## Must have (Fase 1 — MVP)

| ID | Item | Origem | Esforço | Depende de | Status |
|---|---|---|:-:|---|:-:|
| B-10 | ADRs críticos (stack + implantação) | Gestão | S | — | concluído ✅ |
| B-09 | Setup CI build+testes | RNF-14 | S | B-10 | todo |
| B-01 | Cadastro/login/logout + recuperação de senha | RF-001, RF-002 | M | B-10 | todo |
| B-02 | CRUD perfil básico (nome, @, avatar, bio) | RF-003 | S | B-01 | todo |
| B-03 | Publicar post com markdown (texto + code blocks) | RF-004, RF-006, RN-06 | M | B-01 | todo |
| B-24 | Syntax highlighting em code blocks | RF-022, RNF-17 | M | B-03 | todo |
| B-04 | Feed cronológico paginado (seguidos + próprios) | RF-005, RN-07 | L | B-01 | todo |
| B-05 | Seguir/deixar de seguir usuário | RF-007, RN-05 | M | B-01 | todo |
| B-06 | Curtir post (toggle) | RF-008, RN-02 | S | B-03 | todo |
| B-07 | Comentar/excluir comentário | RF-009, RN-01 | M | B-03 | todo |
| B-08 | Busca por usuários e posts | RF-010 | M | B-01 | todo |
| B-25 | Tags com categoria (linguagem/tema/genero/sistema) | RF-023, RN-08 | M | B-03 | todo |
| B-18 | Adicionar 1–5 tags ao post | RF-016, RN-09 | L | B-25 | todo |
| B-19 | Busca por tag | RF-017 | M | B-18 | todo |
| B-20 | Feed por tags populares (seção alternativa) | RF-018 | M | B-18 | todo |
| B-34 | Tela de splash: logo letter metálica (flash) + partes azuis (energia cristalina) | RF-031, RNF-18 | S | B-10 | todo |
| B-35 | Wireframes de todas as telas | — | M | — | todo |
| B-36 | Política de ambientes dev/staging/prod (config por ambiente) | RNF-19, ADR-004 | M | B-10 | todo |
| B-37 | Pipeline CD (deploy automático staging; prod via aprovação) | RNF-20 | M | B-09 | todo |
| B-38 | Backup + restore do banco de produção | RNF-21 | M | B-36 | todo |
| B-39 | Observabilidade: logs, métricas, health checks, alertas | RNF-16, RNF-22 | M | B-36 | todo |
| B-40 | Empacotamento MSIX (installer + sideload; habilitar opção Microsoft Store) | RNF-23 | M | B-01 | todo |
| B-41 | Documentação de suporte (runbook, rollback, troubleshooting) | — | S | B-36 | todo |
| B-42 | Skeleton da API do servidor: ASP.NET Core Web API + auth JWT (access/refresh) + OpenAPI | RNF-07, ADR-005 | M | B-10 | todo |

## Should have (Fase 2)

| ID | Item | Origem | Esforço | Depende de | Status |
|---|---|---|:-:|---|:-:|
| B-11 | Notificações in-app (curtida, comentário, seguidor) | RF-011 | M | B-06, B-07 | backlog |
| B-12 | Upload de imagem em posts | RF-012 | M | B-03 | backlog |
| B-13 | Mensagens diretas 1:1 | RF-013 | XL | B-01 | backlog |
| B-21 | Seguir tags (feed personalizado) | RF-019 | M | B-18 | backlog |
| B-22 | Flair de perfil (badge visual) | RF-020 | S | B-02 | backlog |
| B-23 | Perfil expandido (stack, jogos, autores) | RF-021 | M | B-02 | backlog |
| B-26 | Login/cadastro OAuth via GitHub | RF-024 | L | B-01 | backlog |

## Could have (Fase 3)

| ID | Item | Origem | Esforço | Depende de | Status |
|---|---|---|:-:|---|:-:|
| B-14 | Contas privadas (aprovação de seguidores) | RF-014 | L | B-05 | backlog |
| B-15 | Denúncias/moderação de conteúdo | RF-015 | L | B-07 | backlog |
| B-16 | Temas claro/escuro completos | RNF-05 | S | B-02 | backlog |
| B-27 | Login/cadastro OAuth via Google | RF-025 | L | B-26 | backlog |
| B-28 | Vincular/desvincular provedor externo | RF-026 | M | B-26 | backlog |
| B-29 | Biblioteca de livros no perfil (lidos, want-to-read, notas, resenha) | RF-027 | L | B-02 | backlog |
| B-30 | Integração GitHub (repositórios, projetos, tech stack) | RF-028 | L | B-01 | backlog |
| B-31 | Lista de jogos jogados (horas, review, plataforma) | RF-029 | L | B-02 | backlog |
| B-32 | Mesas e campanhas de RPG (mesa, sessão, ficha, sistema) | RF-030 | XL | B-02 | backlog |

## Definition of Done (proposta)
1. Código revisado + testes no domínio (RNF-13)
2. Critérios do RF/RNF atendidos
3. Documentação atualizada neste vault
4. CI verde (RNF-14)
