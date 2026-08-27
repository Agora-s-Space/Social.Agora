---
tags: [vault, checklist, qualidade]
tipo: checklist
status: ativo
atualizado: 2026-08-25
---

# Checklists de Revisão

> [!tip] Como usar
> Aplicar antes de finalizar qualquer edição. Marcar cada item com `[x]` ao confirmar.

## 1. Requisito pronto (RF/RNF/RN)

### Formato
- [x] RF: frase no padrão "O sistema deve..." ou "O usuário pode..."
- [ ] RNF: meta mensurável com critério numérico (ex: `≤ 2 s (P95)`, `≥ 60%`)
- [ ] RN: regra clara com sujeito explícito e consequência definida

### Estrutura
- [ ] ID sequencial único, sem reutilização (`RF-0xx`, `RNF-0xx`, `RN-0xx`)
- [ ] Prioridade MoSCoW definida (Must / Should / Could / Won't)
- [ ] Ligado a pelo menos 1 caso de uso (coluna `Caso de uso` na tabela)
- [ ] RNs relacionadas referenciadas (e vice-versa: RN → RF impactado)

### Qualidade
- [ ] Hipóteses marcadas com ⚠️
- [ ] Sem ambiguidade — outro desenvolvedor entenderia sem perguntar
- [ ] Atualizar [[Resumo dos Requisitos]] ao adicionar/modificar

## 2. Caso de uso pronto

### Formato tabela (obrigatório)
- [ ] **Ator** identificado (Visitante ou Usuário autenticado)
- [ ] **RF de origem** com link `[[01 Requisitos/Requisitos Funcionais#RF-xxx|RF-xxx]]`
- [ ] **Pré-condição** explícita
- [ ] **Fluxo principal** numerado (1. · 2. · 3. ...)
- [ ] **Fluxos alternativos** rotulados (2a. · 3a. ...)
- [ ] **Exceções** listadas (sem conexão, validação, etc.)
- [ ] **Pós-condição** com entidade de domínio resultante

### Conteúdo
- [ ] Referências a RNs usadas no fluxo (ex: `RN-03`, `RN-06`)
- [ ] Referências a entidades de domínio via link `[[...]]`
- [ ] Todos os RFs mapeados a pelo menos 1 UC

## 3. Diagrama pronto (Mermaid ou PlantUML)

### Regras gerais
- [ ] Fences abertas **e** fechadas (```` ```mermaid ```` ou ```` ```plantuml ````)
- [ ] Sintaxe válida (renderizou no Obsidian)
- [ ] Rótulos em PT-BR
- [ ] `"` dentro de labels escapada: `["texto com \"aspas\""]`
- [ ] Nota `> [!note]` explicando convenções do diagrama

### Divisão de responsabilidade
| Tipo de diagrama | Ferramenta | Onde aparece |
|---|---|---|
| Classes | **PlantUML** | Modelo de Domínio |
| Componentes | **PlantUML** | Arquitetura do Sistema |
| Estados | **PlantUML** | Máquinas de Estado |
| Use cases | **PlantUML** | Casos de Uso |
| Sequência | **Mermaid** | Fluxos Principais |
| Flowchart | **Mermaid** | RF, RN, Regras |
| ER | **Mermaid** | Modelo de Dados |
| Gantt | **Mermaid** | Roadmap |
| Mindmap | **Mermaid** | Visão do Produto |

## 4. ADR pronto

- [ ] Seguiu [[03 Decisões/ADR Template]]
- [ ] Frontmatter: `tags`, `tipo: adr`, `numero: ADR-0xx`, `data: AAAA-MM-DD`, `status`
- [ ] ≥ 2 opções consideradas com prós/contras em tabela
- [ ] Consequências incluem o que monitorar (emoji 👁)
- [ ] Status: `proposta` até aprovação do usuário → `aceita`
- [ ] Indexado na lista de ADRs do template

## 5. Nota nova (frontmatter obrigatório)

- [ ] `tags`: array com ao menos 1 tag
- [ ] `tipo`: documento | requisito | decisao | checklist | template | resumo | cheatsheet
- [ ] `status`: rascunho | ativo | cancelado
- [ ] `atualizado`: data ISO `AAAA-MM-DD` do dia

## 6. Fim de sessão (higiene do vault)

- [ ] [[Home]] reflete a estrutura atual
- [ ] [[Resumo dos Requisitos]] com números atualizados (RF, RNF, RN, UC, entidades, backlog)
- [ ] Backlog e Roadmap coerentes com decisões tomadas
- [ ] Nenhum link vermelho (nota inexistente) criado por engano
- [ ] `atualizado:` real nas notas tocadas
- [ ] Contadores de IDs consistentes (total e por MoSCoW)
- [ ] M1 milestone apenas com RFs Must
