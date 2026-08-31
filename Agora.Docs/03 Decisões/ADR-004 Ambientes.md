---
tags: [decisoes, adr, ambientes, deploy]
tipo: adr
numero: ADR-004
data: 2026-08-27
status: aceita
atualizado: 2026-08-27
publish: true
---

# ADR-004 — Ambientes: dev / staging / produção

## Contexto
O desenvolvimento de **Agora** precisa de um fluxo de entrega que separe ambientes para evitar impactar usuários reais e validar mudanças antes de ir a produção (RNF-14 CI). ADR-002 decide cliente-servidor com um VPS. É preciso definir como o código flui entre ambientes e como garantir consistência entre eles.

## Opções consideradas

| Opção | Prós | Contras |
|---|---|---|
| **A — Ambiente único (só produção)** | Simples; sem overhead | Qualquer commit vai a produção; sem validação; risco alto para bugs |
| **B — dev + produção** | Separa desenvolvimento de uso real | Sem ambiente de validação intermediário; staging ruim |
| **C — dev / staging / produção** | Validação intermediária; espelha produção; detecção de problemas antes do deploy | Custo de 2 ambientes servidos; mais infra para manter |

## Decisão
**C — dev / staging / produção.** Três ambientes separados, com dados e configuração distintos por ambiente.

## Justificativa
1. **Rede social multiusuário** exige validação segura antes de tocar usuários reais (RF-015 moderação, RNF-09 LGPD)
2. Staging espelha produção (mesma stack/config) → catch de problemas antes do release
3. Equipe pequena → um staging compartilhado basta; não precisa de um por dev (CD a partir da branch principal)
4. Dev local: cada dev roda localmente (SQLite local, API local); staging e prod no VPS (Npgsql — ADR-007)
5. Ambientes isolados evitam que dados de teste/poluição cheguem aos 20 betas (OKR O2)

## Consequências
- ➕ Validação intermediária → menos bugs em produção
- ➕ Configuração por ambiente (connection string, secrets, feature flags)
- ➖ Dois ambientes servidos no VPS (staging + prod) → custo/recursos
- ➖ Processo de promoção dev→staging→prod precisa ser documentado (novo B-item)
- 👁 Monitorar: custo mensal do VPS com 2 instâncias; garantir segmentation de dados (nunca usar dados reais em staging)

## Referências
- [[03 Decisões/ADR-002 Implantação|ADR-002]] — modelo cliente-servidor
- [[03 Decisões/ADR-007 Banco do Servidor (Npgsql)|ADR-007]] — provedor do banco (staging/prod)
- [[01 Requisitos/Requisitos Não Funcionais#RNF-14|RNF-14]] — CI
- [[04 Gestão/Backlog do Produto#B-36|B-36]] — política de ambientes
- [[04 Gestão/Operações e Deploy|Operações e Deploy]]
