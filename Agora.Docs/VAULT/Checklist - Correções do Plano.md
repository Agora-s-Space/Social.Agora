---
tags: [vault, checklist, plano, correcao]
tipo: checklist
status: ativo
atualizado: 2026-08-28
---

# Checklist — Correções do Plano

> [!info] Objetivo
> Consolidar **todas** as tarefas levantadas na análise do plano (2026-08-28). Cada item é verificável nos arquivos do vault.
> **Legenda:** `[x]` = resolvido nesta revisão · `[ ]` = pendente (decisão de equipe ou Fase 1).

## A. Ressincronizar o plano de nicho — ✅ concluído

- [x] **A.1** Corrigir tabelas de features com os IDs finais de RF (RF-016..024) em [[VAULT/Plano - Rede Social de Nicho|Plano]]
- [x] **A.2** Alinhar seção de impactos (ER/RN/UC) e backlog do plano aos IDs reais
- [x] **A.3** Registrar **Séries de posts** e **Coleções** como avaliadas e **descartadas** (status explícito)
- [x] **A.4** Fechar decisões **D-1..D-5** e apontar onde cada uma consta (RF/UC/backlog)
- [x] **A.5** Atualizar "Próximos passos", riscos (LGPD) e mapa mental do plano

> Verificado em: `VAULT/Plano - Rede Social de Nicho.md` · `04 Gestão/Backlog do Produto.md`

## B. Persistência e stack — ✅ concluído

- [x] **B.1** Criar **ADR-007 — Banco do Servidor: PostgreSQL (Npgsql)** (`status: aceita`)
- [x] **B.2** Marcar ADR-003 como **parcialmente substituída** apenas no provedor do servidor (ORM continua EF Core 10)
- [x] **B.3** Esclarecer papel do **SQLite no client**: configs locais + rascunho (RNF-15) + cache de feed
- [x] **B.4** Remover ambiguidade "Npgsql/SqlServer" de ER, API e Servidor, Operações e Deploy, Resumo, Home, AGENTS, ADR-004 e README

> Novos arquivos: `03 Decisões/ADR-007 Banco do Servidor (Npgsql).md`

## C. Lacunas de escopo (LGPD) — ✅ concluído

- [x] **C.1** **RF-032** — Excluir conta (RN-04/RNF-09) + **UC-10** + endpoint `DELETE /usuarios/me`
- [x] **C.2** **RF-033** — Exportar dados (RNF-09) + endpoint `GET /usuarios/me/exportar`
- [x] **C.3** Itens de backlog **B-43/B-44/B-45** (exclusão, exportação, e-mail transacional)
- [x] **C.4** Refletir RF-032/033 e RNF-09 em [[04 Gestão/Definição do MVP|Definição do MVP]] (18 RFs / 18 RNFs / 26 itens) e nos critérios de aceitação
- [x] **C.5** Atualizar contadores e marcos (Resumo dos Requisitos, Roadmap)

> Arquivos: `01 Requisitos/Requisitos Funcionais.md` · `01 Requisitos/Casos de Uso.md` · `02 Modelagem/API e Servidor.md` · `04 Gestão/Backlog do Produto.md` · `04 Gestão/Definição do MVP.md`

## D. Decisões de produto que estavam implícitas — ✅ concluído

- [x] **D.1** **Avatar no MVP**: URL externa (`avatar_url`); upload entra com RF-012 (Fase 2)
- [x] **D.2** **Busca textual**: FTS do PostgreSQL (`tsvector` + GIN) no servidor; `LIKE` apenas no cache local
- [x] **D.3** **E-mail transacional** (recuperação de senha / RF-002): SMTP configurável por ambiente → B-45
- [x] **D.4** **Criação de tags**: implícita (upsert) no `POST /posts` + `POST /tags` opcional
- [x] **D.5** **Sugestão de usuários** p/ feed vazio (UC-05): endpoint `GET /usuarios/sugestoes`
- [x] **D.6** **Syntax highlighting**: AvaloniaEdit (editor) + Markdig (renderização) — ⚠️ validar no spike (RNF-17)
- [x] **D.7** Estado `Arquivado` documentado como **não exposto no MVP**

> Notas espalhadas em: `01 Requisitos/Requisitos Funcionais.md` · `02 Modelagem/API e Servidor.md` · `02 Modelagem/Modelo de Dados (ER).md` · `02 Modelagem/Máquinas de Estado.md`

## E. Consistência e erros — ✅ concluído

- [x] **E.1** Corrigir "≤500" → "≤5.000" (RN-06) em `Fluxos Principais` e `Modelo de Domínio`
- [x] **E.2** UC-07: notificação marcada como Fase 2 (RF-011 fora do MVP)
- [x] **E.3** RN-04 (RF-032) e RN-08 (RF-023) com vínculos corretos
- [x] **E.4** Contadores: **33 RFs (18 MVP)** · 23 RNFs · 10 RNs · **10 UCs** · **44 itens** de backlog · **7 ADRs aceitas** (ADR-006 fica **adiada**, fora do MVP)
- [x] **E.5** ADR-006 formalizada como **`adiada`/fora do MVP** (status regularizado em `Propostas Pendentes` e ADR-006)

## F. Pendências — decidir na Fase 1 / com a equipe 🔲

- [x] **F.1** **ADR-006 (Observabilidade)** — ✅ decidido: **adiada**; Prometheus + Grafana **fora do MVP** (Fase 2 a planejar). No MVP: logs (Serilog) + health checks + métricas expostas (OTLP) em B-39 · dashboards/alertas viraram **B-46** (Fase 2)
- [ ] **F.2** Modelar entidades F2/F3 no ER/Domínio (`SEGUE_TAG`, `FLAIR`, `LIVRO`, etc.) — **postergar** até o planejamento de cada Fase (a F2 ainda não foi planejada)
- [x] **F.3** **Carga de infra** — ✅ decidido: **infra é foco do MVP** — desenvolver bem CI/CD, ambientes, MSIX, backup e observabilidade-base (B-36..B-41) para facilitar o desenvolvimento futuro
- [x] **F.4** Libs de editor/feed — ✅ **AvaloniaEdit confirmado** · **Markdig mantido** como parser padrão (pesquisa 2026: alternativas no ecossistema Avalonia são wrappers de Markdig — `LiveMarkdown.Avalonia` open-source (Apache 2.0) vs `Avalonia.Controls.Markdown` pago/Pro). Pendência residual: validar renderer no spike (RNF-17)
- [ ] **F.5** Atualizar [[02 Modelagem/Wireframes|Wireframes]] com as novas telas (excluir conta, exportar dados, sugestões)
- [x] **F.6** Público-alvo — ✅ consolidado: **principais** jogadores, RPG de mesa, leitores, programadores · **secundários** música, cinema e similares · **distantes (possíveis)** ciclismo e afins — em [[01 Requisitos/Visão do Produto|Visão do Produto]] e [[01 Requisitos/Stakeholders e Personas|Personas]]
- [x] **F.7** Refresh token no desktop — ✅ decidido: **DPAPI** (escopo `CurrentUser`) → blob criptografado em `%LOCALAPPDATA%\Agora\secrets.dat`; access token só em memória. Virou [[03 Decisões/ADR-008 Segurança de Sessão (DPAPI)|ADR-008]] (fecha [[03 Decisões/Propostas Pendentes#P-002|P-002]])

---

Links: [[Home]] · [[VAULT/Plano - Rede Social de Nicho|Plano]] · [[Resumo dos Requisitos]] · [[04 Gestão/Backlog do Produto|Backlog]] · [[AGENTS.md|AGENTS]]