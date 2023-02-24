using CapitalGains;

namespace CapitalGains
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ITradeMachine tradeMachine = new CapitalGainsTradeMachine();

            OperationIn[] operations = new OperationIn[]
            {
                new OperationIn(OperationType.Buy, 100.00, 500),
                new OperationIn(OperationType.Buy, 200.00, 500),
                new OperationIn(OperationType.Buy, 300.00, 1000),
                new OperationIn(OperationType.Sell, 250.00, 1000),
                new OperationIn(OperationType.Buy, 100.00, 1000),
                new OperationIn(OperationType.Sell, 50.00, 2000),
            };

            var operationOut = tradeMachine.Bid(operations);

            foreach (var item in operationOut)
            {
                Console.WriteLine(item.ToString());
            }
        }
    }
}




