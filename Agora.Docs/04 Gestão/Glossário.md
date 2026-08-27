---
tags: [gestao, glossario]
tipo: documento
status: ativo
atualizado: 2026-08-25
---

# Glossário

> [!info] Linguagem ubíqua (DDD)
> Termos usados consistentemente no vault, código e conversas.

| Termo | Definição |
|---|---|
| **Agora** | Nome do produto; do grego: praça pública principal; rede social de hobbies nerd |
| **Post** | Publicação de texto criada por um usuário; estados em [[02 Modelagem/Máquinas de Estado]] |
| **Tag** | Classificador de conteúdo por tema/hobby (ex: `csharp`, `dnd`, `fantasia`); slug auto-gerado |
| **PostTag** | Relação N:N entre Post e Tag; máx. 5 tags por post (RN-09) |
| **Categoria de tag** | Campo obrigatório da tag: `linguagem`, `tema`, `genero`, `sistema` (RF-023) |
| **Syntax highlighting** | Renderização colorida de código por linguagem, detectada via fence markdown (RF-022) |
| **Feed** | Lista cronológica de posts (seguidos + próprios), RN-07 |
| **Feed por interesse** | Seção alternativa do feed ordenada por tags que o usuário segue |
| **Seguida** | Relação direcionada seguidor → seguido; entidade [[02 Modelagem/Modelo de Domínio#Seguida\|Seguida]] |
| **Curtida** | Marcação positiva única por usuário/post, com toggle (RN-02) |
| **Rascunho** | Post não publicado; autosave local (RNF-15) |
| **ADR** | Architecture Decision Record — ver [[03 Decisões/ADR Template]] |
| **OAuth** | Protocolo de autorização para login com provedores externos (GitHub, Google) — RF-024, RF-025 |
| **Provider** | Provedor de autenticação: `local` (e-mail+senha), `github` ou `google` |
| **Biblioteca** | Lista de livros do usuário no perfil (lidos, want-to-read, notas) — RF-027 |
| **Resenha** | Opinião escrita sobre um livro, com nota pessoal — RF-027 |
| **Repositório** | Projeto de código fonte vinculado ao GitHub do usuário — RF-028 |
| **Campanha** | Narrativa contínua de RPG com mesas, sessões e fichas — RF-030 |
| **Mesa** | Grupo de jogadores e narrador em uma campanha de RPG — RF-030 |
| **Sessão** | Encontro individual dentro de uma campanha (pode ser one-shot) — RF-030 |
| **Ficha** | Personagem de RPG com atributos, habilidades e inventário — RF-030 |
| **RF / RNF / RN** | Requisito Funcional / Não Funcional / Regra de Negócio |
| **MoSCoW** | Must/Should/Could/Won't — priorização |
| **Local-first** | Abordagem onde dados vivem primeiro no dispositivo, sync depois |
| **Cliente-servidor** | App consome API central; fonte da verdade no servidor |
| **MVVM** | Padrão UI (Model-View-ViewModel) típico em WPF/Avalonia |
| **EF Core** | ORM do .NET para persistência |
| **Ambiente** | Instância isolada (dev/staging/prod) com config e dados próprios ([[03 Decisões/ADR-004 Ambientes\|ADR-004]]) |
| **CI/CD** | Integração e entrega contínuas: build/testes automáticos + deploy automático |
| **Staging** | Ambiente de validação pré-produção, espelhando produção |
| **Deploy** | Publicação de uma versão numa instância (dev/staging/prod) |
| **Backup** | Cópia periódica do banco para recuperação (RNF-21) |
| **Observabilidade** | Capacidade de monitorar logs, métricas, health checks e alertas (RNF-22) |
| **Installer** | Pacote de instalação do app desktop (RNF-23) |
| **MSIX** | Formato de empacotamento Windows moderno; suporta sideload e publicação na Microsoft Store (RNF-23) |
| **Microsoft Store** | Loja de apps Windows; canal de distribuição opcional/futuro para o Agora |
| **Sideload** | Instalação direta do pacote (ex: MSIX) sem passar por loja — usado para entregar aos betas |
| **API** | Interface de programação do servidor: conjunto de endpoints REST ([[02 Modelagem/API e Servidor\|API e Servidor]]) |
| **REST** | Estilo arquitetural de API baseado em HTTP e recursos (ADR-005) |
| **Endpoint** | Rota específica da API (ex: `POST /posts`) |
| **JWT** | Token JSON assinado para autenticação stateless (access/refresh token) |
| **LGPD** | Lei Geral de Proteção de Dados (BR) — base de RNF-09/RN-04 |
| **Soft delete** | Exclusão lógica (`ativo=false` ou `status=excluido`) sem apagar linha |
