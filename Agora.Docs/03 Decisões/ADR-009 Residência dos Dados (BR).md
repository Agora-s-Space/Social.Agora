---
tags: [decisoes, adr, privacidade, infra]
tipo: adr
numero: ADR-009
data: 2026-08-29
status: aceita
atualizado: 2026-08-29
publish: true
---

# ADR-009 — Residência dos dados do servidor (datacenter no Brasil)

## Contexto

O servidor roda em **VPS** ([[03 Decisões/ADR-002 Implantação|ADR-002]], [[03 Decisões/ADR-004 Ambientes|ADR-004]]) com banco **PostgreSQL/Npgsql** ([[03 Decisões/ADR-007 Banco do Servidor (Npgsql)|ADR-007]]). O produto é brasileiro e processa **dados pessoais** de usuários ([[01 Requisitos/LGPD e Privacidade|LGPD]]) — é preciso decidir **onde** os dados do servidor (banco, backups, logs) residem fisicamente, pois afeta conformidade, latência e riscos jurídicos.

## Opções consideradas

| Opção | Prós | Contras |
|---|---|---|
| **A — Datacenter no Brasil (VPS BR)** | Menor latência p/ público BR; atende LGPD sem transferência internacional; alinhado à base de usuários (UNA-Contagem) | *(nenhum impacto operacional relevante para o volume de 20 betas)* |
| **B — Datacenter na UE** | Alinhado a GDPR | Transferência internacional hoje desnecessária; latência maior; custo/fornecedor fora do escopo |
| **C — Brasil + réplica na UE** | Resiliência maior; prontidão GDPR | Custo e complexidade desproporcionais ao MVP; transferência internacional exige cláusulas (art. 46 LGPD) |

## Decisão

**Servidor (banco, backups e logs de produção) hospedado em datacenter no Brasil**, escolhido pelo provedor de VPS no provisionamento dos ambientes staging/produção ([[03 Decisões/ADR-004 Ambientes|ADR-004]]).

## Justificativa

1. **LGPD** é a lei aplicável (público brasileiro) — residência no Brasil evita transferência internacional de dados (LGPD art. 15/33)
2. **Latência** menor para os 20 betas brasileiros (alinhado a RNF-01 feed ≤ 2 s P95)
3. **GDPR** não aplicável hoje (sem titulares/estabelecimento na UE) — ver [[01 Requisitos/LGPD e Privacidade#1. Leis aplicáveis|Nota de Privacidade]]; se mudar, reavaliar (nova ADR)
4. Custo/fornecedor: múltiplos VPS-br (ex.: locais da Hostinger/Contabo/AWS São Paulo) cabem no escopo

## Consequências

- ➕ Conformidade LGPD simplificada (dados permanecem em território nacional)
- ➕ Latência reduzida ao público-alvo (RNF-01)
- ➖ Restringe provedores aos que possuem datacenter no Brasil ⚠️ (não blocante — múltiplas opções)
- 👁 Na expansão para UE, decidir **nova ADR** (transferência internacional / representante GDPR)

## Referências

- [[01 Requisitos/LGPD e Privacidade]] — política de privacidade e aplicabilidade legal
- [[03 Decisões/ADR-002 Implantação|ADR-002]] · [[03 Decisões/ADR-004 Ambientes|ADR-004]] — modelo e ambientes da infra
- [[03 Decisões/ADR-007 Banco do Servidor (Npgsql)|ADR-007]] — banco do servidor
- [[01 Requisitos/Requisitos Não Funcionais#RNF-07|RNF-07]] — TLS (segurança em trânsito)