# Calculadora de Salário Líquido
Web api que atende uma requisição http get com dois parâmetros passados por query:

- salario: salário bruto sobre o qual será calculado o salário líquido.
- dependentes: número de dependentes de quem ganha o salario.

A aplicação atende na url \<host\>/api/calculadora . Exemplo de chamada:
`<host>/api/calucladora?salario=2000&dependentes=2`

A saída é um json no seguinte formato:
```
{
  "salarioLiquido": 0,
  "salarioBruto": 0,
  "descontoInss": 0,
  "descontoDependentes": 0,
  "descontoIRRF": 0
}
```
Os números apresentados na saída são arredondados para até duas casas decimais.