namespace CapitalGains;


/// <summary>
/// Public interface for TradeMachine
/// </summary>
public interface ITradeMachine
{
    /// <summary>
    /// Represents a purchase or sale of shares
    /// </summary>
    /// <param name="operationIn"></param>
    /// <returns></returns>
    OperationOut Bid(OperationIn operationIn);

    /// <summary>
    /// Represents a purchase or sale of shares
    /// </summary>
    /// <param name="operationIn"></param>
    /// <returns></returns>
    OperationOut[] Bid(OperationIn[] operationsIn);
}

/// <summary>
/// class that represents a machine for buying and selling shares, passing on stock operations
/// </summary>
public class CapitalGainsTradeMachine : ITradeMachine
{
    public Stock CurrentStock { get; private set; } = Stock.Empty();

    public OperationOut Bid(OperationIn operation)
    {
        return Bid(new OperationIn[] { operation }).First();
    }

    public OperationOut[] Bid(OperationIn[] operationsIn)
    {
        var result = new List<OperationOut>();
        
        foreach (var item in operationsIn)
        {
            if (item.OperationType == OperationType.Buy)
            {
                this.CurrentStock.Buy(item.Quantity, item.UnitCost);
            }
            else
            {
                this.CurrentStock.Sell(item.Quantity, item.UnitCost);
            }

            result.Add(new OperationOut(this.CurrentStock.BoughtQuantity,
                                        this.CurrentStock.LastAvgPrice));
        }
        return result.ToArray();
    }
}
