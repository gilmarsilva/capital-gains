Click the button below to start a new development environment:

[![Open in Gitpod](https://gitpod.io/button/open-in-gitpod.svg)](https://gitpod.io/#https://github.com/gilmarsilva/capital-gains)


## Intro
O exercício que iremos desenvolver juntos consiste de várias tarefas que vão ser apresentadas à medida que a etapa anterior tiver sido completada.
Em todas as etapas, estaremos trabalhando em uma função de lógica que deve receber um conjunto de dados como argumento, e retornar outra estrutura de dados como resultado.
Você não vai precisar se preocupar com assuntos como:

- Serialização e Deserialização de dados
- Rede
- Banco de Dados e/ou armazenamento de estado persistente

Sinta-se à vontade para utilizar estruturas de dados da própria linguagem de programação escolhida, e realizar os testes ao longo do processo usando testes unitários, REPL, ou qualquer outra forma que preferir.

Vamos trabalhar com um problema que envolve um conjunto de Operações de Compra e Venda de ações. Para todas as partes do exercício vamos trabalhar com o mesmo conjunto de dados de entrada:

### Entrada da função

O parâmetro da função é uma lista de operações do mercado financeiro de ações, em ordem cronológica. Cada operação desta lista contém os seguintes atributos:

|   Nome   | Significado |
|----------|-------------|
| type     | Se é uma operação de compra (buy) ou venda (sell) |
| unitCost | Preço unitário da ação em uma moeda com até duas casas decimais |
| quantity | Quantidade de ações negociadas (número inteiro positivo) |

Desse modo, vamos ter uma lista da seguinte forma:

```javascript
operations = [
 {
   type: 'buy',
   unitCost: 10.00,
   quantity: 100
 },
 {
   type: 'sell',
   unitCost: 15.00,
   quantity: 50
 },
]
```

Desse modo, aqui temos primeiro uma operação de Compra (100 unidades com o valor de $10), seguida de uma operação de Venda (50 unidades com o valor de $15).

## Preço médio ponderado a cada Operação

Nessa primeira etapa, esperamos que nossa função receba uma lista de Operações, e para cada uma delas calcule o **preço médio ponderado** até o momento.
O **preço médio ponderado** é constituído da [média ponderada](https://pt.wikipedia.org/wiki/M%C3%A9dia_aritm%C3%A9tica_ponderada) do preço das ações.

Para calcular a média ponderada a cada operação, podemos usar o seguinte conjunto de regras:

* O valor inicial da média ponderada, e do número de ações, é 0
* Definimos:
  * valor-da-operacao = quantidade-op * preco-unitario 
  * valor-das-operacoes-passadas = quantidade-compradas * preco-medio-atual
  * total-de-acoes = quantidade-compradas + quantidade-op
  * preco-medio = (valor-da-operacao + valor-das-operacoes-passadas) / total-de-acoes
* A cada operação de Compra:
  * Executamos o cálculo de preco-medio
  * Atualizamos o valor da quantidade-comprada, somando o número de ações compradas
  * Atualizamos o valor preco-medio-atual com o resultado obtido no cálculo
* A cada operação de Venda:
  * Preço médio não é alterado em caso de venda parcial
  * Preço médio e a quantidade comprada são alterados para 0 em caso de venda total

A imagem abaixo mostra um exemplo de como a média ponderada é calculada para essas três operações:

1. Compra de 500 unidades a $100
2. Compra de 500 unidades a $200
3. Compra de 1000 unidades a $300

$$
\begin{gather}
  \color{green}{\frac{500 * \$100}{500}} &= \$100
  & (1)\\
  \frac
    {\color{grey}{500 * \$100} + \color{green}{500 * \$200}}
    {\color{grey}{500} + \color{green}{500}} &= \$150
  & (2)\\
  \frac
    {\color{grey}{500 * \$100 + 500 * \$200} + \color{green}{1000 * \$300}}
    {\color{grey}{500 + 500} + \color{green}{1000}} &= \$225
  & (3)
\end{gather}
$$

A média ponderada é alterada em todas as operações de compra, e é zerada quando todas as unidades são vendidas.

### Saída da função

A saída esperada da função é uma lista com o resultado de cada operação, com as seguintes propriedades:

- `avg_price`: preço ponderado até aquela operação
- `bought_quantity`: número de ações adquiridas na carteira até aquela operação

A lista deve conter itens em mesma quantidade e ordem que as operações de entrada.

### Exemplo de Código

```javascript
operations = [
  { type: "buy", unitCost: 100.00, quantity: 500 },
  { type: "buy", unitCost: 200.00, quantity: 500 },
  { type: "buy", unitCost: 300.00, quantity: 1000 },
  { type: "sell", unitCost: 250.00, quantity: 1000 },
  { type: "buy", unitCost: 100.00, quantity: 1000 },
  { type: "sell", unitCost: 50.00, quantity: 2000 }
]

result = execute(operations)

result == [
  { bought_quantity: 500, avg_price: 100.00 },
  { bought_quantity: 1000, avg_price: 150.00 },
  { bought_quantity: 2000, avg_price: 225.00 },
  { bought_quantity: 2000, avg_price: 225.00 },
  { bought_quantity: 3000, avg_price: 183.33 },
  { bought_quantity: 0, avg_price: 0.00 }
]
```