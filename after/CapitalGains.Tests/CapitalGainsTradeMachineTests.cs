using Microsoft.VisualStudio.TestTools.UnitTesting;
using CapitalGains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapitalGains.Tests
{
    [TestClass()]
    public class CapitalGainsTradeMachineTests
    {
        [TestMethod()]
        public void when_bid_bought_quantity_up()
        {
            // Arrange         
            OperationIn[] operations = new OperationIn[]
             {
                new OperationIn(OperationType.Buy, 100.00, 500),
                new OperationIn(OperationType.Buy, 200.00, 500),
                new OperationIn(OperationType.Buy, 300.00, 1000),
                new OperationIn(OperationType.Sell, 250.00, 1000),
                new OperationIn(OperationType.Buy, 100.00, 1000),
                new OperationIn(OperationType.Sell, 50.00, 2000),
             };

            ITradeMachine tradeMachine = new CapitalGainsTradeMachine();

            //Act
            var operationOut = tradeMachine.Bid(operations);

            //Assert
            Assert.AreEqual(operationOut[0].BoughtQuantity, 500);
            Assert.AreEqual(operationOut[1].BoughtQuantity, 1000);
            Assert.AreEqual(operationOut[2].BoughtQuantity, 2000);
            Assert.AreEqual(operationOut[3].BoughtQuantity, 2000);
            Assert.AreEqual(operationOut[4].BoughtQuantity, 3000);
            Assert.AreEqual(operationOut[5].BoughtQuantity, 0);


        }

        [TestMethod()]
        public void when_bid_check_avg_price()
        {
            // Arrange         
            OperationIn[] operations = new OperationIn[]
             {
                new OperationIn(OperationType.Buy, 100.00, 500),
                new OperationIn(OperationType.Buy, 200.00, 500),
                new OperationIn(OperationType.Buy, 300.00, 1000),
                new OperationIn(OperationType.Sell, 250.00, 1000),
                new OperationIn(OperationType.Buy, 100.00, 1000),
                new OperationIn(OperationType.Sell, 50.00, 2000),
             };

            ITradeMachine tradeMachine1 = new CapitalGainsTradeMachine();

            //Act
            var operationOut = tradeMachine1.Bid(operations);

            //Assert
            Assert.AreEqual(operationOut[0].AveragePrice, 100.00);
            Assert.AreEqual(operationOut[1].AveragePrice, 150.00);
            Assert.AreEqual(operationOut[2].AveragePrice, 225.00);
            Assert.AreEqual(operationOut[3].AveragePrice, 225.00);
            
            //TODO: Check the rule for this
            //Assert.AreEqual(operationOut[4].AveragePrice, 183.33);
            Assert.AreEqual(operationOut[5].AveragePrice, 0);
        }
    }
}