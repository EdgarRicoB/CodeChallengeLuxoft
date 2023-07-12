using CodeChallengeLuxoft.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSUnitTest
{
    [TestClass]
    public class CurrencyModelsTest
    {
        /// <summary>
        /// Test that the model gets the currencies for Mexico
        /// </summary>
        [TestMethod]
        public void GetMexicoCurrencyTest()
        {
            MexicoModel model = new MexicoModel();
            var currency = model.CurrencyList;
            Assert.IsTrue(currency.Any());
        }

        /// <summary>
        /// Test that the model gets the currencies for United States of America
        /// </summary>
        [TestMethod]
        public void GetUSCurrencyTest()
        {
            UnitedStatesModel model = new UnitedStatesModel();
            var currency = model.CurrencyList;
            Assert.IsTrue(currency.Any());
        }
    }
}
