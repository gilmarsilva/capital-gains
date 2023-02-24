namespace CapitalGains;

/// <summary>
/// Envelope to operate in the stock buying and selling machine
/// </summary>
/// <param name="OperationType"></param>
/// <param name="UnitCost"></param>
/// <param name="Quantity"></param>
public record OperationIn(OperationType OperationType, double UnitCost, int Quantity)
{
    /// <summary>
    /// Unit price, non-negative greater than zero
    /// </summary>
    public double UnitCost { get; } = UnitCost > 0 ? UnitCost :
        throw new ArgumentException("Argument cannot be negative or zero.", nameof(UnitCost));

    /// <summary>
    /// Number of shares traded (positive integer)
    /// </summary>
    public int Quantity { get; } = Quantity > 0 ? Quantity :
        throw new ArgumentException("Argument cannot be negative or zero.", nameof(Quantity));

    /// <summary>
    /// Transaction value, unit price x quantity
    /// </summary>
    public double OperationValue { get { return Quantity * UnitCost; }}
}

/// <summary>
/// Result of the purchase operation, with the weighted average of the operation
/// </summary>
/// <param name="BoughtQuantity"></param>
/// <param name="AveragePrice"></param>
public record OperationOut(int BoughtQuantity, double AveragePrice)
{
    /// <summary>
    /// Unit price, non-negative greater than zero
    /// </summary>
    public int BoughtQuantity { get; } = BoughtQuantity >= 0 ? BoughtQuantity :
        throw new ArgumentException("Argument cannot be negative.", nameof(BoughtQuantity));

    /// <summary>
    /// Number of shares traded (positive integer)
    /// </summary>
    public double AveragePrice { get; } = AveragePrice >= 0 ? AveragePrice :
        throw new ArgumentException("Argument cannot be negative.", nameof(AveragePrice));

}