---
tags: [modelagem, arquitetura]
tipo: documento
status: rascunho
atualizado: 2026-08-25
---

# Arquitetura do Sistema

> [!warning] Decisão aberta (impacto alto)
> Modelo de implantação decidido: **B — Cliente-servidor** ([[03 Decisões/ADR-002 Implantação|ADR-002]]).

## Visão em camadas (aplicação desktop)

```plantuml
@startuml
skinparam componentStyle rectangle
skinparam packageStyle rectangle

package "App Desktop (.NET 10)" as Desktop {
  [Camada de Apresentação\nMVVM · Views + ViewModels] as UI
  [Camada de Aplicação\nCasos de uso · DTOs · Interfaces] as APP
  [Núcleo de Domínio\nEntidades · RNs · Value Objects] as DOM
  [Infraestrutura\nEF Core · HTTP client · Auth · Logs] as INFRA
}

database "Servidor\nREST API + Banco" as SERV

UI -down-> APP
APP -down-> DOM
APP -down-> INFRA
INFRA -down-> DOM
INFRA -right-> SERV : HTTPS/TLS

note bottom of DOM
  Dependência sempre aponta
  para dentro (Domínio)
end note
@enduml
```

## Regras das camadas

| Camada | Responsabilidade | Pode depender de |
|---|---|---|
| Apresentação | Telas, bindings, UX | Aplicação |
| Aplicação | Orquestra casos de uso (UC-01..09) | Domínio, abstrações de Infra |
| Domínio | Entidades + regras ([[01 Requisitos/Regras de Negócio\|RN]]) | **Nada** (POCO puro) |
| Infra | Persistência, rede, crypto | Domínio |

> [!important] Dependência sempre aponta para dentro (Domínio)
> Testabilidade (RNF-13) e troca de SGBD/UI dependem disso.

## Componentes-chave

```plantuml
@startuml
skinparam componentStyle rectangle

[FeedViewModel] --> [ObterFeedService]
[ObterFeedService] --> [IPostRepository]
[IPostRepository] ..> [EF Core Repository] : implementação
[ObterFeedService] --> [Cache local de página]
@enduml
```

## Requisitos que moldaram a arquitetura
- RNF-10 → camadas desacopladas da UI específica
- RNF-12/RNF-13 → separação domínio/aplicação + testes
- RNF-15 → cache/rascunho local mesmo offline
- RNF-07/RNF-06 → TLS + hash de senha na Infra

## Alternativas consideradas
1. **Monólito local (sem servidor)** — inviável para rede social multiusuário de hobby
2. **Cliente fino + tudo no servidor** — perde RNF-15; adotar parcialmente via cache
3. **Camadas como acima** ✅ escolhida (detalhes em [[03 Decisões/ADR-002 Implantação|ADR-002]])

---
Links: [[03 Decisões/ADR Template|ADRs]] · [[02 Modelagem/Fluxos Principais|Fluxos]] · [[01 Requisitos/Requisitos Não Funcionais|RNF]]
