---
tags: [requisitos, rnf]
tipo: documento
status: rascunho
atualizado: 2026-08-30
publish: true
---

# Requisitos Não Funcionais (RNF)

> [!note] Critério SMART
> Cada RNF tem meta mensurável. Valores marcados ⚠️ são hipóteses a validar.

> [!note] Base normativa
> Os atributos de qualidade deste documento seguem o modelo de qualidade de produto **ISO/IEC 25010:2011 (SQuaRE)** — "atributos de qualidade (ISO/IEC)", conforme enunciado da primeira entrega. A tabela abaixo mapeia as categorias usadas às características do padrão.

| Categoria (nesta nota) | Característica ISO/IEC 25010 |
|---|---|
| Desempenho (RNF-01/02/03/25) | *Performance efficiency* |
| Usabilidade e acessibilidade · UI/Experiência (RNF-04/05/18) | *Usability* |
| Segurança (RNF-06/07/08) | *Security* |
| Privacidade e conformidade (RNF-09/26) | *Security* (confidencialidade) + conformidade legal (LGPD/GDPR) |
| Portabilidade e compatibilidade (RNF-10/11) | *Portability* + *Compatibility* |
| Manutenibilidade e qualidade (RNF-12/13/14) | *Maintainability* |
| Confiabilidade e operação (RNF-15/16/17) | *Reliability* (tolerância a falhas/autosave) + operação |
| Implantação e operação (RNF-19/20/21/22/23/24) | *Reliability* (availability/recoverability) + operação/suporte |

## Desempenho

| ID | Requisito | Meta |
|---|---|---|
| RNF-01 | Tempo de carregamento do feed (primeira página) | ≤ 2 s (P95) |
| RNF-02 | Inicialização do app até tela de login/feed | ≤ 3 s em HDD comum ⚠️ |
| RNF-03 | Ações (curtir, comentar, seguir) com feedback visual | ≤ 200 ms |
| RNF-25 | Throughput do servidor — taxa de requisições por segundo sustentada | **≥ 50 req/s** sustentados (cenário de leitura: feed, busca, detalhe de post), **sem degradar RNF-01/03**; validado por teste de carga automatizado no CI (ferramenta candidata: **NBomber** ⚠️) |

## Usabilidade e acessibilidade

| ID | Requisito |
|---|---|
| RNF-04 | Navegação completa possível via teclado (Tab/Enter/setas) — 100% das ações acessíveis sem mouse |
| RNF-05 | Contraste mínimo WCAG AA (ratio ≥ 4.5:1 para texto normal); suporte a temas claro/escuro |

## Segurança

| ID | Requisito | Meta |
|---|---|---|
| RNF-06 | Senhas armazenadas com hash adaptativo (bcrypt/PBKDF2/Argon2), nunca em texto puro | Custo mínimo **bcrypt cost ≥ 10** (OWASP) |
| RNF-07 | Comunicação cliente-servidor sempre via TLS 1.2+ | — |
| RNF-08 | Proteção contra força bruta (limitação de tentativas de login) | **≤ 5 tentativas falhas → bloqueio ≥ 15 min** (backoff) |

## Privacidade e conformidade

| ID | Requisito | Meta |
|---|---|---|
| RNF-09 | Conformidade LGPD: consentimento, exportação e exclusão de dados do usuário ([[01 Requisitos/Regras de Negócio\|RN-04]]) | Solicitações (exportar/excluir) atendidas em **≤ 15 dias** (LGPD art. 19) |
| RNF-26 | Alinhamento com princípios GDPR (minimização de dados, limitação de armazenamento, accountability, notificação de violação) como boas práticas — GDPR não aplicável hoje (ver [[01 Requisitos/LGPD e Privacidade#1. Leis aplicáveis\|LGPD e Privacidade]]) | Registro de consentimento (data/versão) no cadastro; retenção: anonimizar dados ≤ 30 d · backups ≤ 30 d; notificação de violação ≤ 72 h da ciência ⚠️ |

## Portabilidade e compatibilidade

| ID | Requisito | Meta |
|---|---|---|
| RNF-10 | Windows 10+ como alvo primário; arquitetura não pode impedir Linux/macOS futuros ([[03 Decisões/ADR-001 Stack Tecnológica\|ADR-001]]) | — (restrição arquitetural) |
| RNF-11 | .NET LTS (10+) como runtime | **.NET 10 LTS**; suporte garantido até **nov/2028** |

## Manutenibilidade e qualidade

| ID     | Requisito                                                                                               | Meta                                                                                     |
| ------ | ------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------- |
| RNF-12 | Código em camadas ([[02 Modelagem/Arquitetura do Sistema\|Arquitetura]]): UI, Aplicação, Domínio, Infra | **0 violações de dependência** entre camadas — verificado por teste de arquitetura no CI |
| RNF-13 | Cobertura de testes ≥ 60% no núcleo de domínio ⚠️                                                       | ≥ 60% no núcleo de domínio ⚠️                                                            |
| RNF-14 | CI executando build + testes a cada push                                                                | Build + testes **≤ 10 min ⚠️**                                                           |

## Confiabilidade e operação

| ID | Requisito |
|---|---|
| RNF-15 | Falha de rede não perde rascunho de post em edição (autosave local) |
| RNF-16 | Logs estruturados sem dados sensíveis |
| RNF-17 | Renderização de blocos de código com syntax highlighting completa em ≤ 100 ms (P95) para blocos de até 50 linhas |

## Implantação e operação

| ID     | Requisito                                                                                                                       | Meta                                                                                                                                                                                                       |                                                  |
| ------ | ------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ------------------------------------------------ |
| RNF-19 | Múltiplos ambientes (dev / staging / produção) isolados                                                                         | 3 ambientes ([[03 Decisões/ADR-004 Ambientes\|ADR-004]])                                                                                                                                                   |                                                  |
| RNF-20 | Deploy automatizado com pipeline (CI/CD)                                                                                        | Deploy p/ staging automático; prod via aprovação                                                                                                                                                           |                                                  |
| RNF-21 | Backup e restauração do banco de produção                                                                                       | Backup diário; restore testado                                                                                                                                                                             |                                                  |
| RNF-22 | Observabilidade: logs, métricas, health checks, alertas                                                                         | Logs estruturados (Serilog) + health checks no MVP; métricas expostas em formato Prometheus (OTLP); dashboards/alertas (Prometheus + Grafana) na Fase 2 — [[03 Decisões/ADR-006 Observabilidade\|ARD-006]] | [[03 Decisões/ADR-006 Observabilidade\|ARD-006]] |
| RNF-23 | Empacotamento/distribuição do app desktop — formato **MSIX** (compatível com sideload e possível publicação na Microsoft Store) | Installer MSIX + identidade de pacote; atualização via Store (opcional)                                                                                                                                    |                                                  |
| RNF-24 | SLA de disponibilidade do servidor — uptime mensal da **produção** | **≥ 99,5%** ao mês; **exclui janelas de manutenção programadas** (agendadas e comunicadas); medido via health checks/uptime monitoring (RNF-22); detalhes e compensação em [[04 Gestão/SLA de Disponibilidade\|SLA de Disponibilidade]] — escopo: produção apenas |                                                  |

## UI / Experiência

| ID | Requisito | Meta |
|---|---|---|
| RNF-18 | Animações e transições de tela (splash, loading, transições entre telas) | 60 fps; splash ≤ 3s; transições ≤ 300 ms |

## Verificações de consistência

### RNF → RF/RN (quando aplicável)

| RNF | Impacta | OK? |
|---|---|:-:|
| RNF-06 | RN-03 (hash de senha) | ✅ |
| RNF-08 | RF-002 (proteção login) | ✅ |
| RNF-09 | RN-04 (LGPD) | ✅ |
| RNF-26 | RF-034 (consentimento), RN-11/RN-12 (retenção/violação) | ✅ |
| RNF-14 | B-09 (CI) | ✅ |
| RNF-15 | RF-004 (rascunho local) | ✅ |
| RNF-17 | RF-022 (syntax highlighting) | ✅ |

### Metas mensuráveis

| RNF | Meta | Mensurável? |
|---|---|:-:|
| RNF-01 | ≤ 2 s (P95) feed | ✅ |
| RNF-02 | ≤ 3 s inicialização ⚠️ | ✅ (estimativa) |
| RNF-03 | ≤ 200 ms feedback | ✅ |
| RNF-04 | 100% ações via teclado | ✅ |
| RNF-05 | ratio ≥ 4.5:1 (WCAG AA) | ✅ |
| RNF-06 | bcrypt cost ≥ 10 | ✅ |
| RNF-08 | ≤ 5 tentativas → bloqueio ≥ 15 min | ✅ |
| RNF-09 | Solicitações LGPD (exportar/excluir) ≤ 15 dias | ✅ |
| RNF-26 | Consentimento registrado (data/versão); retenção ≤ 30 d (dados/backups); notificação ≤ 72 h ⚠️ | ✅ |
| RNF-11 | .NET 10 LTS; suporte até nov/2028 | ✅ |
| RNF-12 | 0 violações de dependência entre camadas | ✅ |
| RNF-13 | ≥ 60% cobertura domínio ⚠️ | ✅ (estimativa) |
| RNF-14 | Build + testes ≤ 10 min ⚠️ | ✅ (estimativa) |
| RNF-17 | ≤ 100 ms (P95, ≤50 linhas) | ✅ |
| RNF-19 | 3 ambientes (dev/staging/prod) | ✅ |
| RNF-20 | Deploy p/ staging automático; prod via aprovação | ✅ |
| RNF-21 | Backup diário; restore testado | ✅ |
| RNF-22 | Health check + alerta em falha | ✅ |
| RNF-23 | Pacote MSIX gera install + sideload | ✅ |
| RNF-24 | ≥ 99,5% uptime mensal (produção); janelas de manutenção excluídas | ✅ |
| RNF-25 | ≥ 50 req/s sustentados; sem degradar RNF-01/03 | ✅ |

### Contadores

| Métrica | Valor |
|:--|:-:|
| **Total RNFs** | **26** |

---
Links: [[01 Requisitos/Requisitos Funcionais|RF]] · [[01 Requisitos/Regras de Negócio|RN]] · [[02 Modelagem/Arquitetura do Sistema|Arquitetura]]
