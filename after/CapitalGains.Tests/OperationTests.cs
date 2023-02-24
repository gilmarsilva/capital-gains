using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapitalGains.Tests
{
    [TestClass()]
    public class OperationTests
    {
        [TestMethod]
        public void check_operation_for_not_positive_quantity()
        {
            // Assert
            Assert.ThrowsException<ArgumentException>(() => {
                // Arrange           
                OperationIn buy = new(OperationType.Buy, -1, 10);

            });
        }

        [TestMethod]
        public void check_operation_for_zero_quantity()
        {
            // Assert
            Assert.ThrowsException<ArgumentException>(() => {
                // Arrange
                OperationIn buy = new(OperationType.Buy, 0, 10);

            });
        }

        [TestMethod]
        public void check_operation_for_zero_unity_cost()
        {
            // Assert
            Assert.ThrowsException<ArgumentException>(() => {
                // Arrange
                OperationIn buy = new(OperationType.Buy, 10, 0);

            });
        }

        [TestMethod]
        public void check_operation_for_negative_unity_cost()
        {
            // Assert
            Assert.ThrowsException<ArgumentException>(() => {
                // Arrange
                OperationIn buy = new(OperationType.Buy, 10, -1);

            });
        }

    }
}
