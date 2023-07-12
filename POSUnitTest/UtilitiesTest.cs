using CodeChallengeLuxoft.Enums;
using CodeChallengeLuxoft.Utilities;
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
    public class UtilitiesTest
    {
        /// <summary>
        /// Test that if a file does not exist for the logger it gets created
        /// </summary>
        [TestMethod]
        public void CreateLoggerTest()
        {
            string message = "Unit Test Logger";
            string filePath = "application.log";

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            Logger.log_message(message, nameof(LogLevels.Info), filePath);

            if (File.Exists(filePath))
            {
                using (StreamReader reader = File.OpenText(filePath))
                {
                    string text = reader.ReadToEnd();
                    Assert.IsNotNull(text);
                    if (!text.Contains(message))
                    {
                        Assert.Fail();
                    }
                }
            }
        }

        /// <summary>
        /// Test that if a file for the logger already exist it will append to that file
        /// </summary>
        [TestMethod]
        public void AppendLoggerTest()
        {
            string message = "Unit Test Logger";
            string appendMessage = "Appended to log";
            string filePath = "application.log";
            using (StreamWriter writer = File.CreateText(filePath))
            {
                writer.WriteLine(message);
            }

            Logger.log_message(appendMessage, nameof(LogLevels.Info), filePath);

            if (File.Exists(filePath))
            {
                using (StreamReader reader = File.OpenText(filePath))
                {

                    string text = reader.ReadLine();
                    if (text.Contains(message))
                    {
                        text = reader.ReadLine();
                        if (!text.Contains(appendMessage))
                        {
                            Assert.Fail();
                        }
                    }
                    else
                    {
                        Assert.Fail();
                    }

                }
            }
        }

        /// <summary>
        /// Test the load of a json file
        /// </summary>
        [TestMethod]
        public void JsonLoaderTest()
        {
            string CurrencyJson = @"..\..\..\CodeChallengeLuxoft\Settings\CurrencyBills.json";

            var json = JsonParser.LoadJsonFile(Path.GetFullPath(CurrencyJson));

            Assert.IsNotNull(json);
        }

        /// <summary>
        /// Test the method to find an attribute inside a JToken
        /// </summary>
        [TestMethod]
        public void JsonFinderTest()
        {
            string CurrencyJson = @"..\..\..\CodeChallengeLuxoft\Settings\CurrencyBills.json";

            var json = JsonParser.LoadJsonFile(Path.GetFullPath(CurrencyJson));
            Assert.IsNotNull(json);

            var found = JsonParser.FindTokens(json, "Country");
            Assert.IsNotNull(found);

        }

    }
}
