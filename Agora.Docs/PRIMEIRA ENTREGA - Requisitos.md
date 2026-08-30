# Requisitos
## O que o Levantamento deve Contemplar?
1. Requisitos Funcionais (RF): O que o sistema deve fazer (ex: casos de
uso ou histórias de usuário com critérios de aceite).
2. Requisitos Não Funcionais (RNF): Atributos de qualidade (ISO/IEC — tempo de resposta máximo, SLA de disponibilidade, taxa de requisições por segundo, conformidade com LGPD/GDPR, etc.
3. Modelagem de Dados Inicial: Diagrama Entidade-Relacionamento (DER)
ou modelo conceitual das principais entidades do banco de dados.
4. Arquitetura de Código e Classes: Diagrama de classes simplificado
contendo os principais domínios e regras de negócio.
5. Estratégia de Repositório e CI/CD: Escolha do controle de versão
(GitHub/GitLab), estratégia de ramificação (GitFlow, GitHub Flow) e
políticas de Code Review (ex: aprovação obrigatória de Pull Requests).
## Diretrizes para os Entregáveis
### 1. Documento de Especificação (.docx / Word)
O documento final deve seguir o padrão de um relatório técnico profissional de Engenharia de Software.
Estrutura Obrigatória:
 1. Capa e Sumário: Identificação da instituição, disciplina, integrantes e índice estruturado.
 2. Visão Geral do Sistema: Escopo, objetivo do produto e público-alvo.
 3. Matriz de Requisitos: 
   - Tabela contendo: ID, Tipo (RF/RNF), Descrição, Prioridade (Alta/Média/Baixa) e Critério de Aceite.
1. Modelagem de Dados e Arquitetura:
   - Esquema do banco de dados (tabelas/coleções e relacionamentos).
   - Diagrama de Classes indicando atributos, métodos e relacionamentos principais.
5. Estratégia de Versionamento e Qualidade:
   - Definição do repositório remoto (link configurado se aplicável).
   - Estratégia de branches, convenções de commit e critérios mínimos para merge.
6. Plano de Garantia de Qualidade (QA):
   - Descrição de pelo menos 3 cenários de teste (Entrada, Passo a Passo, Resultado Esperado).

### 2. Apresentação Executiva e Técnica (.pptx / PowerPoint)
A apresentação simulará um pitch técnico para a equipe de arquitetura e liderança de produto (Stakeholders). 
#### Estrutura da Apresentação (Limite: 8 a 10 slides):
• **Slide 1:** Capa (Nome do projeto, grupo e disciplina).
• **Slide 2:** Problema e Escopo da Solução.
• **Slide 3:** Principais Requisitos Funcionais e Fluxo Crítico do Usuário.
• **Slide 4:** Requisitos Não Funcionais e Metas de Qualidade (SLA,
Performance, Segurança).
• **Slide 5:** Arquitetura de Dados (DER / Modelo Conceitual).
• **Slide 6:** Diagrama de Classes e Regras de Negócio do Domínio Principal.
• **Slide 7:** Pipeline de Desenvolvimento (Git, Repositório e Code Review).
• **Slide 8:** Estratégia de Testes e Validação de Qualidade.
• **Slide 9:** Conclusão e Desafios Identificados.
#### Regras para os Slides:
• **Tempo de Apresentação:** 10 a 15 minutos por grupo, seguido de 5
minutos para perguntas.
• **Design:** Utilizar contraste adequado entre texto e fundo; priorizar
esquemas visuais, diagramas e tópicos curtos (bullet points) em vez de
blocos extensos de texto.
• **Participação:** Todos os integrantes do grupo devem apresentar uma
parte do trabalho.

| Critério                    | Peso | Descrição                                                                                     |
| --------------------------- | ---- | --------------------------------------------------------------------------------------------- |
| Completes dos Requisitos    | 25%  | Clareza e cobertura dos RFs e RNFs alinhados ao tema escolhido.                               |
| Rastreabilidade e Qualidade | 20%  | Definição clara de critérios de aceite e plano de testes executável.                          |
| Modelagem Técnica           | 25%  | Coerência da modelagem de banco de dados, diagramas de classe e arquitetura de versionamento. |
| Documentação (Word)         | 15%  | Organização, padronização técnica, clareza textual e formatação.                              |
| Apresentação (PowerPoint)   | 15%  | Domínio do conteúdo, clareza na exposição, qualidade visual dos slides e gestão do tempo.     |

# Verificação da Entrega

A verificação dos 5 tópicos (RF, RNF, DER/MER, Classes, Repo/CI-CD) — com os **RNFs do MVP**, as **metas de qualidade** e as **ferramentas de validação** (ex.: NBomber para o throughput ≥ 50 req/s) — está na nota própria:

**→ [[04 Gestão/Verificação da Primeira Entrega]]**