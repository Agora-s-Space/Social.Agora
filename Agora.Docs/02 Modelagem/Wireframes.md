---
tags: [modelagem, ui, wireframes]
tipo: modelo
status: rascunho
atualizado: 2026-08-25
---

# Wireframes (Telas)

> [!info] Propósito
> Documentação visual dos layout de cada tela do Agora. Estados: loading, vazio, erro.

## Tela: Splash / Loading

> Corresponde a [[01 Requisitos/Requisitos Funcionais#RF-031|RF-031]] e [[01 Requisitos/Requisitos Não Funcionais#RNF-18|RNF-18]]

```
┌─────────────────────────────────────────┐
│                                         │
│                                         │
│          ╔═══════════════════╗          │
│          ║                   ║          │
│          ║    LOGO AGORA     ║          │
│          ║  [A] metálico     ║          │
│          ║  + azul cristal   ║          │
│          ╚═══════════════════╝          │
│                                         │
│              ● ● ●  loading             │
│                                         │
└─────────────────────────────────────────┘
```

**Comportamento:**
- Logo com letter metálica + partes azuis
- Animação: flash no metal + "energia cristalina" nas partes azuis
- Duração: 1–3s (conforme RNF-02)
- Transição automática → Tela de Login ou Feed

## Inventario de telas

| Tela | Módulo | States a documentar |
|---|---|---|
| Splash / Loading | UI | loading |
| Login | Conta | idle, erro, loading |
| Cadastro | Conta | idle, validação, erro |
| Feed principal | Conteúdo | loading, vazio, com dados, erro |
| Feed por tags | Conteúdo | loading, vazio, com dados |
| Publicar post | Conteúdo | rascunho, publicando, erro |
| Perfil (próprio) | Conta | visualização, editando |
| Perfil (outro) | Social | visualizando, seguido, não seguido |
| Busca | Descoberta | idle, resultados, sem resultados |
| Seguidores / Seguindo | Social | lista vazia, com dados |

> [!todo] Próximos passos
> - [ ] Criar wireframe de cada tela listada
> - [ ] Definir paleta de cores e tipografia
> - [ ] Criar mockup de alta fidelidade (Figma/outro)

---

Links: [[02 Modelagem/Modelo de Domínio]] · [[01 Requisitos/Requisitos Funcionais]] · [[04 Gestão/Definição do MVP|MVP]]
