---
tags: [vault, plantuml, cheatsheet]
tipo: cheatsheet
status: ativo
atualizado: 2026-08-25
---

# Cheatsheet PlantUML (Obsidian)

> [!info] Uso no projeto
> PlantUML é plugin do Obsidian. Usado para: **classes**, **componentes**, **estados** e **use cases**. Mermaid continua para sequence, flowchart, ER, gantt e mindmap.

## class — modelo de domínio
````markdown
```plantuml
@startuml
skinparam classAttributeIconSize 0

class Usuario {
  -Id : Guid
  -Email : string <<unique>>
  -CriadoEm : DateTime
  --
  +Publicar(conteudo) : Post
  +Excluir(postId) : void
}

Usuario "1" *-- "0..*" Post : escreve
@enduml
```
````

> [!tip] Visibilidade
> `-` privado, `+` público, `#` protegido, `~` pacote. Use `--` para separar atributos de métodos.

## component — arquitetura
````markdown
```plantuml
@startuml
skinparam componentStyle rectangle

[FeedViewModel] --> [ObterFeedService]
[ObterFeedService] --> [IPostRepository]
[IPostRepository] ..> [EF Core Repository] : implementação
@enduml
```
````

> [!tip] Notações
> `-->` dependência, `..>` dependência tracejada, `database` para bancos, `package` para agrupar camadas.

## state — máquinas de estado
````markdown
```plantuml
@startuml
[*] --> Rascunho
Rascunho --> Publicado : Publicar()
Publicado --> Editado : Autor edita
Editado --> [*]
@enduml
```
````

> [!tip] Sintaxe
> `[*]` estado inicial/final. `-->` transição. `: rótulo` na transição. PlantUML suporta estados compostos e entry/exit points.

## use case — diagrama de casos de uso
````markdown
```plantuml
@startuml
left to right direction

rectangle "Agora" as Sistema {
  usecase "UC-01 Cadastrar" as UC1
  usecase "UC-02 Autenticar" as UC2
}

actor "Visitante" as V
V --> UC1
V --> UC2
@enduml
```
````

> [!tip] Direção
> `left to right direction` para layout horizontal. `top to bottom` para vertical (padrão). Use `include`/`extend` para relacionamentos entre UCs.

## Armadilhas comuns
1. Sempre begin/end com `@startuml` / `@enduml` — sem eles o plugin não renderiza
2. `skinparam` para estilizar: `classAttributeIconSize 0` oculta ícones de visibilidade
3. `hide circle` remove o círculo de composição da classe
4. Labels com espaço: use `"texto com espaço"` ou `~` para neutralizar
5. `note left of`, `note right of`, `note bottom of` para anotações no diagrama
6. `left to right direction` inverte eixo do use case/component diagram

## Referência oficial
https://plantuml.com/
