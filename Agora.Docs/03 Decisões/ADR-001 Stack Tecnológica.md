---
tags: [decisoes, adr, stack]
tipo: adr
numero: ADR-001
data: 2026-08-25
status: aceita
atualizado: 2026-08-25
publish: true
---

# ADR-001 — Stack Tecnológica (UI e runtime)

## Contexto
O produto é **Agora**, um app desktop de rede social de hobbies nerd em **C#** ([[01 Requisitos/Visão do Produto]]). É preciso escolher o framework de UI e fixar o runtime antes da Fase 1. Restrições relevantes: RNF-10 (Windows primeiro, sem bloquear Linux/macOS) e equipe pequena.

> [!note] Escopo desta ADR
> Framework de UI + runtime. Persistência → [[03 Decisões/ADR-003 Persistência|ADR-003]] (aceita); implantação → [[03 Decisões/ADR-002 Implantação|ADR-002]].

## Opções consideradas

| Opção        | Prós                                                    | Contras                                                                                          |
| ------------ | ------------------------------------------------------- | ------------------------------------------------------------------------------------------------ |
| **WPF**      | Maduro; MVVM consolidado; enorme material de referência | Só Windows                                                                                       |
| **WinUI 3**  | UI moderna Windows; Fluent nativo                       | Ecossistema jovem; bugs conhecidos; só Windows                                                   |
| **Avalonia** | Cross-plat real; XAML parecido com WPF; ativo           | Comunidade menor que WPF; alguns componentes/controles avançados possuem alternativas comerciais |

## Decisão
**Avalonia UI + .NET 10 (LTS)**, com padrão MVVM (CommunityToolkit.Mvvm 8.4.x).

## Justificativa
1. RNF-10 exige não fechar portas p/ Linux/macOS → descarta WPF/WinUI a médio prazo
2. .NET 10 LTS cobre RNF-11 com suporte até nov/2028; .NET 8 LTS atinge EOL em nov/2026
3. Curva de aprendizado aceitável vindo do ecossistema XAML/C#
4. Avalonia 12 e CommunityToolkit.Mvvm 8.4.x já suportam .NET 10
5. C# 14 traz extension members e field-backed properties — reduz boilerplate no padrão MVVM

## Consequências
- ➕ Portabilidade futura sem rewrite; XAML familiar
- ➕ Suporte LTS até nov/2028; sem dívida técnica de EOL imediato
- ➖ Alguns pacotes NuGet específicos de WPF não funcionam direto
- 👁 Monitorar: desempenho de listas grandes no feed (RNF-01) com virtualização (`ItemsControl` virtualizada)

## Pendências relacionadas
- [[02 Modelagem/Arquitetura do Sistema]]: modelo de implantação → **[[03 Decisões/ADR-002 Implantação|ADR-002]]** (decidido: cliente-servidor)
- Persistência (ORM) → **[[03 Decisões/ADR-003 Persistência|ADR-003]]** (decidido: EF Core 10)
