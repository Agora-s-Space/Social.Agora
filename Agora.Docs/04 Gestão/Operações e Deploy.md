---
tags: [operacoes, deploy, infra]
tipo: documento
status: rascunho
atualizado: 2026-08-27
---

# Operações e Deploy

> [!info] Visão geral
> Como o Agora é entregue, operado e mantido. Consolida as decisões de deploy, ambientes, testes e suporte espalhadas no vault.

## 1. Ambientes

Decidido em [[03 Decisões/ADR-004 Ambientes|ADR-004]]:

| Ambiente     | Finalidade                        | Onde roda     | Banco            |
| ------------ | --------------------------------- | ------------- | ---------------- |
| **dev**      | Desenvolvimento local de cada dev | Máquina local | Npgsql/SqlServer |
| **staging**  | Validação pré-release             | VPS           | Npgsql/SqlServer |
| **produção** | Uso dos usuários (20 betas)       | VPS           | Npgsql/SqlServer |

> [!warning] Regras
> - Nunca usar dados reais em staging
> - Configuração (connection string, secrets, feature flags) por ambiente
> - Fluxo de promoção: dev → staging → produção

## 2. Pipeline (CI/CD)

| Etapa | Onde | Responsável |
|---|---|---|
| Build + testes | CI (a cada push — RNF-14) | CI |
| Deploy automático → staging | CI/CD | CI |
| Deploy → produção | CD (manual/aprovado) | Dev |
| Migrations de banco | Deploy (EF Core — ADR-003) | CD |

Referência: [[04 Gestão/Backlog do Produto#B-09|B-09 CI]] · [[04 Gestão/Backlog do Produto#B-37|B-37 CD]]

## 3. Backup e restore

> [!todo] A definir
> - Frequência de backup do banco de produção
> - Local de armazenamento dos backups (offsite)
> - Procedimento de restore (RTO/RPO)
> - Referência: [[04 Gestão/Backlog do Produto#B-38|B-38]]

## 4. Monitoramento e observabilidade

| Área | Ferramenta proposta | Status |
|---|---|---|
| Logs estruturados | Serilog (RNF-16) | documentado |
| Métricas de API | `System.Diagnostics.Metrics` + OpenTelemetry + Prometheus/Grafana — [[03 Decisões/ADR-006 Observabilidade|ADR-006]] | proposta |
| Uptime/alertas | Health checks + alerting | proposto |
| Crash reporting do app desktop | Sentry (opcional) | proposto |

Referência: [[04 Gestão/Backlog do Produto#B-39|B-39]]

## 5. Empacotamento e distribuição

> [!info] Formato
> **MSIX** (RNF-23) — funciona para sideload (betas) e habilita uma eventual publicação na **Microsoft Store**.

| Item | Status |
|---|---|
| Installer do app desktop (Windows) | **MSIX** |
| Assinatura de código | certificado p/ assinatura do pacote |
| Atualização | via Store (se publicada) ou sideload |
| Entrega aos 20 betas | sideload (via link/arquivo MSIX) |
| Publicação na Microsoft Store | **opcional / futuro** |

> [!note] Microsoft Store — considerações para avaliar
> - Requer pacote **MSIX** com identidade de pacote (package identity)
> - Conta de desenvolvedor **Partner Center** (taxa única ~US$ 19)
> - **Assinatura de código** com certificado (a Store assina; sideload exige self/enterprise)
> - Cumprimento das **políticas** da Store: conteúdo, privacidade/LGPD (RNF-09), requisito de login próprio, etc.
> - A Store gerencia **atualizações** automáticas dos usuários
> - Sem necessidade de .NET runtime na máquina se empacotado **self-contained** (ou usar framework-dependent + instalar runtime)
> - Requer declaração de capacidades (permissões) via manifest do pacote
>
> *Decisão de publicar na Store não tomada ainda — segue como opção de distribuição (ver [[04 Gestão/Backlog do Produto#B-40\|B-40]]).*

Referência: [[04 Gestão/Backlog do Produto#B-40|B-40]]

## 6. Segurança operacional

| Item | Status |
|---|---|
| Hash de senha (RNF-06) | documentado |
| TLS (RNF-07) | documentado |
| Proteção força bruta (RNF-08) | documentado |
| Secrets management (connection strings, keys) | a definir |
| Patching / renovação de certificados | a definir |

## 7. Documentação de suporte

> [!todo] A definir
> - Runbook de troubleshooting
> - Procedimento de rollback
> - Canais de suporte aos betas
> - Referência: [[04 Gestão/Backlog do Produto#B-41|B-41]]

## 8. Padrões de código

| Item | Status |
|---|---|
| Testes de domínio ≥ 60% (RNF-13) | documentado |
| CI build+testes (RNF-14) | documentado |
| Analyzers .NET (linters) | proposto |
| Formatação de código | proposto |
| Code review | a definir |

---

Links: [[03 Decisões/ADR-002 Implantação|ADR-002]] · [[03 Decisões/ADR-004 Ambientes|ADR-004]] · [[04 Gestão/Backlog do Produto|Backlog]] · [[01 Requisitos/Requisitos Não Funcionais|RNF]]
