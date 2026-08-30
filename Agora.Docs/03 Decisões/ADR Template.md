---
tags: [decisoes, adr]
tipo: template
status: ativo
atualizado: 2026-08-24
---

# ADR Template — Registro de Decisão de Arquitetura

```markdown
---
tags: [decisoes, adr]
tipo: adr
numero: ADR-XXX
status: proposta | aceita | adiada | substituída por [[ADR-YYY]] | rejeitada
data: AAAA-MM-DD
---

# ADR-XXX — Título da decisão

## Contexto
Qual problema/força motriz exige uma decisão agora?

## Opções consideradas
| Opção | Prós | Contras |
|---|---|---|

## Decisão
Escolha feita, em uma frase.

## Justificativa
Critérios usados (requisitos RNF-xx, custo, prazo...).

## Consequências
Positivas · Negativas · Neutras — e o que monitorar.
```

## Índice de ADRs
- [[03 Decisões/ADR-001 Stack Tecnológica|ADR-001 — Stack Tecnológica (UI e runtime)]] — `aceita`
- [[03 Decisões/ADR-002 Implantação|ADR-002 — Modelo de Implantação]] — `aceita`
- [[03 Decisões/ADR-003 Persistência|ADR-003 — Persistência (ORM)]] — `aceita`
- [[03 Decisões/ADR-004 Ambientes|ADR-004 — Ambientes (dev/staging/prod)]] — `aceita`
- [[03 Decisões/ADR-005 API do Servidor|ADR-005 — Design da API do Servidor]] — `aceita`
- [[03 Decisões/ADR-006 Observabilidade|ADR-006 — Observabilidade: Métricas da API]] — `adiada`
- [[03 Decisões/ADR-007 Banco do Servidor (Npgsql)|ADR-007 — Banco do Servidor (PostgreSQL/Npgsql)]] — `aceita`
- [[03 Decisões/ADR-008 Segurança de Sessão (DPAPI)|ADR-008 — Segurança de Sessão (DPAPI)]] — `aceita`
- [[03 Decisões/ADR-009 Residência dos Dados (BR)|ADR-009 — Residência dos Dados (datacenter no Brasil)]] — `aceita`
- [[03 Decisões/ADR-010 Sistema Operacional da VPS (Linux)|ADR-010 — S.O. da VPS (Linux + Docker Compose)]] — `aceita`

## Propostas pendentes
- [[03 Decisões/Propostas Pendentes|Propostas Pendentes]] — decisões aguardando escolha

## Regras
1. ADRs são **imutáveis**: mudou o contexto → novo ADR marcando o anterior como *substituído*
2. Numerados sequencialmente, sem reutilizar números
3. Status só muda entre `proposta → aceita` ou `→ rejeitada/substituída`
