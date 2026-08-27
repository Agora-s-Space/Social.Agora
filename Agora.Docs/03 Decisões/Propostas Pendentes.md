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
