---
tags: [decisoes, adr, persistencia]
tipo: adr
numero: ADR-003
data: 2026-08-25
status: aceita
atualizado: 2026-08-25
---

# ADR-003 — Persistência (ORM)

## Contexto
Com ADR-002 (cliente-servidor) decidido, o app desktop precisa de uma camada de persistência para: rascunho local (RNF-15), cache de feed, e comunicação com a API do servidor.

## Opções consideradas

| Opção | Prós | Contras |
|---|---|---|
| **A — EF Core** | ORM completo do .NET; migrations; change tracking; LINQ; integração com ASP.NET Core no servidor; enorme comunidade | Abstração pesada; SQL gerado pode ser ineficiente; learning curve para consultas complexas; overhead para operações simples |
| **B — Dapper** | Micro-ORM leve; SQL manual = controle total; performance superior; baixo overhead | Sem migrations; sem change tracking; mais boilerplate; queries manuais = mais chances de erro |
| **C — ADO.NET puro** | Máximo controle; zero overhead | Muito boilerplate; sem abstração; difícil de manter em projetos grandes |
| **D — EF Core + Dapper híbrido** | EF para CRUD simples + migrations; Dapper para consultas complexas do feed | Dois orçamentos; complexidade de manter dois estilos; time precisa dominar ambos |

## Decisão
**A — EF Core 10** para todas as camadas de persistência.

## Justificativa
1. Migrações nativas — o [[02 Modelagem/Modelo de Dados (ER)|ER]] define 9 tabelas com FKs e indexes; EF Core Migrations resolve com versionamento
2. LINQ elimina queries manuais para CRUD — reduz boilerplate significativo
3. `Microsoft.EntityFrameworkCore.Sqlite` para o client (rascunho local, RNF-15); `Npgsql` ou `SqlServer` para o servidor
4. Integração nativa com ASP.NET Core — o servidor usa o mesmo ORM que o client
5. Comunidade enorme; documentação oficial da Microsoft; fácil encontrar ajuda
6. Padrão repositório ([[02 Modelagem/Arquitetura do Sistema|Arquitetura]]) encaixa naturalmente com `DbSet<T>`

## Consequências
- ➕ Migrações versionadas desde o primeiro commit — schema sempre rastreável
- ➕ LINQ reduz erros de tipagem em queries
- ➕ Mesmo ORM no client e server → moins troca de contexto
- ➖ SQL gerado pode ser ineficiente em consultas complexas (monitorar com logging em dev)
- 👁 Monitorar: performance do feed (RNF-01) — se SQL gerado for lento, otimizar com `FromSqlRaw` ou indexes manuais
- 👁 Para consultas pesadas futuras, considerar Dapper como complemento (opção D)

## Referências
- [[03 Decisões/ADR-001 Stack Tecnológica|ADR-001]] — stack UI e runtime
- [[03 Decisões/ADR-002 Implantação|ADR-002]] — modelo cliente-servidor
- [[02 Modelagem/Arquitetura do Sistema]] — diagrama de componentes
- [[02 Modelagem/Modelo de Dados (ER)|ER]] — schema relacional
