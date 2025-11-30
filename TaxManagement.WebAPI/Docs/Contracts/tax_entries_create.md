>*Ultima atualização: 30/11/2025*

# Create Tax Entry

Cria uma nova entrada fiscal baseada em um pedido de venda. Este endpoint recebe os dados brutos do pedido e retorna o cálculo das taxas associadas.

## 📄 Endpoint

`POST /api/tax-entries`

### Request Body

O corpo da requisição deve conter os dados do pedido.

| Campo | Tipo | Obrigatório | Descrição |
| :--- | :--- | :---: | :--- |
| `orderId` | UUID | Sim | Identificador único do pedido. |
| `totalAmountReceived` | Double | Sim | Valor total recebido no pedido. |
| `originState` | String | Sim | Sigla do estado de origem (ex: SP). |
| `customerState` | String | Sim | Sigla do estado do cliente/destino (ex: RJ). |
| `orderDate` | DateTime | Sim | Data e hora da realização do pedido com UTC explícito. |

#### 📝 Exemplo de Requisição (JSON)

```json
{
  "orderId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "totalAmountReceived": 1500.50,
  "originState": "SP",
  "customerState": "RJ",
  "orderDate": "2023-10-27T14:30:00Z"
}
```

### Responses

### ✅ 200 OK
Retorna os detalhes da entrada fiscal criada, incluindo o imposto calculado.

| Campo | Tipo | Descrição |
| :--- | :--- | :--- |
| `id` | `UUID` | Identificador único da entrada fiscal. |
| `orderId` | `UUID` | Identificador do pedido associado. |
| `totalOrderTax` | `Double` | Valor total do imposto calculado. |
| `createdAt` | `DateTime` | Data de processamento. |

#### 📝 Exemplo de Resposta (JSON)
```json
{
  "id": "7fa85f64-5717-4562-b3fc-2c963f66afa7",
  "orderId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "totalOrderTax": 120.75,
  "createdAt": "2023-10-27T14:35:00Z"
}
```

### ⚠️ 400 Bad Request
Indica que a requisição é inválida, geralmente devido a dados ausentes ou incorretos.

>Para mais detalhes de erros, consulte o arquivo de padronização de errors : [`problem_details_json.md`](problem_details_json.md).