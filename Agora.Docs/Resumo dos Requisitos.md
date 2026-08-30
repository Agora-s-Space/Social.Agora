---
tags: [requisitos, resumo]
tipo: resumo
status: ativo
atualizado: 2026-08-30
---

# Resumo dos Requisitos

> [!abstract] Visão em uma frase
> Aplicativo **desktop em C# (.NET)** que implementa uma rede social de hobbies nerd — **Agora** — com perfis, publicações com markdown, feed cronológico, tags de conteúdo, seguidores e interações (curtidas/comentários), começando pelo Windows e evoluindo para mais plataformas.
>
> **Este documento consolida TODOS os requisitos.** Os detalhes vivem nas notas-fonte linkadas em cada seção.

---

## 1. Requisitos Funcionais (completo)

Fonte detalhada: [[01 Requisitos/Requisitos Funcionais]]

### MVP — Fase 1 (Must)

> Detalhes completos: [[04 Gestão/Definição do MVP|Definição do MVP]]

| ID | Requisito | Módulo | Regras | UC |
|---|---|---|---|---|
| RF-001 | Cadastro com e-mail + senha, validando e-mail único | Conta | [[01 Requisitos/Regras de Negócio\|RN-03]] | UC-01 |
| RF-002 | Autenticação (login/logout) + recuperação de senha por e-mail | Conta | RNF-06, RNF-08 | UC-02 |
| RF-003 | Perfil editável: nome de exibição, @apelido único, avatar, bio | Conta | [[01 Requisitos/Regras de Negócio\|RN-03]] | UC-03 |
| RF-004 | Publicar posts com markdown e syntax highlighting (limite: 5.000 chars) | Conteúdo | [[01 Requisitos/Regras de Negócio\|RN-06]] | UC-04 |
| RF-005 | Feed cronológico (seguidos + próprios), paginado | Conteúdo | [[01 Requisitos/Regras de Negócio\|RN-07]] | UC-05 |
| RF-006 | Autor edita ou exclui seus próprios posts | Conteúdo | [[01 Requisitos/Regras de Negócio\|RN-01]] | UC-04 |
| RF-007 | Seguir / deixar de seguir qualquer usuário | Social | [[01 Requisitos/Regras de Negócio\|RN-05]] | UC-06 |
| RF-008 | Curtir posts (1 curtida por usuário/post, com toggle) | Social | [[01 Requisitos/Regras de Negócio\|RN-02]] | UC-07 |
| RF-009 | Comentar posts; exclusão pelo autor do post ou do comentário | Social | [[01 Requisitos/Regras de Negócio\|RN-01]] | UC-07 |
| RF-010 | O sistema deve permitir busca por usuários (@apelido/nome) e posts (palavra-chave) | Descoberta | — | UC-08 |
| RF-016 | Adicionar 1–5 tags de conteúdo ao post | Conteúdo | [[01 Requisitos/Regras de Negócio\|RN-08]], [[01 Requisitos/Regras de Negócio\|RN-09]] | UC-04 |
| RF-017 | Busca por tag, filtrando feed e resultados | Descoberta | — | UC-08 |
| RF-018 | O sistema deve exibir feed alternativo: no MVP por tags populares; Fase 2 por tags seguidas (requer RF-019) | Conteúdo | — | UC-05 |
| RF-022 | Syntax highlighting em blocos de código (detecção via fence markdown) | Conteúdo | RNF-17 | UC-04 |
| RF-023 | O sistema deve exigir campo `categoria` obrigatório nas tags (linguagem/tema/genero/sistema) | Descoberta | RN-08 | UC-04 |
| RF-031 | Tela de splash com logo (letter metálica + partes azuis): flash no metal + energia cristalina | UI | RNF-18 | UC-01 |
| RF-032 | Excluir conta com anonimização em ≤ 30 dias; posts removidos/anonimizados por escolha do usuário | Conta | [[01 Requisitos/Regras de Negócio\|RN-04]] | UC-10 |
| RF-033 | Exportar dados pessoais em formato legível (LGPD) | Conta | RNF-09 | UC-10 |
| RF-034 | Registrar aceite da Política de Privacidade/Termos (data + versão) e revogar consentimento | Conta | [[01 Requisitos/Regras de Negócio\|RN-04]], RNF-26 | UC-10 |

### Fases futuras

| ID | Requisito | Fase |
|---|---|:-:|
| RF-011 | O sistema deve exibir notificações in-app: nova curtida, comentário, seguidor | 2 (Should) |
| RF-012 | O sistema deve permitir upload de mídia (imagem) em posts | 2 |
| RF-013 | O sistema deve permitir mensagens diretas 1:1 entre usuários | 2 |
| RF-019 | O sistema deve permitir que o usuário siga tags, não só usuários | 2 |
| RF-020 | O sistema deve permitir flair de perfil (badge visual) | 2 |
| RF-021 | O sistema deve permitir perfil expandido (stack tech, jogos favoritos, autores favoritos) | 2 |
| RF-024 | Cadastro/login via OAuth com GitHub | 2 (Should) |
| RF-014 | O sistema deve permitir contas privadas (aprovação de seguidores) | 3 |
| RF-015 | O sistema deve permitir moderação: denúncia de conteúdo/usuário | 3 |
| RF-025 | Cadastro/login via OAuth com Google | 3 (Could) |
| RF-026 | Vincular/desvincular conta de provedor externo (1 provedor por conta) | 3 (Could) |
| RF-027 | Biblioteca de livros no perfil (lidos, want-to-read, notas, resenha) | 3 (Could) |
| RF-028 | Integração com GitHub (repositórios, projetos, tech stack) | 3 (Could) |
| RF-029 | Lista de jogos jogados (horas, review, plataforma) | 3 (Could) |
| RF-030 | Mesas e campanhas de RPG (mesa, sessão, ficha, sistema, one-shots) | 3 (Could) |

---

## 2. Requisitos Não Funcionais (completo)

Fonte detalhada: [[01 Requisitos/Requisitos Não Funcionais]]

> [!note] Base normativa
> Metas de qualidade seguem o modelo **ISO/IEC 25010:2011 (SQuaRE)** — "atributos de qualidade (ISO/IEC)" do enunciado da entrega.

| ID | Categoria | Requisito | Meta/Critério |
|---|---|---|---|
| RNF-01 | Desempenho | Carregamento da primeira página do feed | ≤ 2 s (P95) |
| RNF-02 | Desempenho | Inicialização até tela de login/feed | ≤ 3 s em HDD comum ⚠️ |
| RNF-03 | Desempenho | Feedback visual de ações (curtir/comentar/seguir) | ≤ 200 ms |
| RNF-04 | Usabilidade | Navegação completa via teclado — 100% das ações acessíveis sem mouse | Tab/Enter/setas |
| RNF-05 | Usabilidade/A11y | Contraste WCAG AA (ratio ≥ 4.5:1) + temas claro/escuro | Auditoria visual |
| RNF-06 | Segurança | Senhas com hash adaptativo, nunca em texto puro | bcrypt cost ≥ 10 (OWASP) |
| RNF-07 | Segurança | Comunicação cliente-servidor cifrada | TLS 1.2+ |
| RNF-08 | Segurança | Proteção contra força bruta no login | ≤ 5 tentativas falhas → bloqueio ≥ 15 min |
| RNF-09 | Privacidade | Conformidade LGPD (consentimento, exportação, exclusão) | Solicitações ≤ 15 dias (art. 19)
| RNF-10 | Portabilidade | Windows 10+ primário, sem bloquear Linux/macOS | Decisão em [[03 Decisões/ADR-001 Stack Tecnológica\|ADR-001]] |
| RNF-11 | Portabilidade | Runtime .NET LTS | .NET 10 LTS até nov/2028 |
| RNF-12 | Manutenibilidade | Código em camadas (UI → Aplicação → Domínio ← Infra) | 0 violações de dependência (teste de arquitetura no CI) |
| RNF-13 | Qualidade | Cobertura de testes no núcleo de domínio | ≥ 60% ⚠️ |
| RNF-14 | Qualidade | CI com build + testes a cada push | Build + testes ≤ 10 min ⚠️ |
| RNF-15 | Confiabilidade | Falha de rede não perde rascunho em edição | Autosave local |
| RNF-16 | Operação | Logs estruturados sem dados sensíveis | Padrão de logging |
| RNF-17 | Desempenho | Renderização de syntax highlighting em blocos de código | ≤ 100 ms (P95, bloco ≤ 50 linhas) |
| RNF-18 | UI/Experiência | Animações e transições de tela (splash, loading, transições entre telas) | 60 fps; splash ≤ 3s; transições ≤ 300 ms |
| RNF-19 | Implantação | Múltiplos ambientes isolados (dev/staging/prod) | 3 ambientes ([[03 Decisões/ADR-004 Ambientes\|ADR-004]]) |
| RNF-20 | Implantação | Deploy automatizado via pipeline (CI/CD) | Deploy p/ staging automático; prod via aprovação |
| RNF-21 | Operação | Backup e restauração do banco de produção | Backup diário; restore testado |
| RNF-22 | Operação | Observabilidade: logs, métricas, health checks, alertas | Logs estruturados; alerta em falha |
| RNF-23 | Entrega | Empacotamento do app desktop em **MSIX** (sideload; opção futura Microsoft Store) | Installer + atualização possível |
| RNF-24 | Operação | SLA de disponibilidade do servidor — uptime mensal da **produção** | ≥ 99,5%/mês; exclui janelas de manutenção programadas · [[04 Gestão/SLA de Disponibilidade\|SLA]] |
| RNF-25 | Desempenho | Throughput do servidor — taxa de req/s sustentada | ≥ 50 req/s; sem degradar RNF-01/03 · teste de carga no CI |
| RNF-26 | Privacidade | Alinhamento com princípios GDPR (minimização, limitação de armazenamento, accountability, notificação de violação) — GDPR não aplicável hoje ([[01 Requisitos/LGPD e Privacidade\|LGPD e Privacidade]]) | Consentimento registrado (data/versão); retenção ≤ 30 d (dados/backups); notificação ≤ 72 h ⚠️ |

---

## 3. Regras de Negócio (completo)

Fonte detalhada: [[01 Requisitos/Regras de Negócio]]

| ID | Regra | Impacta |
|---|---|---|
| RN-01 | Somente o autor edita/exclui o próprio post/comentário | RF-006, RF-009 |
| RN-02 | Máx. 1 curtida por usuário/post; curtir de novo desfaz (toggle) | RF-008 |
| RN-03 | E-mail e @apelido únicos no sistema | RF-001, RF-003 |
| RN-04 | Exclusão de conta anonimiza dados pessoais em ≤ 30 dias; posts podem ser removidos ou anonimizados por escolha do usuário | RNF-09, RF-032 |
| RN-05 | Um usuário não pode seguir a si mesmo | RF-007 |
| RN-06 | Posts limitados a 5.000 caracteres (configurável) — flexibilizado para code blocks | RF-004, RF-022 |
| RN-07 | Feed ordena por data de publicação desc.; sem promoção algorítmica | RF-005 |
| RN-08 | Tags: nomes únicos, slug auto-gerado | RF-016, RF-023 |
| RN-09 | Máximo de 5 tags por post | RF-016 |
| RN-10 | Conta criada via OAuth não possui senha local; login exclusivo pelo provedor | RF-024, RF-025 |
| RN-11 | Retenção: dados anonimizados ≤ 30 d; backups ≤ 30 d; servidor em datacenter BR | RNF-09, RNF-26, RF-032 |
| RN-12 | Violação de dados pessoais → notificar ANPD/titulares (LGPD art. 48) ≤ 72 h + registro interno | RNF-26 |

---

## 4. Casos de Uso (completo)

Fonte detalhada: [[01 Requisitos/Casos de Uso]]

| ID | Nome | Ator | Resumo | RFs |
|---|---|---|---|---|
| UC-01 | Cadastrar conta | Visitante | Criar conta com validação de unicidade | RF-001 |
| UC-02 | Autenticar | Visitante | Login/logout + recuperação de senha | RF-002 |
| UC-03 | Gerenciar perfil | Usuário | Editar nome, @apelido, avatar, bio | RF-003 |
| UC-04 | Publicar post | Usuário | Compor, salvar rascunho, publicar/editar/excluir, markdown + syntax highlighting + tags | RF-004, RF-006, RF-016, RF-022 |
| UC-05 | Visualizar feed | Usuário | Lista cronológica paginada + feed por tags | RF-005, RF-018 |
| UC-06 | Seguir usuário | Usuário | Seguir/deixar de seguir; afeta feed | RF-007 |
| UC-07 | Interagir com post | Usuário | Curtir (toggle), comentar, excluir comentário | RF-008, RF-009 |
| UC-08 | Buscar | Usuário | Resultados separados: usuários / posts / tags | RF-010, RF-017 |
| UC-09 | Autenticar via OAuth | Visitante | Cadastro/login via GitHub/Google; fluxo OAuth com callback | RF-024, RF-025 |
| UC-10 | Gerenciar dados da conta | Usuário | Excluir conta (remover/anonimizar posts), exportar dados e gerenciar consentimento | RF-032, RF-033, RF-034 |

---

## 5. Modelo de dados — entidades (resumo)

Fonte detalhada: [[02 Modelagem/Modelo de Domínio]] · [[02 Modelagem/Modelo de Dados (ER)|ER]]

| Entidade | Papel | Chaves/constraints principais |
|---|---|---|
| `USUARIO` | Conta do sistema | `email` UK, `apelido` UK (RN-03), `hash_senha` nullable (RN-10), `provider`, `provider_id`, `exclusao_agendada_em`/`destino_posts` (RN-04) |
| `PERFIL` | Dados exibidos (1:1 com usuário) | `usuario_id` PK/FK |
| `POST` | Publicação de texto | `status` (rascunho/publicado/editado/arquivado/excluido), ≤5.000 chars (RN-06) |
| `COMENTARIO` | Resposta a um post | FK `post_id`, FK `autor_id` |
| `CURTIDA` | Marcação única (toggle) | PK composta (`usuario_id`,`post_id`) = RN-02 |
| `SEGUIDA` | Relação direcionada seguidor→seguido | PK composta; impede RN-05 |
| `CONSENTIMENTO` | Aceite/revogação de política (privacidade/termos) | `usuario_id` FK, `versao`, `aceito_em`, `revogado_em` (RF-034) |
| `TAG` | Classificador de conteúdo por tema/hobby | `nome` UK (RN-08), `slug` auto-gerado |
| `POST_TAG` | Relação N:N post-tag | PK composta; máx. 5/post (RN-09) |

### Entidades das Fases 2/3 (modeladas em 2026-08-29)

| Entidade | Papel | Fase |
|---|---|---|
| `NOTIFICACAO` | Eventos para o destinatário — badge de não lidas (RF-011) | 2 |
| `SEGUE_TAG` | Usuário segue tags (feed por interesse, RF-019) | 2 |
| `FLAIR` / `USUARIO_FLAIR` | Badges visuais de perfil (RF-020) | 2 |
| `PERFIL` (extensão) | Campos opcionais: stack tech, jogos/autores favoritos (RF-021) | 2 |
| `LIVRO` / `USUARIO_LIVRO` | Catálogo global + estado de leitura/nota/resenha (RF-027) | 3 |
| `JOGO` / `USUARIO_JOGO` | Catálogo global + horas/review/plataforma (RF-029) | 3 |
| `REPOSITORIO` | Repositórios GitHub no perfil (RF-028) | 3 |
| `CAMPANHA` / `MESA` / `SESSAO` / `FICHA` | Mesas e campanhas de RPG (RF-030) | 3 |

Detalhes em [[02 Modelagem/Modelo de Dados (ER)|ER]] e [[02 Modelagem/Modelo de Domínio|Domínio]].

> [!note] Feed
> O **Feed** não é uma entidade persistida — é uma *consulta agregadora* que combina posts dos seguidos + próprios (RF-005). Sua implementação depende do modelo de implantação (ADR-002).

---

## 6. Decisões de arquitetura (ADRs)

Fonte detalhada: [[03 Decisões/ADR Template]]

| ID | Título | Status | Decisão/resumo |
|---|---|:-:|---|
| ADR-001 | Stack Tecnológica (UI e runtime) | aceita ✅ | Avalonia UI + .NET 10 LTS + MVVM (CommunityToolkit) |
| ADR-002 | Implantação (local-first × cliente-servidor) | aceita ✅ | Cliente-servidor desde o início; cache local p/ rascunho (RNF-15) |
| ADR-003 | Persistência (ORM) | aceita ✅ | EF Core 10 — SQLite client (configs/rascunho/cache); Npgsql server (ADR-007) |
| ADR-004 | Ambientes (dev/staging/prod) | aceita ✅ | 3 ambientes isolados; dev local, staging e prod no VPS |
| ADR-005 | Design da API do Servidor | aceita ✅ | REST (ASP.NET Core Web API) + JWT Bearer |
| ADR-007 | Banco do Servidor | aceita ✅ | PostgreSQL (Npgsql); SQLite fica no client (configs/rascunho/cache) |
| ADR-008 | Segurança de sessão (desktop) | aceita ✅ | Refresh token via DPAPI (escopo CurrentUser); access token só em memória |
| ADR-009 | Residência dos dados (datacenter no Brasil) | aceita ✅ | Servidor (banco, backups, logs) em datacenter no Brasil — conformidade LGPD |
| ADR-010 | S.O. da VPS (Linux + Docker Compose) | aceita ✅ | Ubuntu LTS 24.04; app e PostgreSQL em containers; deploy via CI/CD |

> [!note] ADR-006
> [[03 Decisões/ADR-006 Observabilidade|ADR-006]] (Observabilidade) está registrada como **adiada / fora do MVP** — Prometheus+Grafana na Fase 2 ([[VAULT/Checklist - Correções do Plano|checklist]]).

---

## 7. Backlog (completo)

Fonte detalhada: [[04 Gestão/Backlog do Produto]]

| ID | Item | Origem | Esforço | Prioridade | Status |
|---|---|---|---|:-:|:-:|:-:|
| B-10 | ADRs críticos (stack + implantação) | Gestão | S | Must | concluído ✅ |
| B-09 | Setup CI build+testes | RNF-14 | S | Must | todo |
| B-01 | Cadastro/login/logout + recuperação de senha | RF-001, RF-002 | M | Must | todo |
| B-02 | CRUD perfil básico (nome, @, avatar, bio) | RF-003 | S | Must | todo |
| B-03 | Publicar post com markdown (texto + code blocks) | RF-004, RF-006, RN-06 | M | Must | todo |
| B-24 | Syntax highlighting em code blocks | RF-022, RNF-17 | M | Must | todo |
| B-04 | Feed cronológico paginado (seguidos + próprios) | RF-005, RN-07 | L | Must | todo |
| B-05 | Seguir/deixar de seguir usuário | RF-007, RN-05 | M | Must | todo |
| B-06 | Curtir post (toggle) | RF-008, RN-02 | S | Must | todo |
| B-07 | Comentar/excluir comentário | RF-009, RN-01 | M | Must | todo |
| B-08 | Busca por usuários e posts | RF-010 | M | Must | todo |
| B-25 | Tags com categoria (linguagem/tema/genero/sistema) | RF-023, RN-08 | M | Must | todo |
| B-18 | Adicionar 1–5 tags ao post | RF-016, RN-09 | L | Must | todo |
| B-19 | Busca por tag | RF-017 | M | Must | todo |
| B-20 | Feed por tags populares (seção alternativa) | RF-018 | M | Must | todo |
| B-34 | Tela de splash: logo letter metálica (flash) + partes azuis (energia cristalina) | RF-031, RNF-18 | S | Must | todo |
| B-35 | Wireframes de todas as telas | — | M | Must | todo |
| B-36 | Política de ambientes dev/staging/prod (config por ambiente) | RNF-19, ADR-004 | M | Must | todo |
| B-37 | Pipeline CD (deploy automático staging; prod via aprovação) | RNF-20 | M | Must | todo |
| B-38 | Backup + restore do banco de produção | RNF-21 | M | Must | todo |
| B-39 | Observabilidade: logs, métricas, health checks, alertas | RNF-16, RNF-22 | M | Must | todo |
| B-40 | Empacotamento MSIX (installer + sideload; habilitar opção Microsoft Store) | RNF-23 | M | Must | todo |
| B-41 | Documentação de suporte (runbook, rollback, troubleshooting) | — | S | Must | todo |
| B-42 | Skeleton da API do servidor: ASP.NET Core Web API + auth JWT (access/refresh) + OpenAPI | RNF-07, ADR-005 | M | Must | todo |
| B-43 | Exclusão de conta (remover/anonimizar posts) | RF-032, RN-04 | M | Must | todo |
| B-44 | Exportação de dados pessoais (LGPD) | RF-033, RNF-09 | S | Must | todo |
| B-45 | E-mail transacional p/ recuperação de senha | RF-002 | S | Must | todo |
| B-47 | Medição/verificação do SLA de disponibilidade (uptime da produção — RNF-24) | RNF-24 | S | Must | todo |
| B-48 | Teste de carga + benchmark de throughput (≥ 50 req/s) no CI | RNF-25 | M | Must | todo |
| B-49 | Teste de arquitetura (0 violações de dependência entre camadas — regras do RNF-12) no CI | RNF-12 | S | Must | todo |
| B-50 | Política de Privacidade + Termos de Uso (aceite no cadastro) | RF-034, RNF-26 | S | Must | todo |
| B-51 | Registro e revogação do consentimento (data/hora + versão da política) | RF-034, RNF-26 | S | Must | todo |
| B-52 | Política de retenção: anonimizar ≤ 30 d + backups ≤ 30 d + residência BR | RN-11, ADR-009 | S | Must | todo |
| B-53 | Runbook de notificação de violação de dados pessoais | RN-12, RNF-26 | S | Must | todo |
| B-46 | Dashboards + alertas Prometheus/Grafana | RNF-22, ADR-006 | M | Fase 2 | backlog |
| B-11 | Notificações in-app (curtida, comentário, seguidor) | RF-011 | M | Should | backlog |
| B-12 | Upload de imagem em posts | RF-012 | M | Should | backlog |
| B-13 | Mensagens diretas 1:1 | RF-013 | XL | Should | backlog |
| B-21 | Seguir tags (feed personalizado) | RF-019 | M | Should | backlog |
| B-22 | Flair de perfil (badge visual) | RF-020 | S | Should | backlog |
| B-23 | Perfil expandido (stack, jogos, autores) | RF-021 | M | Should | backlog |
| B-26 | Login/cadastro OAuth via GitHub | RF-024 | L | Should | backlog |
| B-14 | Contas privadas (aprovação de seguidores) | RF-014 | L | Could | backlog |
| B-15 | Denúncias/moderação de conteúdo | RF-015 | L | Could | backlog |
| B-16 | Temas claro/escuro completos | RNF-05 | S | Could | backlog |
| B-27 | Login/cadastro OAuth via Google | RF-025 | L | Could | backlog |
| B-28 | Vincular/desvincular provedor externo | RF-026 | M | Could | backlog |
| B-29 | Biblioteca de livros no perfil (lidos, want-to-read, notas, resenha) | RF-027 | L | Could | backlog |
| B-30 | Integração GitHub (repositórios, projetos, tech stack) | RF-028 | L | Could | backlog |
| B-31 | Lista de jogos jogados (horas, review, plataforma) | RF-029 | L | Could | backlog |
| B-32 | Mesas e campanhas de RPG (mesa, sessão, ficha, sistema) | RF-030 | XL | Could | backlog |

---

## 8. Marcos e fases (resumo do Roadmap)

Fonte detalhada: [[04 Gestão/Roadmap]]

| Marco | Fase | Critério de saída | Janela estimada ⚠️ |
|---|:-:|---|---|
| M0 | 0 | Modelagem aprovada; ADRs 001/002/003/004/005/007 aceitos | ago–set 2026 |
| M1 | 1 | RF-001..010, RF-016..018, RF-022..023, RF-031..034 entregues; RNF-01/03/17/18/24/25 medidos OK; ambientes + CI/CD prontos | out–dez 2026 |
| M2 | 2–3 | Beta externo com 20 usuários ativos (OKR O2) | jan–mar 2027 |

---

## 9. Números-chave

| Métrica | Valor |
|---|:-:|
| Requisitos funcionais documentados | 34 (19 no MVP) |
| Requisitos não funcionais | 26 |
| Regras de negócio | 12 |
| Casos de uso | 10 |
| Entidades de domínio | 22 (9 MVP + 13 F2/F3) |
| Itens de backlog | 51 |
| ADRs | 9 aceitas* |

> *ADRs 008/009/010 (segurança de sessão, residência dos dados, SO da VPS) incluídas. ADR-006 é registrada como **adiada/fora do MVP** e não conta entre as aceitas.

> [!question] Decisões abertas que impactam os requisitos
> Arquitetura crítica resolvida (**9 ADRs aceitas** + ADR-006 adiada). **[[03 Decisões/Propostas Pendentes|Propostas em aberto]]:** P-001 (Microsoft Store, Fase 2/3). **ADR-006** (observabilidade) **adiada, fora do MVP** — Prometheus + Grafana na Fase 2 via [[04 Gestão/Backlog do Produto#B-46|B-46]]. O conjunto que compõe o MVP está estável.
