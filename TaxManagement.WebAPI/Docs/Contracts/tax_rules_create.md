>*Ultima atualização: 30/11/2025*

# Create Tax Rule

Cria uma nova regra fiscal no sistema, definindo alíquotas interestaduais e datas de vigência.

## 📄 Endpoint

`POST /api/tax-rules`

### Request Body

Objeto contendo as definições das alíquotas.

| Campo | Tipo | Obrigatório | Descrição |
| :--- | :--- | :---: | :--- |
| `originState` | String | Sim | Estado de origem. |
| `destinationState` | String | Sim | Estado de destino. |
| `interstateRate` | Double | Sim | Alíquota interestadual (ex: 0.12 para 12%). |
| `difalRate` | Double | Sim | Alíquota do DIFAL. |
| `fcpRate` | Double | Sim | Alíquota do Fundo de Combate à Pobreza. |
| `effectiveDate` | DateTime | Sim | Data de início da vigência da regra. |

### Exemplo de Requisição (JSON)

```json
{
  "originState": "SP",
  "destinationState": "RJ",
  "interstateRate": 0.12,
  "difalRate": 0.06,
  "fcpRate": 0.02,
  "effectiveDate": "2024-01-01T00:00:00Z"
}
```
## 📤 Responses

### ✅ 201 Created
A regra fiscal foi criada com sucesso. Não retorna conteúdo no corpo (conforme especificação atual).

### ⚠️ 400 Bad Request
Indica que a requisição é inválida, geralmente devido a dados ausentes ou incorretos.

>Para mais detalhes de erros, consulte o arquivo de padronização de errors : [`problem_details_json.md`](problem_details_json.md).

