---
tags: [privacidade, lgpd, requisitos]
tipo: documento
status: rascunho
atualizado: 2026-08-29
---

# LGPD e Privacidade

> [!info] Propósito
> Documento de privacidade de **Agora**: leis aplicáveis, bases legais, inventário de dados pessoais, retenção, consentimento, notificação de violação e transparência. Consolida os requisitos RF-032/033/034, RNF-09/26 e RN-04/11/12.

## 1. Leis aplicáveis

| Lei | Aplicável? | Por quê |
|---|---|---|
| **LGPD** (BR) | ✅ Sim — hoje | Público-alvo brasileiro (UNA-Contagem, betas internos); dados processados para oferta do serviço |
| **GDPR/RGPD** (UE) | ⚠️ Não hoje | Não há estabelecimento nem oferta de serviço a titulares na UE; sem monitoramento de comportamento na UE (GDPR art. 2/3) |
| Princípios GDPR | ✅ Adotados como boas práticas | Minimização, limitação de armazenamento, accountability e privacidade *by design* — ver [[01 Requisitos/Requisitos Não Funcionais#RNF-26|RNF-26]] |

> [!note] Reavaliação
> Se o produto for ofertado a usuários na UE no futuro, dispara nova análise de GDPR (escopo de mercado, transferência internacional e DPO) — registrar antes da expansão.

## 2. Inventário de dados pessoais

| Dado | Onde vive | Finalidade | Base legal (LGPD art. 7) |
|---|---|---|---|
| E-mail | `USUARIO.email` | Identificação/login/recuperação | Execução de contrato (V) + consentimento (I) |
| Senha (hash) | `USUARIO.hash_senha` | Autenticação | Execução de contrato (V) |
| Nome de exibição, @apelido | `USUARIO.nome_exibicao`, `apelido` | Perfil público | Execução de contrato (V) |
| Bio, avatar (URL) | `PERFIL.bio`, `avatar_url` | Perfil público | Consentimento (I) |
| Provider OAuth + ID | `USUARIO.provider`, `provider_id` | Login via GitHub/Google (Fase 2/3) | Execução de contrato (V) |
| Posts, comentários, curtidas, seguidas | `POST`, `COMENTARIO`, `CURTIDA`, `SEGUIDA` | Funcionalidade social | Execução de contrato (V) |
| Consentimento (aceite da política) | (B-51, RF-034) | Registro de conformidade | Obrigação legal (II) |

## 3. Retenção e limitação de armazenamento

Política definida em [[01 Requisitos/Regras de Negócio#RN-11|RN-11]]:

- **Exclusão de conta:** dados pessoais anonimizados em **≤ 30 dias** ([[01 Requisitos/Requisitos Funcionais#RF-032|RF-032]], [[01 Requisitos/Regras de Negócio#RN-04|RN-04]])
- **Backups do banco de produção:** retidos **≤ 30 dias** — após isso, descartados/anonimizados ([[01 Requisitos/Requisitos Não Funcionais#RNF-21|RNF-21]], [[04 Gestão/Backlog do Produto#B-52|B-52]])
- **Dados anonimizados** deixam de ser dados pessoais (sem vínculo com o titular) — posts mantidos como "anônimos" é escolha do usuário na exclusão

## 4. Consentimento

- Registrado no **cadastro** (aceite da Política de Privacidade + Termos de Uso) — armazena **data/hora, versão da política aceita** ([[01 Requisitos/Requisitos Funcionais#RF-034|RF-034]], B-51)
- **Revogação** disponível a qualquer momento via UC-10 (gerenciar dados da conta)
- O consentimento é base legal complementar; a base principal é execução de contrato — ver seção 2

## 5. Direitos do titular (LGPD cap. III)

| Direito | Como é atendido |
|---|---|
| Acesso | Perfil completo via API própria (GET `/usuarios/me`) |
| Portabilidade | Exportação em formato legível ([[01 Requisitos/Requisitos Funcionais#RF-033|RF-033]] — GET `/usuarios/me/exportar`) |
| Correção | Edição de perfil (RF-003) |
| Eliminação/esquecimento | Exclusão de conta (RF-032) |
| Revogação de consentimento | UC-10 (revogar aceite) |
| Informação/transparência | Esta nota + Política de Privacidade (B-50) |

## 6. Segurança e mitigação

- Criptografia em trânsito: TLS 1.2+ ([[01 Requisitos/Requisitos Não Funcionais#RNF-07|RNF-07]])
- Hash adaptativo de senha: bcrypt cost ≥ 10 ([[01 Requisitos/Requisitos Não Funcionais#RNF-06|RNF-06]])
- Proteção contra força bruta no login ([[01 Requisitos/Requisitos Não Funcionais#RNF-08|RNF-08]])
- Logs estruturados sem dados sensíveis ([[01 Requisitos/Requisitos Não Funcionais#RNF-16|RNF-16]])
- Refresh token criptografado via DPAPI ([[03 Decisões/ADR-008 Segurança de Sessão (DPAPI)|ADR-008]])

## 7. Incidentes e notificação de violação

- Procedimento documentado no runbook ([[04 Gestão/Backlog do Produto#B-53|B-53]])
- **LGPD art. 48** (e princípio GDPR art. 33): violação com risco a titulares → notificar à ANPD e aos afetados em **prazo razoável** (alvo **≤ 72 h** da ciência) + registro interno do incidente ([[01 Requisitos/Regras de Negócio#RN-12|RN-12]])

## 8. Transparência

- **Política de Privacidade + Termos de Uso** entregues no cadastro (aceite registrado — RF-034) — [[04 Gestão/Backlog do Produto#B-50|B-50]]
- Lógica simples e auditável: feed cronológico **sem algoritmo** (RN-07) — reduz risco de perfilamento

---

Links: [[01 Requisitos/Requisitos Funcionais|RF]] · [[01 Requisitos/Requisitos Não Funcionais|RNF]] · [[01 Requisitos/Regras de Negócio|RN]] · [[04 Gestão/Definição do MVP|MVP]] · [[01 Requisitos/Casos de Uso#UC-10 — Gerenciar dados da conta|UC-10]]