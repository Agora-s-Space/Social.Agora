---
tags: [gestao, operacoes, sla]
tipo: sla
status: rascunho
atualizado: 2026-08-29
escopo: producao
meta_uptime: "99,5%"
periodo: mensal
---

# SLA de Disponibilidade

> Acordo de Nível de Serviço do **Agora**. Define a meta de disponibilidade do servidor de **produção**, como é medida e a **compensação** ao usuário caso não seja cumprida. Derivado de [[01 Requisitos/Requisitos Não Funcionais#RNF-24|RNF-24]].

## 1. Serviço coberto

| Campo | Valor |
|---|---|
| Serviço | API do servidor do Agora (produção) |
| Ambientes | **Somente produção** (dev e staging não entram na medição) |
| Horário de medição | Contínuo (24×7), calculado mensalmente |
| Meta | **≥ 99,5%** de uptime ao mês |

## 2. Como a disponibilidade é medida

- Medição por **health checks** + uptime monitoring em produção ([[01 Requisitos/Requisitos Não Funcionais#RNF-22|RNF-22]], [[04 Gestão/Backlog do Produto#B-39|B-39]]) e consolidada por [[04 Gestão/Backlog do Produto#B-47|B-47]].
- O servidor é considerado **indisponível** quando o endpoint de saúde (`/health`) responde com erro ou timeout pelo período contínuo mínimo registrado.
- O cálculo mensal é:
  - `uptime % = (tempo_total − tempo_indisponível) ÷ tempo_total × 100`
  - `tempo_indisponível` **exclui** as janelas de manutenção programadas (seção 3).
- Resultado apurado por relatório mensal de disponibilidade, revisado em [[04 Gestão/Operações e Deploy|Operações e Deploy]].

## 3. Exclusões (não contam contra a SLA)

- **Janelas de manutenção programadas**: períodos agendados e comunicados (ex.: janela noturna semanal), definidos com aviso prévio.
- Falhas de infraestrutura de terceiros fora do controle direto (ex.: indisponibilidade do provedor do VPS upstream).
- Indisponibilidade causada por uso abusivo ou ações de ataque/incidente de segurança em curso (com mitigação aplicada em tempo razoável).

## 4. Compensação ao usuário (créditos proporcionais)

Se a SLA não for cumprida no mês, o usuário recebe **créditos de acesso proporcionais ao uptime perdido**, convertidos em extensão de acesso a features premium futuras (Fase 2+).

| Uptime mensal apurado | Compensação |
|---|---|
| ≥ 99,5% | Sem compensação (SLA cumprida) |
| < 99,5% até 99,0% | **10%** de crédito mensal (extensão de acesso) |
| < 99,0% | **30%** de crédito mensal (extensão de acesso) |

> [!note] Forma de compensação
> O Agora é beta gratuito — não há fiança em dinheiro. O crédito é convertido em **extensão de acesso** a funcionalidades premium quando estas existirem (proposta P-001 [[03 Decisões/Propostas Pendentes|Microsoft Store]]/planos futuros). O percentual de crédito espelha a perda de disponibilidade: cada mês acima do limite gera crédito acumulado sobre o período pago/relevante.

Referência de mercado: o modelo de **créditos proporcionais** (percentual do período não atendido) é o padrão em SLA de SaaS — as faixas acima reproduzem essa lógica proporcional.

## 5. Fluxo de acionamento

1. Relatório mensal aponta que a meta de ≥ 99,5% não foi atingida.
2. Para cada usuário afetado, o crédito é calculado conforme a tabela da seção 4.
3. Créditos registrados e creditados quando o recurso premium correspondente estiver disponível.
4. Post-mortem do incidente documentado no vault (runbook/rollback em [[04 Gestão/Backlog do Produto#B-41|B-41]]).

## 6. Referências

- [[01 Requisitos/Requisitos Não Funcionais#RNF-24|RNF-24]] — requisito de origem (SLA ≥ 99,5%)
- [[01 Requisitos/Requisitos Não Funcionais#RNF-22|RNF-22]] — observabilidade/health checks
- [[04 Gestão/Backlog do Produto#B-39|B-39]] — observabilidade (MVP) · [[04 Gestão/Backlog do Produto#B-47|B-47]] — medição SLA
- [[04 Gestão/Operações e Deploy|Operações e Deploy]] — pipeline, ambientes e monitoramento
- [[03 Decisões/ADR-004 Ambientes|ADR-004]] — ambientes dev/staging/produção
- [[04 Gestão/Glossário]] — termos *SLA de disponibilidade*, *Uptime*, *Janela de manutenção*

---

Links: [[04 Gestão/Definição do MVP|Definição do MVP]] · [[04 Gestão/Operações e Deploy|Operações e Deploy]] · [[Resumo dos Requisitos]]