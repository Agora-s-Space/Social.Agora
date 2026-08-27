---
tags: [decisoes, adr, implantacao]
tipo: adr
numero: ADR-002
data: 2026-08-25
status: aceita
atualizado: 2026-08-25
---

# ADR-002 — Modelo de Implantação

## Contexto
**Agora** é uma rede social multiusuário. É preciso definir como os dados fluem entre dispositivos para que o feed, seguir usuários e interações funcionem. Restrições: RNF-15 (rascunho local mesmo offline), equipe pequena, Fase 1 com 20 betas (OKR O2).

## Opções consideradas

| Opção | Prós | Contras |
|---|---|---|
| **A — Local-first** | Dados vivem no dispositivo; sync posterior; offline-first natural | Complexidade de conflitos (CRDT/OT); feed de outros usuários requer sync constante; validar integridade social é difícil |
| **B — Cliente-servidor** | Modelo consagrado para redes sociais; feed alheio sempre consistente; auth centralizada; moderação viável | Requer servidor desde cedo; latência em ações; RNF-15 precisa de cache local |
| **C — Híbrido (cache + sync)** | Offline para rascunho; sync sob demanda | Complexidade alta; dois modos de operação; bugs de consistência |

## Decisão
**B — Cliente-servidor desde o início.** App consome API REST central; fonte da verdade no servidor.

## Justificativa
1. Rede social é inerentemente multiusuário — feed alheio, seguir, curtir, comentário exigem dados centralizados
2. Local-first (opção A) resolve offline para rascunho, mas não resolve "ver posts de outros" sem sync complexo
3. Equipe pequena → minimize complexidade operacional; cliente-servidor é o modelo mais maduro
4. RNF-15 (rascunho não perde offline) é atendido com cache local + autosave, sem precisar de local-first
5. 20 betas (OKR O2) são poucos para justificar infra de sync distribuído
6. Moderação futura (RF-015) exige ponto central de controle

## Consequências
- ➕ Feed sempre consistente; auth centralizada; moderação viável
- ➕ Ecossistema ASP.NET Core maduro para a API
- ➖ Requer servidor desde a Fase 1 (custo infra mínimo: um VPS)
- ➖ Latência em ações sociais (curtir/comentar depende de rede)
- 👁 Monitorar: latência de API em ações (RNF-03 ≤ 200 ms); custo mensal de servidor
- 👁 RNF-15 (rascunho local) implementar com cache local + autosave, não com local-first

## Referências
- [[02 Modelagem/Arquitetura do Sistema]] — seção "Alternativas consideradas"
- [[01 Requisitos/Requisitos Não Funcionais#RNF-15|RNF-15]] — autosave local
- [[01 Requisitos/Visão do Produto]] — premissa de componente servidor
