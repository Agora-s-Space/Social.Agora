---
tags: [modelagem, sequencia, fluxos]
tipo: documento
status: rascunho
atualizado: 2026-08-25
---

# Fluxos Principais (Diagramas de Sequência)

> [!note] Convenção
> Todos os diagramas usam Mermaid `sequenceDiagram`. Participantes: VM = ViewModel, APP = Service/Application, DB = Repositório local, SRV = Servidor/API.

## 1. Autenticar (UC-02)

```mermaid
sequenceDiagram
    actor U as Usuário
    participant VM as LoginViewModel
    participant APP as AutenticacaoService
    participant SRV as Servidor/Auth API

    U->>VM: informa e-mail + senha
    VM->>APP: Autenticar(email, senha)
    APP->>SRV: POST /auth/login (TLS)
    alt credenciais válidas
        SRV-->>APP: token + dados do perfil
        APP-->>VM: sessão iniciada
        VM-->>U: navega p/ Feed
    else inválidas/bloqueada
        SRV-->>APP: 401 / 423
        APP-->>VM: erro amigável
        VM-->>U: mensagem + nova tentativa
    end
```

## 2. Publicar post (UC-04)

```mermaid
sequenceDiagram
    actor U as Autor
    participant VM as CompositorViewModel
    participant APP as PublicarPostService
    participant DB as Repositório local (cache)
    participant SRV as Servidor/API

    U->>VM: digita texto (≤5.000 - RN-06)
    VM->>DB: autosave rascunho (RNF-15)
    U->>VM: clica "Publicar"
    VM->>APP: Publicar(postId, conteudo)
    APP->>SRV: POST /posts
    SRV-->>APP: 201 Created (post persistido)
    APP-->>VM: sucesso
    VM-->>U: post aparece no topo do feed
```

> [!note] Offline
> Se o passo com `SRV` falhar, o post permanece `Rascunho` local e reenvia quando a conexão voltar.

## 3. Seguir usuário e efeito no feed (UC-06)

```mermaid
sequenceDiagram
    actor U as Usuário
    participant UI as PerfilView
    participant SRV as Servidor/API

    U->>UI: clicar "Seguir"
    UI->>SRV: POST /usuarios/{id}/seguidores
    SRV-->>UI: 200 OK
    UI-->>U: botão vira "Seguindo"
    Note over SRV: Próximos GET /feed<br/>incluem posts do seguido (RN-07)
```

## 4. Interação gera notificação (RF-011)

```mermaid
sequenceDiagram
    participant A as Autor da interação
    participant SRV as Servidor/API
    participant NQ as Fila de notificações
    participant D as Destinatário

    A->>SRV: curte/comenta post X
    SRV->>NQ: enfileira evento (tipo, origem)
    NQ-->>D: notificação in-app no próximo poll/push
    D->>D: badge de não lidas incrementa
```

## 5. Publicar post com tags (UC-04 + RF-016/017)

```mermaid
sequenceDiagram
    actor U as Autor
    participant VM as CompositorViewModel
    participant APP as PublicarPostService
    participant DB as Repositório local (cache)
    participant SRV as Servidor/API

    U->>VM: digita texto (≤5.000 - RN-06)
    U->>VM: seleciona 1–5 tags (RF-016)
    VM->>DB: autosave rascunho (RNF-15)
    U->>VM: clica "Publicar"
    VM->>APP: Publicar(postId, conteudo, tags)
    APP->>SRV: POST /posts + POST /posts/{id}/tags
    SRV-->>APP: 201 Created
    APP-->>VM: sucesso
    VM-->>U: post aparece no feed; tags indexadas para busca
```

## 6. Autenticar via OAuth — GitHub (UC-09)

```mermaid
sequenceDiagram
    actor U as Usuário
    participant APP as App Desktop
    participant NAV as Navegador
    participant GH as GitHub API
    participant SRV as Servidor/Auth API

    U->>APP: clica "Entrar com GitHub"
    APP->>NAV: abreAuthorize URL
    NAV->>GH: usuário autoriza app
    GH-->>NAV: redirect com code
    NAV-->>APP: code capturado (callback)
    APP->>SRV: POST /auth/github {code}
    SRV->>GH: troca code por access token
    GH-->>SRV: access_token + dados do usuário
    alt conta já existe (provider="github")
        SRV-->>APP: JWT + dados do perfil
    else e-mail já existe com conta local
        SRV-->>APP: conflito — pergunta: vincular ou criar nova?
    else novo usuário
        SRV->>SRV: cria USUARIO(provider="github") + PERFIL
        SRV-->>APP: JWT + dados do perfil
    end
    APP-->>U: sessão iniciada → feed
```

---

Links: [[02 Modelagem/Arquitetura do Sistema|Arquitetura]] · [[01 Requisitos/Casos de Uso|UC]]
