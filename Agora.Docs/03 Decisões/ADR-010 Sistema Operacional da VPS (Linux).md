---
tags: [decisoes, adr, infra]
tipo: adr
numero: ADR-010
data: 2026-08-30
status: aceita
atualizado: 2026-08-30
---

# ADR-010 — Sistema Operacional da VPS (Linux + Docker Compose)

## Contexto

O servidor roda em **VPS** ([[03 Decisões/ADR-002 Implantação|ADR-002]], [[03 Decisões/ADR-004 Ambientes|ADR-004]]) em **datacenter no Brasil** ([[03 Decisões/ADR-009 Residência dos Dados (BR)|ADR-009]]), com banco **PostgreSQL/Npgsql** ([[03 Decisões/ADR-007 Banco do Servidor (Npgsql)|ADR-007]]) e app **ASP.NET Core / .NET 10** ([[03 Decisões/ADR-001 Stack Tecnológica|ADR-001]]). Deploy é automatizado por **GitHub Actions** ([[04 Gestão/Backlog do Produto#B-37|B-37]]). A stack está definida, mas nunca foi decidido **qual SO rodar no host** — é a decisão que falta para provisionar staging/produção.

> [!important] Decisão da equipe (2026-08-30)
> Aprovado: **Linux (Ubuntu LTS 24.04) + Docker Compose** como SO/base de execução da VPS.

## Opções consideradas

| Opção | Prós | Contras |
|---|---|---|
| **A — Linux (Ubuntu LTS) + Docker Compose** | Custo 2–4× menor; PostgreSQL nativo; Kestrel/.NET 10 com suporte oficial; containers = padrão industrial; CI/CD trivial; overhead reduzido | Curva de administração p/ time de Windows (mitigada por automação) |
| **B — Windows Server** | GUI/RDP familiar; ferramentas exclusivas do ecossistema MS | Licença cara (2–4× o preço); overhead alto; Docker por WSL = fricção; SQL Server fora do escopo (ADR-007) |
| **C — Linux + instalação direta (sem Docker)** | Menos uma camada; visibilidade clássica do host | Provisionamento manual repetível; upgrades/composição entre app+DB mais frágeis; rollback mais difícil |

## Decisão

**Linux (Ubuntu LTS 24.04) + Docker Compose**: o app (Kestrel) e o **PostgreSQL** rodam como containers gerenciados por `docker compose`; staging e produção usam o mesmo padrão no provedor VPS do Brasil ([[03 Decisões/ADR-009 Residência dos Dados (BR)|ADR-009]]). Administração via SSH e tudo declarado em código (compose + workflows do GitHub Actions).

## Justificativa

1. **Custo**: para 20 betas, a economia de licença Windows financia um plano melhor (RAM/CPU) ou reduz custo operacional (alinhado a RNF-19 ambientes)
2. **Stack**: PostgreSQL e .NET/ASP.NET Core têm suporte de primeira classe no Linux (Microsoft oficial para .NET LTS; pg_dump/tooling nativos) — nada em ADR-001/007/005 pede Windows
3. **Containers**: composição app+DB, upgrade/rollback e reprodução em dev/staging/prod (RNF-19) ficam determinísticos; CI/CD (B-37) só faz `docker compose build/up`
4. **Overhead/segurança**: menor footprint; ufw/fail2ban/atualizações automáticas maduros no ecossistema
5. **Time de Windows**: a curva é absorvida pela automação — quase nenhuma operação manual além de SSH

## Consequências

- ➕ Custo e performance favoráveis; deploy/rollback determinísticos (compose + CI/CD)
- ➕ Backups via `pg_dump` no container com retenção ≤ 30 dias ([[01 Requisitos/Regras de Negócio#RN-11|RN-11]], [[04 Gestão/Backlog do Produto#B-52|B-52]])
- ➖ Time precisa de fluxos SSH/systemd/`docker compose` mínimos (mitigado por automação — B-37/B-41)
- 👁 Confirmar que o plano da VPS (ex.: 2 vCPU / 4 GB RAM) sustenta 20 betas + containers antes do provisionamento

## Referências

- [[03 Decisões/ADR-002 Implantação|ADR-002]] · [[03 Decisões/ADR-004 Ambientes|ADR-004]] — modelo e ambientes da infra
- [[03 Decisões/ADR-009 Residência dos Dados (BR)|ADR-009]] — provedor/datacenter no Brasil
- [[03 Decisões/ADR-007 Banco do Servidor (Npgsql)|ADR-007]] — PostgreSQL no servidor
- [[04 Gestão/Backlog do Produto#B-36|B-36]] · [[04 Gestão/Backlog do Produto#B-37|B-37]] — política de ambientes e pipeline CD
- [[04 Gestão/Operações e Deploy|Operações e Deploy]] — runbook operacional