---
tags: [decisoes, adr, api, servidor]
tipo: adr
numero: ADR-005
data: 2026-08-27
status: aceita
atualizado: 2026-08-27
---

# ADR-005 — Design da API do Servidor

## Contexto
ADR-002 decide **cliente-servidor** (app consome API REST central). É preciso definir o estilo/design dessa API para que os fluxos (feed, posts, interações, auth, busca) sejam implementados de forma consistente. Sem isso, endpoints e autenticação ficam indefinidos.

## Opções consideradas

| Opção | Prós | Contras |
|---|---|---|
| **A — REST (ASP.NET Core Web API) + JWT Bearer** | Padrão consagrado; ASP.NET Core já escolhido (ADR-002); auth stateless com JWT simples; ferramentas maduras | JWT não revogável imediatamente; REST tem verbos semânticos |
| **B — GraphQL** | Consultas flexíveis; menos over-fetch | Complexidade; tooling menor no ecossistema; overkill p/ ~20 betas |
| **C — RPC/JSON (gRPC)** | Tipado, eficiente | Menos adequado p/ app desktop multi-plataforma + navegador OAuth; setup complexo |

## Decisão
**A — REST (ASP.NET Core Web API) + autenticação JWT Bearer.**

## Justificativa
1. **ASP.NET Core já é o servidor** (ADR-002/003) — Web API é o caminho nativo
2. **JWT stateless** escala e é simples para uma equipe pequena; app desktop guarda token e envia em cada request
3. **20 betas** (OKR O2) não justificam complexidade de GraphQL/gRPC (opções B/C)
4. REST + OpenAPI/Swagger dá documentação automática dos endpoints
5. Fluxos existentes ([[02 Modelagem/Fluxos Principais|Fluxos]]) já descrevem endpoints REST de forma consistente

## Consequências
- ➕ API auto-documentada (Swagger/OpenAPI)
- ➕ Ecossistema ASP.NET Core (auth via `Microsoft.AspNetCore.Authentication.JwtBearer`)
- ➕ Mesma stack .NET no client e server
- ➖ JWT não revogável em tempo real → usar token de curta duração + refresh token
- ➖ REST pode exigir mais endpoints de ida e volta
- 👁 Monitorar: RNF-03 (≤ 200 ms P95 resposta); revogação de token/logout

## Referências
- [[03 Decisões/ADR-002 Implantação|ADR-002]] — modelo cliente-servidor
- [[02 Modelagem/API e Servidor|API e Servidor]] — contrato detalhado
- [[02 Modelagem/Fluxos Principais|Fluxos Principais]] — sequências de interação
