---
tags: [gestao, publicacao, site]
tipo: processo
status: ativo
atualizado: 2026-08-30
---

# Publicação do Site (Social.Quartz) — Pendências

> [!note] Estado
> O site está no ar em **https://agora-s-space.github.io/Social.Quartz/** (Quartz v5 → GitHub Pages). A publicação é **opcional**: notas só entram no site com `publish: true` no frontmatter (plugin `explicit-publish`). Fases 1 e 2 abaixo já estão **concluídas**.

## Automação Agora → Quartz

**Como funciona (quando tudo estiver pronto):** push na `main` do Agora → workflow de sync roda → filtra notas com `publish: true` → faz commit na `main` do Social.Quartz → `deploy.yml` reconstrói o site.

- [x] **Fase 1 — Padronização no vault**: 22 notas da entrega com `publish: true` (PR #4 no Agora-lei `docs/revisao-entrega-1`)
- [x] **Fase 2 — Estrutura no Quartz**: `scripts/sync-content.mjs` (`npm run sync`), plugin `explicit-publish` ativo, build local validado, CI corrigido (CLI via `node ./quartz/bootstrap-cli.mjs`)
- [ ] **Merge do PR #4** na `main` do Agora (sem isso, a main não carrega o `publish: true`)
- [ ] **Fase 3 — Token de sync**: criar fine-grained PAT `agora-docs-sync` — acesso **só** ao `Social.Quartz`, permissão **Contents: Read and write**, owner `Agora-s-Space` (por status admin, é aprovado na hora). Guardar como secret **`SYNC_PAT`** no repo Social.Agora → `gh secret set SYNC_PAT -R Agora-s-Space/Social.Agora`
- [ ] **Fase 4 — Workflow no Agora**: `.github/workflows/sync-content.yml` (`on: push: branches: [main]`): clona o Quartz com o PAT, roda o sync, `commit`+`push` na `main` do Quartz só se houver mudança (autor bot, sem `--force`, `pull --rebase` antes do push)
- [ ] **Fase 5 — Teste ponta-a-ponta**: disparo manual + edição real numa nota → confirmar commit automático no Quartz em ~1 min
- [ ] **Lembrete recorrente**: renovar o PAT a cada **90 dias** (fine-grained expira) — registrar na agenda/tarefa quando faltar ~15 dias

## Pendências antigas do vault

- [ ] **RNF-13**: trocar a estimativa ⚠️ por nota de confirmação em cascata — arquivos afetados: [[01 Requisitos/Requisitos Não Funcionais|RNF]], [[04 Gestão/Definição do MVP|Definição do MVP]], [[04 Gestão/Verificação da Primeira Entrega]], [[Resumo dos Requisitos]], `AGENTS.md`, [[Documento de Especificação]]

## Referências

- Repo do site → Social.Quartz (public), Pages: `agora-s-space.github.io/Social.Quartz`
- Script de sync: `Social.Quartz/scripts/sync-content.mjs`
- Regra de publicação: `publish: true` no frontmatter (sem a prop → não publica)