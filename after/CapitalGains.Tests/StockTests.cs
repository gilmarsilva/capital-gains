using Microsoft.VisualStudio.TestTools.UnitTesting;
using CapitalGains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;

namespace CapitalGains.Tests
{
    [TestClass()]
    public class StockTests
    {
        [TestMethod()]
        public void buy_stock()
        {
            // Arrange           
            Stock stock = Stock.Empty();

            // Act           
            stock.Buy(10, 5);
            
            // Assert           
            Assert.AreEqual(stock.BoughtQuantity, 10);
            Assert.AreEqual(stock.InStock, 10);
            Assert.AreEqual(stock.LastAvgPrice, 5);
        }

        [TestMethod()]
        public void buy_stock_with_two_operations()
        {
            // Arrange           
            Stock stock = Stock.Empty();

            // Act           
            stock.Buy(10, 5);
            stock.Buy(12, 5);
           
            // Assert           
            Assert.AreEqual(stock.BoughtQuantity, 22);
            Assert.AreEqual(stock.InStock, 22);
            Assert.AreEqual(stock.LastAvgPrice, 5);
        }


        [TestMethod()]
        public void when_sell_bougth_quantitity_not_change()
        {
            // Arrange           
            Stock stock = Stock.Empty();

            // Act           
            stock.Buy(10, 5);
            stock.Buy(12, 5);
            stock.Sell(10, 3);
           
            // Assert           
            Assert.AreEqual(stock.BoughtQuantity, 22);
        }

        [TestMethod()]
        public void when_sell_in_stock_decrease()
        {
            // Arrange           
            Stock stock = Stock.Empty();

            // Act           
            stock.Buy(10, 5);
            stock.Buy(12, 5);
            stock.Sell(10, 3);
            
            // Assert           
            Assert.AreEqual(stock.InStock, 12);
        }

        [TestMethod()]
        public void when_sell_all_stock_avg_and_bought_quantity_is_zero()
        {
            // Arrange           
            Stock stock = Stock.Empty();

            // Act           
            stock.Buy(10, 5);
            stock.Buy(12, 5);
            stock.Sell(22, 3);

            // Assert           
            Assert.AreEqual(stock.InStock, 0);
            Assert.AreEqual(stock.BoughtQuantity, 0);
            Assert.AreEqual(stock.LastAvgPrice, 0);
        }

        [TestMethod()]
        public void when_sell_last_avg_value_not_change()
        {
            // Arrange           
            Stock stock = Stock.Empty();

            // Act           
            stock.Buy(10, 5);
            stock.Buy(12, 5);
            stock.Sell(1, 3);

            // Assert           
            Assert.AreEqual(stock.LastAvgPrice, 5);
        }
    }
}