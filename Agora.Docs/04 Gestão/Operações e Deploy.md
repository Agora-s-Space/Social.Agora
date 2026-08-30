---
tags: [operacoes, deploy, infra]
tipo: documento
status: rascunho
atualizado: 2026-08-30
---

# Operações e Deploy

> [!info] Visão geral
> Como o Agora é entregue, operado e mantido. Consolida as decisões de deploy, ambientes, testes e suporte espalhadas no vault.

## 1. Ambientes

Decidido em [[03 Decisões/ADR-004 Ambientes|ADR-004]]:

| Ambiente     | Finalidade                        | Onde roda     | Banco            |
| ------------ | --------------------------------- | ------------- | ---------------- |
| **dev**      | Desenvolvimento local de cada dev | Máquina local | Npgsql/PostgreSQL |
| **staging**  | Validação pré-release             | VPS           | Npgsql/PostgreSQL |
| **produção** | Uso dos usuários (20 betas)       | VPS           | Npgsql/PostgreSQL |

> [!note] Banco
> Provedor do servidor: **PostgreSQL via Npgsql** ([[03 Decisões/ADR-007 Banco do Servidor (Npgsql)|ADR-007]]). SQL Server fora do escopo. O cliente usa SQLite para configs, rascunho (RNF-15) e cache ([[03 Decisões/ADR-003 Persistência|ADR-003]]).

> [!info] Sistema operacional e execução ([[03 Decisões/ADR-010 Sistema Operacional da VPS (Linux)|ADR-010]])
> VPS com **Linux (Ubuntu LTS 24.04) + Docker Compose**. App (Kestrel) e **PostgreSQL** rodam como containers; staging e produção usam o mesmo padrão no provedor do Brasil ([[03 Decisões/ADR-009 Residência dos Dados (BR)|ADR-009]]). Operações comuns (restart, logs, backup) via `docker compose`, tudo declarado em código e acionado pelo pipeline (B-37).

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

### 3.1 Retenção e residência

- **Retenção dos backups:** ≤ **30 dias** (política de retenção — [[01 Requisitos/Regras de Negócio#RN-11|RN-11]], [[04 Gestão/Backlog do Produto#B-52|B-52]])
- **Residência:** backups armazenados em **datacenter no Brasil**, junto ao próprio servidor ([[03 Decisões/ADR-009 Residência dos Dados (BR)|ADR-009]]); offsite ainda "a definir" — manter preferência por co-location BR

## 4. Monitoramento e observabilidade

| Área                           | Ferramenta proposta                                                                                                  | Status                               |
| ------------------------------ | -------------------------------------------------------------------------------------------------------------------- | ------------------------------------ |
| Logs estruturados              | Serilog (RNF-16)                                                                                                     | documentado                          |
| Métricas de API                | `System.Diagnostics.Metrics` + OpenTelemetry + Prometheus/Grafana (Fase 2) — [[03 Decisões/ADR-006 Observabilidade\|ADR-006]]; rodam como **containers do compose** no VPS ([[03 Decisões/ADR-010 Sistema Operacional da VPS (Linux)\|ADR-010]], B-46) | [[ADR-006 Observabilidade\|ADR-006]] |
| Uptime/alertas                 | Health checks + alerting                                                                                             | proposto                             |
| Crash reporting do app desktop | Sentry (opcional)                                                                                                    | proposto                             |

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

### 6.1 Notificação de violação de dados (LGPD art. 48)

> [!todo] A definir — Runbook ([[04 Gestão/Backlog do Produto#B-53|B-53]])
> - Escalar quando suspeitar de violação com risco a titulares
> - Notificar **ANPD** e **titulares afetados** (alvo ≤ 72 h da ciência — [[01 Requisitos/Regras de Negócio#RN-12|RN-12]])
> - Registrar incidente internamente (o que, quando, impacto, correção)

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
| Code review | documentado ([[04 Gestão/Operações e Deploy#9. Estratégia de ramificação e revisão\|seção 9]]) |

## 9. Estratégia de ramificação e revisão

> [!info] Modelo
> **GitHub Flow simplificado** — resposta ao tópico "Estratégia de Repositório e CI/CD" do enunciado ([[PRIMEIRA ENTREGA - Requisitos]]): `main` + branches curtas de feature/docs; sem `develop`; CD a partir da branch principal ([[03 Decisões/ADR-004 Ambientes|ADR-004]]).

| Item | Decisão |
|---|---|
| Controle de versão | Git — repositório GitHub: `https://github.com/Agora-s-Space/Social.Agora.git` |
| Branch principal | `main` (protegida — sem push direto) |
| Branches de trabalho | Curtas e descritivas: `feat/...`, `docs/...`, `chore/...`, `fix/...` |
| Convenção de commit | **Conventional Commits** — `feat:`, `fix:`, `docs:`, `chore:`, `refactor:`, `test:` ⚠️ proposta a validar na Fase 1 |
| Code review | PR com **≥ 1 aprovação** obrigatória (CODEOWNERS cobre `Agora.Docs/`); abrir PR para `main` |
| Critérios mínimos de merge | PR aberto p/ `main` com ≥ 1 aprovação · **CI verde** (build + testes — RNF-14) · sem conflitos · commit padronizado (Conventional Commits) |

Referência fluxo de promoção: [[04 Gestão/Operações e Deploy#2. Pipeline (CI/CD)|Pipeline (CI/CD)]]

---

Links: [[03 Decisões/ADR-002 Implantação|ADR-002]] · [[03 Decisões/ADR-004 Ambientes|ADR-004]] · [[04 Gestão/Backlog do Produto|Backlog]] · [[01 Requisitos/Requisitos Não Funcionais|RNF]]
