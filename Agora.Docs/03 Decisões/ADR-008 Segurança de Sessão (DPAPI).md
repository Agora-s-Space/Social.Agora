---
tags: [decisoes, adr, seguranca, sessao, client]
tipo: adr
numero: ADR-008
data: 2026-08-28
status: aceita
atualizado: 2026-08-28
---

# ADR-008 — Armazenamento do refresh token no desktop (DPAPI)

## Contexto

O app usa auth JWT com access + refresh token ([[03 Decisões/ADR-005 API do Servidor|ADR-005]]; skeleton em [[04 Gestão/Backlog do Produto#B-42|B-42]]). O cliente é desktop **Avalonia** empacotado em **MSIX** ([[01 Requisitos/Requisitos Não Funcionais#RNF-23|RNF-23]]) e já usa **SQLite** para configs, rascunho e cache de feed ([[03 Decisões/ADR-003 Persistência|ADR-003]]). É preciso onde guardar o **refresh token** (login persistente) sem expor o segredo em texto claro no disco.

> [!important] Decisão da equipe (2026-08-28)
> Registrar como ADR a opção **DPAPI** escolhida na proposta [[03 Decisões/Propostas Pendentes#P-002|P-002]].

## Opções consideradas

| Opção                                                                               | Prós                                                                                                                              | Contras                                                                                                                   |
| ----------------------------------------------------------------------------------- | --------------------------------------------------------------------------------------------------------------------------------- | ----------------------------------------------------------------------------------------------------------------------- |
| **A — DPAPI** (`ProtectedData`, escopo `CurrentUser`) + arquivo em `%LOCALAPPDATA%` | API nativa do .NET; sem dependência externa; limite generoso (arquivo, ~1 MB); padrão usado por `gh`/`gcloud`; escopo por usuário | Não portável p/ outra máquina/usuário (por design)                                                                       |
| **B — Windows Credential Manager** (`PasswordVault`)                                | UX integrada ao SO; sincroniza com conta MS                                                                                       | Teto de **2.400 bytes** por credencial; exige TFM `-windows10.0.19041` e WinRT                                             |
| **C — Windows Hello / TPM**                                                         | Máxima segurança (biometria/PIN)                                                                                                  | Fricção de UX por sessão; sobrequalidade p/ MVP                                                 |
| **D — Só SQLite (config)**                                                          | Simples                                                                                                                           | Segredo em plaintext no cache — risco p/ LGPD ([[01 Requisitos/Requisitos Não Funcionais#RNF-09]]) e token de sessão |

## Decisão

**DPAPI (escopo `CurrentUser`)** para o **refresh token** → blob criptografado em `%LOCALAPPDATA%\Agora\secrets.dat`. O **access token** fica **somente em memória** (nunca persistido).

## Justificativa

1. Nativo do .NET (`System.Security.Cryptography.ProtectedData`) — zero dependência externa
2. Sem limite de ~2,4 KB do Credential Manager; suficiente para payload de refresh token
3. Escopo por usuário do Windows; padrão consolidado em CLIs (`gh`, `gcloud`)
4. Suficiente para app desktop mono-usuário; mantém segredo fora do SQLite (config/cache)

## Consequências

- ➕ Refresh token persistido **criptografado** (blob DPAPI), separado do SQLite de config/cache
- ➕ Access token apenas em memória (revogado por expiração/[[#logout]])
- ➖ Chave presa ao perfil do Windows (sem portabilidade entre máquinas — aceitável p/ MVP)
- 👁 Credential Manager fica como **evolução futura**, caso surja multi-conta/roaming

## Referências

- [[03 Decisões/Propostas Pendentes#P-002|P-002]] — proposta que originou esta ADR (fechada)
- [[03 Decisões/ADR-005 API do Servidor|ADR-005]] — auth JWT (access + refresh)
- [[03 Decisões/ADR-003 Persistência|ADR-003]] — SQLite no client (não armazena segredo de sessão)
- [[02 Modelagem/API e Servidor|API e Servidor]] — fluxo de auth/logout/refresh
- [[01 Requisitos/Requisitos Não Funcionais#RNF-09|RNF-09]] — LGPD / proteção de dados pessoais
