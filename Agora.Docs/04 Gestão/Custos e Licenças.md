---
tags: [gestao, operacoes, custos, licencas]
tipo: documento
status: rascunho
atualizado: 2026-08-30
---

# Custos e Licenças

> Visão consolidada dos custos do **Agora**: licenças de software, infraestrutura e distribuição. A stack é **100% open source** — o único custo obrigatório é o **VPS no Brasil** ([[03 Decisões/ADR-009 Residência dos Dados (BR)|ADR-009]] + [[03 Decisões/ADR-010 Sistema Operacional da VPS (Linux)|ADR-010]]).

## 1. Resumo executivo

| Item | Custo |
|---|---|
| Licenças de software da stack | **R$ 0** (tudo open source) |
| VPS no Brasil (dev/staging/produção) | **Único custo mensal obrigatório** (~R$ 30–150 p/ 20 betas ⚠️) |
| Distribuição aos 20 betas (sideload MSIX) | **R$ 0** (cert self-signed) |
| Publicação na Microsoft Store (opcional) | **R$ 0** (conta gratuita desde 2025; Store assina) |
| Assinatura de código p/ distribuição pública fora da Store | Opcional: OV ~US$ 150–500/ano **ou** Azure Artifact Signing ~US$ 10/mês |

## 2. Licenças da stack — todas gratuitas

| Componente | Licença | Custo |
|---|---|---|
| .NET 10 / ASP.NET Core / EF Core | MIT | R$ 0 |
| Avalonia UI · CommunityToolkit.Mvvm | MIT | R$ 0 |
| SQLite (client) | Domínio público | R$ 0 |
| PostgreSQL · Npgsql | Licença PostgreSQL / MIT | R$ 0 |
| Serilog · Prometheus · Docker Engine · Ubuntu LTS | Apache-2.0 / AGPL (self-host) | R$ 0 |
| GitHub Actions | Tier gratuito (privado: 2.000 min/mês) | R$ 0 (time pequeno) |

> [!note] Por que não SQL Server
> Escolhido **PostgreSQL/Npgsql** ([[03 Decisões/ADR-007 Banco do Servidor (Npgsql)|ADR-007]]): SQL Server Express é gratuito mas limitado (10 GB / CPU); SQL Server Standard é pago por core. O Postgres elimina esse custo de licença.

## 3. Infraestrutura (custo real)

| Item | Obrigatório? | Estimativa mensal |
|---|---|---|
| VPS no Brasil (Ubuntu LTS + Docker Compose) | ✅ Sim — [[03 Decisões/ADR-009 Residência dos Dados (BR)\|ADR-009]]/[[03 Decisões/ADR-010 Sistema Operacional da VPS (Linux)\|ADR-010]] | ~R$ 30–150 ⚠️ (depende do provedor) |
| SSL/TLS (HTTPS — RNF-07) | Sim | **R$ 0** (Let's Encrypt) |
| Banco de dados | Compartilha o VPS (container) | **R$ 0** |
| Domínio/DNS | **A definir** (não decidido) | — |

## 4. Distribuição

| Caminho | Custo |
|---|---|
| **Sideload aos 20 betas** (MSIX, B-40) | **R$ 0** — cert **self-signed** + sideload (cada beta confia no `.cer` uma vez, ou Dev Mode) |
| **Microsoft Store** (opcional/futuro, B-40) | **R$ 0** — conta Partner Center **gratuita** (Individual e Company desde 2025, sem taxa de registro); Store **assina** o MSIX |
| Distribuição pública **fora** da Store (S/O) | Opcional pago: **OV cert** (~US$ 150–500/ano) **ou Azure Artifact Signing** (~US$ 10/mês; disponível p/ individual só EUA/Canadá ⚠️) |

> [!note] Ferramentas de empacotamento
> MSIX Packaging Tools e Windows SDK são **gratuitos** ([[04 Gestão/Operações e Deploy#5. Empacotamento e distribuição|Operações e Deploy §5]]).

## 5. Custo mensal estimado

| Cenário | Mensal |
|---|---|
| Mínimo (VPS pequeno, 20 betas, sideload) | **~R$ 30–60** ⚠️ |
| Típico (VPS médio + reserva) | **~R$ 80–150** ⚠️ |
| Publicação pública fora da Store | + ~US$ 10/mês (Artifact Signing) ou ~US$ 150–500/ano (OV) |

> Valores são estimativas ⚠️ — a validar na cotação real do provedor na Fase 1 ([[04 Gestão/Backlog do Produto#B-37|B-37]]).

## 6. Referências

- [[03 Decisões/ADR-009 Residência dos Dados (BR)|ADR-009]] — datacenter no Brasil (residência dos dados)
- [[03 Decisões/ADR-010 Sistema Operacional da VPS (Linux)|ADR-010]] — Ubuntu LTS + Docker Compose
- [[04 Gestão/Operações e Deploy|Operações e Deploy]] — ambientes, pipeline, empacotamento (§5)
- [[01 Requisitos/Requisitos Não Funcionais#RNF-23|RNF-23]] — empacotamento MSIX · [[01 Requisitos/Requisitos Não Funcionais#RNF-24|RNF-24]] — SLA
- [[04 Gestão/Verificação da Primeira Entrega|Verificação da Primeira Entrega]] — ferramentas confirmadas × candidatas

---

Links: [[04 Gestão/Operações e Deploy|Operações e Deploy]] · [[03 Decisões/ADR-010 Sistema Operacional da VPS (Linux)|ADR-010]] · [[Resumo dos Requisitos]]