namespace CapitalGains;

public class Stock
{
    public static Stock Empty() => new Stock(0, 0, 0, 0);

    public int InStock { get; private set; }
    public double ValueOfPastOperations { get; private set; }
    public int BoughtQuantity { get; private set; }
    public double LastAvgPrice { get; private set; }

    public Stock() { }
    private Stock(int InStock, double ValueOfPastOperations, int BoughtQuantity, double LastAvgPrice)
    {
        this.InStock = InStock;
        this.ValueOfPastOperations = ValueOfPastOperations;
        this.BoughtQuantity = BoughtQuantity;
        this.LastAvgPrice = LastAvgPrice;
    }

    public void Buy(int Quantity, double UnitPrice)
    {
        //Atualizamos o valor preco-medio-atual com o resultado obtido no cálculo
        ComputeLastAvgPrice(Quantity, UnitPrice);

        //Atualizamos o valor da quantidade-comprada, somando o número de ações compradas
        this.InStock += Quantity;

        this.BoughtQuantity += Quantity;

        this.ValueOfPastOperations += ComputeOperationValue(Quantity, UnitPrice);
    }

    public void Sell(int Quantity, double UnitPrice)
    {
        //A cada operação de Venda:
        //Preço médio não é alterado em caso de venda parcial
        this.InStock -= Quantity;

        this.ValueOfPastOperations -= ComputeOperationValue(Quantity, UnitPrice);

        //Preço médio e a quantidade comprada são alterados para 0 em caso de venda total
        if (this.InStock == 0)
        {
            ResetAmountAndBoughtQuantity();
        }
    }

    private void ResetAmountAndBoughtQuantity()
    {
        LastAvgPrice = 0;
        BoughtQuantity = 0;
    }

    private double ComputeOperationValue(int Quantity, double UnitPrice) 
    { 
        return Quantity * UnitPrice;
    }

    private void ComputeLastAvgPrice(int Quantity, double UnitPrice)
    {
        var currentTotal = this.InStock + Quantity;
        var currentPrice = this.ValueOfPastOperations + ComputeOperationValue(Quantity, UnitPrice);
        this.LastAvgPrice = Math.Round(currentPrice / currentTotal, 2);
    }
}
