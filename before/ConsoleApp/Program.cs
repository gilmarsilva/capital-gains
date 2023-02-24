using System.Reflection.Metadata.Ecma335;

namespace ConsoleApp;

public class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }

    public static int InStock { get; set; }

    public static double PastTradesValue { get; set; }

    public static List<OperationResult> Execute(List<Operation> operations)
    {
        var resultList = new List<OperationResult>();

        foreach (var item in operations)
        {
            switch (item.Type)
            {
                case OperationType.Buy:
                    resultList.Add(ExecuteBuy(item));
                    break;
                case OperationType.Sell:
                    resultList.Add(ExecuteSell(item));
                    break;
                default:
                    break;
            }
        }

        return resultList;
    }

    public static OperationResult ExecuteSell(Operation operation) 
    {

        return null;
    }

    public static OperationResult ExecuteBuy(Operation operation) 
    {
        var operationValue = ProcessOperationValue(operation.Quantity, operation.UnitCost);

        var currentPrice = PastTradesValue + operationValue;

        var currentTotal = InStock + operation.Quantity;

        var AvgPrice = currentPrice / currentTotal;

        PastTradesValue = currentPrice;
        
        InStock = currentTotal;

        return new OperationResult(InStock, AvgPrice);
    }

    public static double ProcessOperationValue(int Quantity, double UnitCost) 
    {
        return Quantity * UnitCost;
    }
}

public record Operation(OperationType Type, double UnitCost, int Quantity)
{
    public double UnitCost { get; } = UnitCost > 0 ? UnitCost : 
        throw new ArgumentException("Argument cannot be negative.", nameof(UnitCost));
    public int Quantity { get; } = Quantity > 0 ? Quantity : 
        throw new ArgumentException("Argument cannot be negative.", nameof(Quantity));
}

public record OperationResult(int BoughtQuantity, double AvgPrice);

public enum OperationType
{
    Buy = 0,
    Sell = 1,
}
