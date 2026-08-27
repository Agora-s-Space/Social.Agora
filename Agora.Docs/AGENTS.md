---
tags: [vault, agentes, instrucoes]
tipo: instrucoes-agentes
status: ativo
atualizado: 2026-08-27
---

# AGENTS.md — Instruções para Agentes de IA

> [!important] Fonte única de verdade sobre **como trabalhar neste vault**
> Qualquer agente (ou humano) que for criar/editar documentação aqui DEVE seguir este arquivo. Em caso de conflito entre notas, vale o que está aqui.

## 1. Contexto do projeto
- Produto: **Agora** — rede social de hobbies nerd, app desktop em **C# (.NET)** — [[01 Requisitos/Visão do Produto]]
- Stack decidida (ADR-001, status *aceita*): **Avalonia UI + .NET 10 LTS + MVVM (CommunityToolkit.Mvvm) + EF Core 10 (ADR-003, *aceita*)**
- Arquitetura: 4 camadas — UI → Aplicação → Domínio ← Infra (dependência aponta p/ dentro) — detalhes em [[02 Modelagem/Arquitetura do Sistema]]
- Fase atual: **Fase 0 (Foundation)** — levantamento de requisitos e modelagem; sem código ainda
- Documentação em **PT-BR**; código/identificadores futuros em inglês; entidades de domínio usam nomes em PT (ex: `USUARIO`, `POST`, `CURTIDA`)
- Nenhuma ferramenta de build/lint/teste configurada ainda — comandos na seção 6 são placeholders
- Plano de nicho (devs/gamers/leitores) em [[VAULT/Plano - Rede Social de Nicho]] — conceito base do produto, features de nicho integradas ao MVP
- Comece sempre por [[Home]] para se orientar

## 2. Mapa do vault — onde cada coisa vive

| Caminho | Propósito | Quando mexer |
|---|---|---|
| `AGENTS.md` (raiz) | Instruções para agentes de IA — este arquivo | Qualquer edição/move que afete o processo |
| `01 Requisitos/` | Visão, personas, RF/RNF/RN, casos de uso | Novo requisito ou mudança de escopo |
| `02 Modelagem/` | Domínio, ER, arquitetura, estados, fluxos, wireframes, API | Mudança estrutural no sistema |
| `03 Decisões/` | ADRs + template | Toda decisão técnica relevante |
| `04 Gestão/` | Roadmap, backlog, glossário, definição do MVP, operações | Planejamento/priorização |
| `Canvas/` | Mapas visuais | Opcional |
| `VAULT/` | Utilitários: templates, checklists, cheatsheets, plano de nicho | Manutenção do próprio processo |

**Não criar** pastas novas sem registrar aqui primeiro. Não renumerar pastas existentes.

## 3. Regras invioláveis

1. **ADRs são imutáveis**: contexto mudou → novo ADR marcando o anterior como *substituído por [[...]]*
2. **IDs sequenciais, nunca reutilizados**: `RF-0xx`, `RNF-0xx`, `RN-0xx`, `UC-0xx`, `B-0xx`, `ADR-0xx`
3. **Toda nota nova** nasce com frontmatter YAML: `tags`, `tipo`, `status`, `atualizado` (data ISO)
4. **Links internos** `[[]]` obrigatórios ao citar outro documento — nada de referência solta em texto puro
5. **Diagramas**: PlantUML para classes, componentes, estados e use cases; Mermaid para sequence, flowchart, ER, gantt e mindmap — fences ```` ```plantuml ```` ou ```` ```mermaid ```` sempre fechadas; testar mentalmente sintaxe antes de salvar
6. **Hipóteses não confirmadas** recebem ⚠️ e ficam explícitas — jamais apresentar estimativa como fato
7. **Rastreabilidade**: RF novo → ligar a UC e backlog; RN nova → ligar aos RFs que a usam
8. **Ao alterar estrutura ou escopo**, atualizar também: [[Home]], [[Resumo dos Requisitos]] e [[04 Gestão/Backlog do Produto]]
9. Nunca apagar histórico de requisitos — itens cancelados viram `status: cancelado`
10. Português claro e direto; tabelas > parágrafos longos
11. **Entidades de domínio** usam nomes PT (`USUARIO`, `POST`, `CURTIDA`) — não traduzir para inglês nos diagramas ER/modelo

## 4. Fluxos de trabalho padrão

### 4.1 Adicionar/atualizar requisito
1. Criar/atualizar entrada em [[01 Requisitos/Requisitos Funcionais]] (ou RNF/RN)
2. Garantir caso de uso correspondente em [[01 Requisitos/Casos de Uso]]
3. Adicionar item em [[04 Gestão/Backlog do Produto]] com MoSCoW
4. Atualizar contadores no [[Resumo dos Requisitos]]
5. Se for do MVP, refletir em [[04 Gestão/Definição do MVP|Definição do MVP]]

### 4.2 Propor decisão técnica
1. Copiar [[03 Decisões/ADR Template]] → `03 Decisões/ADR-0XX <título>.md`
2. Preencher contexto/opções/consequências; status inicial: `proposta`
3. Indexar no template e, se aceita pelo usuário, mudar status para `aceita`
4. Refletir impacto nas notas de [[02 Modelagem/Arquitetura do Sistema]]

### 4.3 Criar qualquer nota
Usar o esqueleto de [[VAULT/Template - Nova Nota]].

## 5. Definition of Done (documento)
- [ ] Frontmatter completo e data `atualizado` real
- [ ] Links bidirecionais com documentos relacionados
- [ ] Diagramas renderizam sem erro de sintaxe
- [ ] Sem informação duplicada entre notas (referencie, não copie)
- [ ] Hipóteses marcadas com ⚠️

## 6. Comandos do projeto (quando houver código)
```powershell
# placeholders — preencher na Fase 1
dotnet build
dotnet test
```
- Nenhuma ferramenta de lint/formatter configurada — RNF-14 exige CI verde (B-09 no backlog)
- Testes alvo: ≥ 60% cobertura no núcleo de domínio (RNF-13, ⚠️ estimativa)
- ADR-002 (implantação) **aceita**: cliente-servidor — servidor desde Fase 1, cache local para rascunho (RNF-15)
- ADR-003 (persistência) **aceita**: EF Core 10 — SQLite client, Npgsql/SqlServer server
- ADR-004 (ambientes) **aceita**: dev / staging / produção — 3 ambientes isolados (config por ambiente); detalhes em [[04 Gestão/Operações e Deploy|Operações e Deploy]]
- ADR-005 (design da API) **aceita**: REST (ASP.NET Core Web API) + JWT Bearer — contrato em [[02 Modelagem/API e Servidor|API e Servidor]]

## 7. Referências rápidas
- [[VAULT/Checklists|Checklists de revisão]]
- [[VAULT/Cheatsheet Mermaid|Cheatsheet Mermaid]]
- [[VAULT/Cheatsheet PlantUML|Cheatsheet PlantUML]]
- [[04 Gestão/Glossário]] — linguagem ubíqua (use estes termos!)
- [[04 Gestão/Definição do MVP|Definição do MVP]] — escopo consolidado do MVP (RFs/RNFs/RNs/backlog)
