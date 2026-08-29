---
tags: [decisoes, propostas]
tipo: documento
status: ativo
atualizado: 2026-08-27
---

# Propostas Pendentes

Propostas de arquitetura aguardando decisão. Quando uma proposta é aceita, vira ADR nova com `status: aceita`.

> [!note] Regra
> Propostas **não são ADRs** — são rascunhos de decisão. Só viram ADR quando a escolha é feita.

> [!note] ADR-006 (Observabilidade)
> [[03 Decisões/ADR-006 Observabilidade|ADR-006]] está registrada como ADR com `status: adiada` — **fora do MVP** (Prometheus + Grafana vêm na Fase 2, ainda a planejar). Aceitar/reformular quando a Fase 2 iniciar — ver [[VAULT/Checklist - Correções do Plano]].

---

## P-001 — Publicação na Microsoft Store (canal de distribuição)

**Status:** aberta · **Criada em:** 2026-08-27 · **Data alvo de decisão:** Fase 2/3

**Questão:** Deve o Agora ser distribuído também via **Microsoft Store**, além de sideload?

**Contexto:**
- Empacotamento já decidido como **MSIX** ([[01 Requisitos/Requisitos Não Funcionais#RNF-23|RNF-23]]) — compatível com sideload e com a Store
- Beta (Fase 1) usa sideload; Store seria um canal adicional/alternativo
- Detalhes em [[04 Gestão/Operações e Deploy|Operações e Deploy]]

**Impactos a avaliar:**
- Custo: conta Partner Center (~US$ 19) + certificado de assinatura
- Políticas da Store: conteúdo, privacidade/LGPD ([[01 Requisitos/Requisitos Não Funcionais#RNF-09|RNF-09]]), login próprio
- Atualizações: Store gerencia automáticas
- Distribuição/visibilidade de novos usuários

**Próximos passos:** decidir na Fase 2/3; se aceita, vira ADR de distribuição.

---

## P-002 — Armazenamento do refresh token no desktop

**Status:** ✅ **fechada (2026-08-28)** → virou [[03 Decisões/ADR-008 Segurança de Sessão (DPAPI)|ADR-008]]

**Questão:** Onde guardar o **refresh token** (login persistente) no cliente desktop Avalonia, sem expor o segredo em disco?

**Contexto:**
- Auth JWT com access/refresh decidido em [[03 Decisões/ADR-005 API do Servidor|ADR-005]]; skeleton da API é [[04 Gestão/Backlog do Produto#B-42|B-42]]
- Cliente já usa **SQLite** p/ configs, rascunho e cache ([[03 Decisões/ADR-003 Persistência|ADR-003]]) — mas guardar token de sessão junto com cache puro é frágil
- App desktop Windows empacotado em **MSIX** ([[01 Requisitos/Requisitos Não Funcionais#RNF-23|RNF-23]])

**Opções consideradas:**
| Opção | Prós | Contras |
|---|---|---|
| **DPAPI** (`ProtectedData`, escopo `CurrentUser`) + arquivo em `%LOCALAPPDATA%` | API nativa do .NET (`System.Security.Cryptography.ProtectedData`); sem dependência externa; limite generoso (arquivo, ~1 MB); padrão usado por `gh`/`gcloud`; escopo por usuário | Não portável p/ outras máquinas/usuários (por design); chave presa ao perfil do Windows |
| **Windows Credential Manager** (`PasswordVault`) | UX integrada ao SO; sincroniza com conta MS; usuário pode gerenciar | Teto de **2.400 bytes** por credencial (cabe refresh token sozinho, mas não payload JSON completo); exige TFM `-windows10.0.19041` e WinRT; comportamento varia em sessão remota/novos builds (24H2) |
| **Windows Hello / TPM** | Máxima segurança (biometria/PIN) | Fricção de UX p/ cada desbloqueio; sobrequalidade p/ MVP |
| **Só SQLite** (config) | Simples | Segredo em plaintext no cache — risco p/ LGPD (RNF-09) e p/ token de sessão |

**Recomendação (aceita ✅):** **DPAPI (escopo `CurrentUser`)** para o refresh token → blob criptografado em `%LOCALAPPDATA%\Agora\secrets.dat`. Access token fica **só em memória**. Simples, nativo, sem limite de 2,4 KB e suficiente p/ app desktop mono-usuário. Credential Manager fica como evolução futura (se houver múltiplas contas/roaming).

**Desfecho:** escolhido pelo dono do produto em 2026-08-28 → registrado em [[03 Decisões/ADR-008 Segurança de Sessão (DPAPI)|ADR-008]] (status `aceita`).

---
