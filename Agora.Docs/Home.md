---
tags: [moc, home]
tipo: moc
status: ativo
atualizado: 2026-08-25
---

# 🏠 Home — Agora

> **Instituição:** UNA - Contagem
> **Disciplina:** Garantia da Qualidade de Software  
> **Integrantes:** Athur Marquez Diniz, Bernardo Luiz Monteverde Gonçalves, Luiz Filipe Pimenta Correa, Patrick Oliveira Rabelo de Brito
> **Professor(a):** Daniel Henrique Matos de Paiva  
> **Data:** 24 de agosto de 2026

> [!info] Sobre este vault
> Central de documentação do projeto **Agora (C#)**: rede social de hobbies nerd — levantamento de requisitos, modelagem, decisões arquiteturais e gestão.

## Navegação

### 1. Requisitos
- [[01 Requisitos/Visão do Produto|Visão do Produto]]
- [[01 Requisitos/Stakeholders e Personas|Stakeholders e Personas]]
- [[Resumo dos Requisitos]] — resumo executivo
- [[01 Requisitos/Requisitos Funcionais|Requisitos Funcionais (RF)]]
- [[01 Requisitos/Requisitos Não Funcionais|Requisitos Não Funcionais (RNF)]]
- [[01 Requisitos/Regras de Negócio|Regras de Negócio (RN)]]
- [[01 Requisitos/Casos de Uso|Casos de Uso]]

### 2. Modelagem
- [[02 Modelagem/Modelo de Domínio|Modelo de Domínio (UML)]]
- [[02 Modelagem/Modelo de Dados (ER)|Modelo de Dados (ER)]]
- [[02 Modelagem/Arquitetura do Sistema|Arquitetura do Sistema]]
- [[02 Modelagem/Máquinas de Estado|Máquinas de Estado]]
- [[02 Modelagem/Fluxos Principais|Fluxos Principais (Sequência)]]
- [[02 Modelagem/API e Servidor|API e Servidor]]
- [[02 Modelagem/Wireframes|Wireframes (Telas)]]

### 3. Decisões (ADRs)
- [[03 Decisões/ADR Template|Template de ADR]]
- [[03 Decisões/ADR-001 Stack Tecnológica|ADR-001 — Stack Tecnológica (UI e runtime)]] — `aceita`
- [[03 Decisões/ADR-002 Implantação|ADR-002 — Modelo de Implantação]] — `aceita`
- [[03 Decisões/ADR-003 Persistência|ADR-003 — Persistência (ORM)]] — `aceita`
- [[03 Decisões/ADR-004 Ambientes|ADR-004 — Ambientes (dev/staging/prod)]] — `aceita`
- [[03 Decisões/ADR-005 API do Servidor|ADR-005 — Design da API do Servidor]] — `aceita`
- [[03 Decisões/Propostas Pendentes|Propostas Pendentes]] — decisões aguardando escolha

### 4. Gestão
- [[04 Gestão/Definição do MVP|MVP (Definição)]]
- [[04 Gestão/Roadmap|Roadmap]]
- [[04 Gestão/Backlog do Produto|Backlog do Produto]]
- [[04 Gestão/Glossário|Glossário]]
- [[04 Gestão/Operações e Deploy|Operações e Deploy]]

### 5. VAULT (utilitários)
- [[AGENTS.md|AGENTS.md]] — **instruções para agentes de IA** (fonte única de verdade, na raiz)
- [[VAULT/Checklists|Checklists de revisão]]
- [[VAULT/Template - Nova Nota|Template — Nova Nota]]
- [[VAULT/Cheatsheet Mermaid|Cheatsheet Mermaid]]

### Extras
- [[Canvas/Mapa do Projeto.canvas|Mapa do Projeto (Canvas)]]

## Status atual
> [!todo] Próximos passos
> 1. ✅ Stack UI decidida: [[03 Decisões/ADR-001 Stack Tecnológica|ADR-001]] — Avalonia + .NET 10
> 2. ✅ Modelo de implantação decidido: [[03 Decisões/ADR-002 Implantação|ADR-002]] — cliente-servidor
> 3. ✅ Persistência decidida: [[03 Decisões/ADR-003 Persistência|ADR-003]] — EF Core 10
> 4. ✅ Ambientes decididos: [[03 Decisões/ADR-004 Ambientes|ADR-004]] — dev/staging/prod
> 5. ✅ Design da API decidido: [[03 Decisões/ADR-005 API do Servidor|ADR-005]] — REST + JWT
> 6. Iniciar Fase 1 (código)

## Convenções do vault
- **Regras completas em [[AGENTS.md]]** — leia antes de criar/editar notas
- Prefixos numéricos definem a ordem das pastas (`01`, `02`...)
- IDs: `RF-xxx`, `RNF-xxx`, `RN-xx`, `UC-xx`, `ADR-xxx`
- Toda nota usa frontmatter YAML (`tags`, `tipo`, `status`, `atualizado`)
- Diagramas em **Mermaid** (renderização nativa do Obsidian)
