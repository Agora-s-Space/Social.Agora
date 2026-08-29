---
tags: [decisoes, adr, persistencia, banco]
tipo: adr
numero: ADR-007
data: 2026-08-28
status: aceita
atualizado: 2026-08-28
---

# ADR-007 — Banco do servidor: PostgreSQL (Npgsql)

## Contexto

[[03 Decisões/ADR-003 Persistência|ADR-003]] (aceita) definiu **EF Core 10** como ORM, mas deixou em aberto o provedor do banco do **servidor** ("Npgsql **ou** SqlServer"). O produto precisa de um provedor único para: migrações determinísticas, recursos de busca textual ([[01 Requisitos/Requisitos Funcionais#RF-010|RF-010]]/[[01 Requisitos/Requisitos Funcionais#RF-017|RF-017]]) e custo operacional previsível no VPS ([[03 Decisões/ADR-002 Implantação|ADR-002]]/[[03 Decisões/ADR-004 Ambientes|ADR-004]]).

## Opções consideradas

| Opção | Prós | Contras |
|---|---|---|
| **A — PostgreSQL (Npgsql)** | FTS nativo (tsvector/GIN) para RF-010/017; licença gratuita; roda no mesmo VPS; padrão de mercado | Curva do time (mitigável) |
| **B — SQL Server** | Mesmo ecossistema .NET | Licença; FTS limitado no plano gratuito; mais pesado para VPS pequeno |
| **C — Npgsql + SqlServer (duplo)** | Flexibilidade | Duplica migrações/testes/ops — contradiz simplicidade da ADR-002 |

## Decisão

**PostgreSQL via `Npgsql.EntityFrameworkCore.PostgreSQL`** como provedor do banco do **servidor** (fonte da verdade — ADR-002). SQL Server fica **fora do escopo**.

## Justificativa

1. FTS nativo cobre [[01 Requisitos/Requisitos Funcionais#RF-010|RF-010]]/[[01 Requisitos/Requisitos Funcionais#RF-017|RF-017]] sem serviço externo (índice GIN em `tsvector`)
2. Custo zero de licença; roda no mesmo VPS dos ambientes (ADR-004)
3. Um provedor só → uma suíte de migrações e testes ([[01 Requisitos/Requisitos Não Funcionais#RNF-14|RNF-14]])
4. Ecossistema .NET maduro (`Npgsql`)

## Consequências

- ➕ Busca textual via `tsvector` + índice GIN
- ➕ Migrações/testes em um único SGBD no servidor
- ➖ SQL Server deixa de ser suportado (fim da ambiguidade da ADR-003)
- 👁 Portabilidade de tipos entre SQLite (client) e Postgres (server) — obrigatório manter schema compatível (ADR-003)

## Referências

- [[03 Decisões/ADR-003 Persistência|ADR-003]] — ORM (EF Core 10) + papel do SQLite no client
- [[02 Modelagem/Modelo de Dados (ER)|ER]] — schema relacional (FTS em POST)
- [[02 Modelagem/API e Servidor|API e Servidor]] — contrato do servidor
- [[04 Gestão/Operações e Deploy|Operações e Deploy]] — ambientes e backup