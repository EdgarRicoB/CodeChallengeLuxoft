using CodeChallengeLuxoft.BusinessLogic;
using CodeChallengeLuxoft.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace POSUnitTest
{
    [TestClass]
    public class SaleProcessTests
    {
        /// <summary>
        /// Test the functionality of the SaleProcess.GetPayment method
        /// </summary>
        /// <param name="money">The input string for the money</param>
        [TestMethod]
        [DataRow("3$100.00, 2$50.00, 1$10.00, 1$0.50")]
        [DataRow("1$50.00, 2$20.00, 5$0.50")]
        [DataRow("7$10.00, 2$50.00, 1$100.00, 1$0.10")]
        public void GetPaymentTest(string money)
        {
            var payment = SaleProcess.GetPayment(money);

            Assert.IsTrue(payment.Any());
        }

        /// <summary>
        /// Test the functionality of the SaleProcess.ValidateTotal method
        /// </summary>
        /// <param name="inputTotal">The input string for the total</param>
        [TestMethod]
        [DataRow("999.00")]
        [DataRow("404.00")]
        [DataRow("0.75")]
        public void ValidateTotalTest(string inputTotal)
        {
            var total = SaleProcess.ValidateTotal(inputTotal);

            Assert.AreEqual(total, decimal.Parse(inputTotal));
        }

        /// <summary>
        /// Test the functionality of the SaleProcess.ValidateAmounts method
        /// </summary>
        /// <param name="inputTotal">The input string for the total</param>
        [TestMethod]
        [DataRow("999.00")]
        [DataRow("404.00")]
        [DataRow("0.75")]
        public void ValidateAmountsTest(string inputTotal)
        {
            try
            {
                var payment = new List<Currency>() { new Currency() { Value = 100, Quantity = 10 } };
                SaleProcess.ValidateAmounts(decimal.Parse(inputTotal), payment);

            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        /// <summary>
        /// Test the functionality of the SaleProcess.GetChange method
        /// </summary>
        /// <param name="inputTotal">The input string for the total</param>
        [TestMethod]
        [DataRow("999.00")]
        [DataRow("404.00")]
        [DataRow("0.75")]
        public void GetChangeTest(string inputTotal)
        {
            try
            {
                var total = decimal.Parse(inputTotal);
                var payment = new List<Currency>() { new Currency() { Value = 100, Quantity = 10 } };
                var change = SaleProcess.GetChange(total, payment);
                Assert.IsTrue(change.Any());
                Assert.IsTrue(change.Select(x => x.Quantity * x.Value).Sum() + total == payment.Select(x => x.Quantity * x.Value).Sum());
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }
    }
}
