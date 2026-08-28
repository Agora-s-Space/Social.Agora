---
tags: [decisoes, adr]
tipo: adr
numero: ADR-006
status: proposta
data: 2026-08-27
---

# ADR-006 — Observabilidade: Métricas da API

## Contexto
- A API do Agora ([[03 Decisões/ADR-005 API do Servidor|ADR-005]]) é uma ASP.NET Core Web API rodando em VPS nos ambientes staging e produção ([[03 Decisões/ADR-004 Ambientes|ADR-004]])
- [[01 Requisitos/Requisitos Não Funcionais#RNF-22|RNF-22]] exige observabilidade (logs, métricas, health checks, alertas); item **Must** do MVP em [[04 Gestão/Backlog do Produto#B-39|B-39]]
- Logs estruturados já decididos (Serilog — [[01 Requisitos/Requisitos Não Funcionais#RNF-16|RNF-16]]); falta decidir **como medir/expor métricas** e **onde visualizar e alertar**
- Time pequeno, sem custo recorrente desejado — avaliar manter tudo no VPS (sem SaaS) é preferível
- Escopo: **servidor**. Crash reporting do desktop (Avalonia) fica com Sentry (proposto) — fora desta ADR

## Camadas envolvidas
Métrica não é uma ferramenta só: tem **instrumentação** (gerar os números), **exposição** (entregar num formato coletável) e **armazenamento/visualização/alertas**. A ADR resolve as três.

## Opções consideradas
| Opção | Prós | Contras |
|---|---|---|
| **Métricas nativas** `System.Diagnostics.Metrics` + export via **OpenTelemetry** | Padrão .NET; overhead mínimo; vendor-neutral (OTLP/Prometheus); zero dep p/ medir; meters próprios de negócio | Só *gera* dado — precisa de exporter + backend p/ ver e alertar |
| **prometheus-net** (biblioteca) | Simples; `/metrics` direto no processo; dev local fácil | Amarra o app ao formato Prometheus; só expõe, não armazena/alert |
| **Prometheus + Grafana** no VPS | Padrão de mercado; PromQL poderoso; alerting embutido (fecha RNF-22); dashboards | 2 serviços novos p/ operar; curva PromQL; endpoint precisa ser acessível (firewall) |
| **SaaS** (Application Insights, Datadog…) | Zero infra própria; rico em recursos; integração simples | Custo recorrente; dados fora do VPS; dependência de fornecedor |

## Decisão
Instrumentar a API com `System.Diagnostics.Metrics` (métricas nativas do ASP.NET Core + meters próprios de negócio), expor via **OpenTelemetry** e operar **Prometheus + Grafana** no VPS de staging/produção para coleta, dashboards e alertas.

*Status: proposta — aguardando aceite.*

## Justificativa
- [[01 Requisitos/Requisitos Não Funcionais#RNF-22|RNF-22]]/[[04 Gestão/Backlog do Produto#B-39|B-39]]: Prometheus + Grafana entregam métricas, visualização e alertas numa stack só
- Custo operacional baixo: rodam no **mesmo VPS** dos ambientes ([[03 Decisões/ADR-004 Ambientes|ADR-004]]) — sem SaaS
- Vendor-neutral via OTLP: trocar de backend no futuro não exige reescrever a instrumentação
- Desempenho: meters nativos têm overhead mínimo (suporta RNF-01/RNF-03)

## Consequências
**Positivas:**
- Métricas de negócio padronizadas desde a Fase 1 (feed, ações) — ligadas a [[01 Requisitos/Requisitos Não Funcionais#RNF-01|RNF-01]] e [[01 Requisitos/Requisitos Não Funcionais#RNF-03|RNF-03]]
- Alertas configuráveis (5xx, P95 de latência) via Grafana — fecha [[01 Requisitos/Requisitos Não Funcionais#RNF-22|RNF-22]]

**Negativas:**
- Operar 2 serviços extras no VPS; definir retention do Prometheus p/ controlar disco
- Curva de aprendizado PromQL (mitigável com dashboards padrão)

**Neutras:**
- Health checks (já propostos em [[04 Gestão/Operações e Deploy|Operações e Deploy]]) seguem separados p/ uptime; métricas cobrem performance

**A monitorar:**
- Consumo de disco do Prometheus (retention)
- Latência/volume por endpoint na Fase 1

---

Links: [[03 Decisões/ADR-004 Ambientes|ADR-004]] · [[03 Decisões/ADR-005 API do Servidor|ADR-005]] · [[01 Requisitos/Requisitos Não Funcionais|RNF]] · [[04 Gestão/Operações e Deploy|Operações e Deploy]]