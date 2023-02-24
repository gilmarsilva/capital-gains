Click the button below to start a new development environment:

[![Open in Gitpod](https://gitpod.io/button/open-in-gitpod.svg)](https://gitpod.io/#https://github.com/gilmarsilva/capital-gains)


## Intro
O exerc�cio que iremos desenvolver juntos consiste de v�rias tarefas que v�o ser apresentadas � medida que a etapa anterior tiver sido completada.
Em todas as etapas, estaremos trabalhando em uma fun��o de l�gica que deve receber um conjunto de dados como argumento, e retornar outra estrutura de dados como resultado.
Voc� n�o vai precisar se preocupar com assuntos como:

- Serializa��o e Deserializa��o de dados
- Rede
- Banco de Dados e/ou armazenamento de estado persistente

Sinta-se � vontade para utilizar estruturas de dados da pr�pria linguagem de programa��o escolhida, e realizar os testes ao longo do processo usando testes unit�rios, REPL, ou qualquer outra forma que preferir.

Vamos trabalhar com um problema que envolve um conjunto de Opera��es de Compra e Venda de a��es. Para todas as partes do exerc�cio vamos trabalhar com o mesmo conjunto de dados de entrada:

### Entrada da fun��o

O par�metro da fun��o � uma lista de opera��es do mercado financeiro de a��es, em ordem cronol�gica. Cada opera��o desta lista cont�m os seguintes atributos:

|   Nome   | Significado |
|----------|-------------|
| type     | Se � uma opera��o de compra (buy) ou venda (sell) |
| unitCost | Pre�o unit�rio da a��o em uma moeda com at� duas casas decimais |
| quantity | Quantidade de a��es negociadas (n�mero inteiro positivo) |

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

Desse modo, aqui temos primeiro uma opera��o de Compra (100 unidades com o valor de $10), seguida de uma opera��o de Venda (50 unidades com o valor de $15).

## Pre�o m�dio ponderado a cada Opera��o

Nessa primeira etapa, esperamos que nossa fun��o receba uma lista de Opera��es, e para cada uma delas calcule o **pre�o m�dio ponderado** at� o momento.
O **pre�o m�dio ponderado** � constitu�do da [m�dia ponderada](https://pt.wikipedia.org/wiki/M%C3%A9dia_aritm%C3%A9tica_ponderada) do pre�o das a��es.

Para calcular a m�dia ponderada a cada opera��o, podemos usar o seguinte conjunto de regras:

* O valor inicial da m�dia ponderada, e do n�mero de a��es, � 0
* Definimos:
  * valor-da-operacao = quantidade-op * preco-unitario 
  * valor-das-operacoes-passadas = quantidade-compradas * preco-medio-atual
  * total-de-acoes = quantidade-compradas + quantidade-op
  * preco-medio = (valor-da-operacao + valor-das-operacoes-passadas) / total-de-acoes
* A cada opera��o de Compra:
  * Executamos o c�lculo de preco-medio
  * Atualizamos o valor da quantidade-comprada, somando o n�mero de a��es compradas
  * Atualizamos o valor preco-medio-atual com o resultado obtido no c�lculo
* A cada opera��o de Venda:
  * Pre�o m�dio n�o � alterado em caso de venda parcial
  * Pre�o m�dio e a quantidade comprada s�o alterados para 0 em caso de venda total

A imagem abaixo mostra um exemplo de como a m�dia ponderada � calculada para essas tr�s opera��es:

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

A m�dia ponderada � alterada em todas as opera��es de compra, e � zerada quando todas as unidades s�o vendidas.

### Sa�da da fun��o

A sa�da esperada da fun��o � uma lista com o resultado de cada opera��o, com as seguintes propriedades:

- `avg_price`: pre�o ponderado at� aquela opera��o
- `bought_quantity`: n�mero de a��es adquiridas na carteira at� aquela opera��o

A lista deve conter itens em mesma quantidade e ordem que as opera��es de entrada.

### Exemplo de C�digo

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