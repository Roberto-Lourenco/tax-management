## ⚠️ Padrão de Resposta de Erros (ProblemDetails)

Esta API utiliza o padrão da indústria **[RFC 7807 - Problem Details for HTTP APIs](https://tools.ietf.org/html/rfc7807)** para normalizar as respostas de erro.

O objetivo é garantir que todo erro retornado pela API (seja 400, 404, 500, etc.) siga uma estrutura JSON previsível, permitindo que os clientes (Frontend, Mobile) implementem um tratamento de exceções centralizado.

### Estrutura do Objeto de Erro

O objeto JSON retornado em casos de falha contém campos padrão da RFC e uma extensão customizada chamada `errors` para detalhamento granular.

### Schema JSON

```json
{
  "type": "[https://tools.ietf.org/html/rfc7231#section-6.5.1](https://tools.ietf.org/html/rfc7231#section-6.5.1)",
  "title": "Bad Request",
  "status": 400,
  "detail": "Um ou mais erros de validação ocorreram.",
  "errors": [
    {
      "code": "Domain.Error.Code",
      "description": "Descrição legível do erro para o usuário final."
    }
  ]
}
```

### Definição dos Campos

| Campo | Tipo | Descrição |
| :--- | :--- | :--- |
| `type` | String (URI) | Identifica o tipo do problema. Geralmente aponta para a documentação do código de status HTTP. |
| `title` | String | Resumo curto e legível do tipo de problema (ex: "Bad Request", "Not Found"). |
| `status` | Integer | O código de status HTTP (ex: 400, 404, 409). |
| `detail` | String | Explicação específica sobre esta ocorrência do erro. |
| **`errors`** | Array | **Extensão Customizada:** Lista de objetos contendo os erros específicos. Útil para validações de formulário ou regras de negócio múltiplas. |

---

## Exemplos de Respostas

### 1. Erro de Validação (400 Bad Request)
Ocorre quando os dados enviados não atendem aos contratos da API. O array `errors` listará cada campo inválido.

```json
{
  "type": "[https://tools.ietf.org/html/rfc7231#section-6.5.1](https://tools.ietf.org/html/rfc7231#section-6.5.1)",
  "title": "Bad Request",
  "status": 400,
  "detail": "Um ou mais erros de validação ocorreram.",
  "errors": [
    {
      "code": "TaxEntry.TotalOrder.TotalAmountInvalid",
      "description": "O valor total do pedido não pode ser negativo."
    },
    {
      "code": "TaxEntry.CompetenceDate.CannotBeBeforeCurrent",
      "description": "A nova data de competência não pode ser anterior à atual."
    }
  ]
}
```

### 2. Recurso Não Encontrado (404 Not Found)
Ocorre quando um ID ou recurso solicitado não existe.

```json
{
  "type": "[https://tools.ietf.org/html/rfc7231#section-6.5.4](https://tools.ietf.org/html/rfc7231#section-6.5.4)",
  "title": "Not Found",
  "status": 404,
  "detail": "O recurso solicitado não foi encontrado.",
  "errors": [
    {
      "code": "TaxEntry.Entity.NotFound",
      "description": "Nenhum Registro Fiscal encontrado."
    }
  ]
}
```

### 3. Conflito / Regra de Negócio (409 Conflict)
Ocorre quando a requisição é válida, mas o estado atual do servidor não permite a operação.

```json
{
  "type": "[https://tools.ietf.org/html/rfc7231#section-6.5.8](https://tools.ietf.org/html/rfc7231#section-6.5.8)",
  "title": "Conflict",
  "status": 409,
  "detail": "Operação não permitida no estado atual.",
  "errors": [
    {
      "code": "TaxEntry.OrderId.AlreadyExists",
      "description": "Já existe um Registro Fiscal com o mesmo ID de Pedido."
    }
  ]
}
```

---



