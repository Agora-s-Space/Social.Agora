---
tags: [requisitos, rnf]
tipo: documento
status: rascunho
atualizado: 2026-08-25
---

# Requisitos Não Funcionais (RNF)

> [!note] Critério SMART
> Cada RNF tem meta mensurável. Valores marcados ⚠️ são hipóteses a validar.

## Desempenho

| ID | Requisito | Meta |
|---|---|---|
| RNF-01 | Tempo de carregamento do feed (primeira página) | ≤ 2 s (P95) |
| RNF-02 | Inicialização do app até tela de login/feed | ≤ 3 s em HDD comum ⚠️ |
| RNF-03 | Ações (curtir, comentar, seguir) com feedback visual | ≤ 200 ms |

## Usabilidade e acessibilidade

| ID | Requisito |
|---|---|
| RNF-04 | Navegação completa possível via teclado (Tab/Enter/setas) — 100% das ações acessíveis sem mouse |
| RNF-05 | Contraste mínimo WCAG AA (ratio ≥ 4.5:1 para texto normal); suporte a temas claro/escuro |

## Segurança

| ID | Requisito |
|---|---|
| RNF-06 | Senhas armazenadas com hash adaptativo (bcrypt/PBKDF2/Argon2), nunca em texto puro |
| RNF-07 | Comunicação cliente-servidor sempre via TLS 1.2+ |
| RNF-08 | Proteção contra força bruta (limitação de tentativas de login) |

## Privacidade e conformidade

| ID | Requisito |
|---|---|
| RNF-09 | Conformidade LGPD: consentimento, exportação e exclusão de dados do usuário ([[01 Requisitos/Regras de Negócio|RN-04]]) |

## Portabilidade e compatibilidade

| ID | Requisito |
|---|---|
| RNF-10 | Windows 10+ como alvo primário; arquitetura não pode impedir Linux/macOS futuros ([[03 Decisões/ADR-001 Stack Tecnológica|ADR-001]]) |
| RNF-11 | .NET LTS (10+) como runtime |

## Manutenibilidade e qualidade

| ID | Requisito |
|---|---|
| RNF-12 | Código em camadas ([[02 Modelagem/Arquitetura do Sistema|Arquitetura]]): UI, Aplicação, Domínio, Infra |
| RNF-13 | Cobertura de testes ≥ 60% no núcleo de domínio ⚠️ |
| RNF-14 | CI executando build + testes a cada push |

## Confiabilidade e operação

| ID | Requisito |
|---|---|
| RNF-15 | Falha de rede não perde rascunho de post em edição (autosave local) |
| RNF-16 | Logs estruturados sem dados sensíveis |
| RNF-17 | Renderização de blocos de código com syntax highlighting completa em ≤ 100 ms (P95) para blocos de até 50 linhas |

## Implantação e operação

| ID | Requisito | Meta |
|---|---|---|
| RNF-19 | Múltiplos ambientes (dev / staging / produção) isolados | 3 ambientes ([[03 Decisões/ADR-004 Ambientes\|ADR-004]]) |
| RNF-20 | Deploy automatizado com pipeline (CI/CD) | Deploy p/ staging automático; prod via aprovação |
| RNF-21 | Backup e restauração do banco de produção | Backup diário; restore testado |
| RNF-22 | Observabilidade: logs, métricas, health checks, alertas | Logs estruturados (Serilog) + health checks no MVP; métricas expostas em formato Prometheus (OTLP); dashboards/alertas (Prometheus + Grafana) na Fase 2 — [[03 Decisões/ADR-006 Observabilidade|ADR-006]] |
| RNF-23 | Empacotamento/distribuição do app desktop — formato **MSIX** (compatível com sideload e possível publicação na Microsoft Store) | Installer MSIX + identidade de pacote; atualização via Store (opcional) |

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
| RNF-13 | ≥ 60% cobertura domínio ⚠️ | ✅ (estimativa) |
| RNF-17 | ≤ 100 ms (P95, ≤50 linhas) | ✅ |
| RNF-19 | 3 ambientes (dev/staging/prod) | ✅ |
| RNF-20 | Deploy p/ staging automático; prod via aprovação | ✅ |
| RNF-21 | Backup diário; restore testado | ✅ |
| RNF-22 | Health check + alerta em falha | ✅ |
| RNF-23 | Pacote MSIX gera install + sideload | ✅ |

### Contadores

| Métrica | Valor |
|:--|:-:|
| **Total RNFs** | **23** |

---
Links: [[01 Requisitos/Requisitos Funcionais|RF]] · [[01 Requisitos/Regras de Negócio|RN]] · [[02 Modelagem/Arquitetura do Sistema|Arquitetura]]
