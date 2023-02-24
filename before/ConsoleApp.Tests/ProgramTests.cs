using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConsoleApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Tests;

[TestClass()]
public class ProgramTests
{

    [TestMethod]
    public void check_operation_for_not_positive_quantity() 
    {
        // Assert
        Assert.ThrowsException<ArgumentException>(() => {
            // Arrange           
            Operation buy = new(OperationType.Buy, -1, 10);

        });
    }

    [TestMethod]
    public void check_operation_for_zero_quantity()
    {
        // Assert
        Assert.ThrowsException<ArgumentException>(() => {
            // Arrange
            Operation buy = new(OperationType.Buy, 0, 10);

        });
    }

    [TestMethod]
    public void check_operation_for_zero_unity_cost()
    {
        // Assert
        Assert.ThrowsException<ArgumentException>(() => {
            // Arrange
            Operation buy = new(OperationType.Buy, 10, 0);

        });
    }

    [TestMethod]
    public void check_operation_for_negative_unity_cost()
    {
        // Assert
        Assert.ThrowsException<ArgumentException>(() => {
            // Arrange
            Operation buy = new(OperationType.Buy, 10, -1);

        });
    }

    [TestMethod]
    public void process_operation_orders() 
    {
        //arrange
        var operationList = new List<Operation>();

        operationList.Add(new Operation(OperationType.Buy, 100, 500));
        operationList.Add(new Operation(OperationType.Buy, 200, 500));

        //act
        var result = Program.Execute(operationList);

        var operationResults = new List<OperationResult>();
        operationResults.Add(new OperationResult(500, 100));
        operationResults.Add(new OperationResult(1000, 150));


        //Asset 
        for (int i = 0; i < result.Count; i++)
        {
            Assert.AreEqual(result[i], operationResults[i]);
        }
    }

}