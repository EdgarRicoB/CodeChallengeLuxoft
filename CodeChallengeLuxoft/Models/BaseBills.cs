using CodeChallengeLuxoft.Enums;
using CodeChallengeLuxoft.Utilities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeChallengeLuxoft.Models
{
    public abstract class BaseBills
    {
        public const string CurrencyJson = @"..\..\..\CodeChallengeLuxoft\Settings\CurrencyBills.json";
        /// <summary>
        /// Field that contains the list of bills accepted
        /// </summary>
        public List<Currency> CurrencyList = new List<Currency>();

        /// <summary>
        /// Class constructor
        /// </summary>
        public BaseBills(string identifier)
        {
            //Load json with 
            var json = JsonParser.LoadJsonFile(Path.GetFullPath(CurrencyJson));
            if (json == null)
            {
                string message = "Could not load json with configurations for currencies";
                Logger.log_message(message, nameof(LogLevels.Error));
                throw new Exception(message);
            }

            //Get the attribute that matches the country
            var country = json.FindTokens("Country")?.FirstOrDefault(x => x.Value<string>() == identifier);
            //Get the bills
            var bills = country?.Parent?.Parent["Bills"]?.Value<string>().Split(',');
            if (bills == null)
            {
                string message = "No bills were found in the configuration";
                Logger.log_message(message, nameof(LogLevels.Error));
                throw new Exception(message);
            }

            //Load the bills to the list
            foreach (var bill in bills)
            {
                if (!decimal.TryParse(bill, out decimal result))
                {
                    string message = "One of the bills in the configuration is not a valid value";
                    Logger.log_message(message, nameof(LogLevels.Error));
                }

                CurrencyList.Add(new Currency() { Value = result });
            }

        }

        /// <summary>
        /// Obtains the country of the current currency
        /// </summary>
        /// <returns>returns the current country</returns>
        public abstract string GetCountry();
    }
}
