---
tags: [requisitos, stakeholders]
tipo: documento
status: rascunho
atualizado: 2026-08-28
publish: true
---

# Stakeholders e Personas

## Nichos de público

> [!info] Anéis de público-alvo
> | Anel | Nichos |
> |---|---|
> | **Primário** (foco do produto) | Jogadores, jogadores de RPG de mesa, leitores, programadores |
> | **Secundário** (possível) | Musicistas e apreciadores de música, cinéfilos e hobbies similares |
> | **Distante** (ainda plausível) | Outros hobbies (ex.: ciclismo e afins) |
>
> Nichos primários conduzem as funcionalidades profundas (syntax highlighting p/ programadores, tags de sistema/tema p/ RPG, gênero/autor p/ leitores). Secundários e distantes são atendidos por recursos genéricos (tags, markdown, feed), sem features dedicadas no MVP.

## Stakeholders

| Stakeholder | Papel | Interesse | Influência |
|---|---|---|---|
| Patrocinador/Dono do produto | Financia e prioriza | Sucesso do produto | Alta |
| Equipe de desenvolvimento | Constrói o app | Requisitos claros e estáveis | Alta |
| Usuários finais | Usam a rede | Simplicidade, desempenho | Média |
| Operação/Suporte (futuro) | Mantém servidor | Observabilidade, facilidade de deploy | Média |

## Personas

### P1 — Ana, 28, desenvolvedora
- Dia inteiro no PC; odeia trocar de contexto para o celular
- **Dores:** apps sociais web pesados; notificações dispersas; redes genéricas não renderizam código
- **Necessidade:** rede social rápida, navegável por teclado, com markdown e syntax highlighting
- **Citação:** *"Se tem atalho de teclado e destaque de sintaxe, eu fico."*

### P2 — Bruno, 35, mestre de RPG
- Organiza campanhas e compartilha builds de personagens
- **Dores:** algoritmos escondem suas publicações; Discord é bom para chat, mas ruim para feed público
- **Necessidade:** feed **cronológico** previsível; tags de sistema/tema para descobrir outros jogadores
- **Citação:** *"Quero que meus seguidores vejam tudo que eu posto, e encontrar gente que joga a mesma coisa."*

### P3 — Carla, 22, leitora e resenhista
- Monta listas de leitura e recomendações
- **Dores:** Goodreads é limitado e sem feed social; falta tag por gênero/autor
- **Necessidade:** tags de gênero/tema, busca por tag, perfil com autores favoritos
- **Citação:** *"Quero uma rede onde eu descubra gente com o mesmo gosto."*

### P4 — Daniel, 30, moderador de comunidade ⚠️ (persona fase 2)
- Gerencia grupo pequeno e privado
- **Dores:** falta de ferramentas de moderação simples
- **Necessidade:** remover conteúdo/membros facilmente

## Matriz persona × funcionalidade (MVP)

| Funcionalidade (RF) | Ana | Bruno | Carla |
|---|:-:|:-:|:-:|
| RF-001 Cadastro/Login | ✔ | ✔ | ✔ |
| RF-004 Publicar post | ✔✔ | ✔✔ | ✔ |
| RF-005 Feed cronológico | ✔✔ | ✔✔ | ✔ |
| RF-008/009 Curtir/comentar | ✔ | ✔ | ✔ |
| RF-010 Busca | ✔✔ | ✔ | ✔ |
| RF-024 OAuth GitHub | ✔✔ | | |

> [!note] Features de nicho (F2/F3, fora do MVP)
> RF-027 (biblioteca de livros), RF-028 (repos GitHub), RF-029 (jogos jogados) e RF-030 (mesas RPG) são features profundas de nicho de **Fase 2/3** — ainda **não modeladas** no ER/Domínio. Serão detalhadas quando cada fase for planejada (ver [[VAULT/Checklist - Correções do Plano#F.2|F.2]]).

## Links
- [[01 Requisitos/Requisitos Funcionais|RF]] · [[01 Requisitos/Casos de Uso|Casos de Uso]]
