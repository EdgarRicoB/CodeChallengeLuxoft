using CodeChallengeLuxoft.Enums;
using CodeChallengeLuxoft.Models;
using CodeChallengeLuxoft.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeChallengeLuxoft.Settings
{
    public class Configurations
    {
        /// <summary>
        /// Currency model
        /// </summary>
        private static BaseBills model = null;
        
        /// <summary>
        /// Field for the currency Property
        /// </summary>
        private static string currency = string.Empty;

        /// <summary>
        /// Field for the LogName Property
        /// </summary>
        private static string logName = string.Empty;

        /// <summary>
        /// Field for the Country Property
        /// </summary>
        private static string country = string.Empty;

        /// <summary>
        /// Field for the AvailableCurrency Property
        /// </summary>
        private static List<Currency> availableCurrency = null;

        /// <summary>
        /// Configuration that gets the country the POS is configured to
        /// </summary>
        public static string Currency 
        {
            get 
            {
                if (string.IsNullOrEmpty(currency))
                {
                    return currency = ConfigurationManager.AppSettings["Currency"]; 
                }

                return currency;
            }
        }

        /// <summary>
        /// Configuration that gets the file path of the log
        /// </summary>
        public static string LogName
        {
            get
            {
                if (string.IsNullOrEmpty(logName))
                {
                    return logName = ConfigurationManager.AppSettings["LogName"]; 
                }

                return logName;
            }
        }

        /// <summary>
        /// Configuration that returns the available currency list for the configured country
        /// </summary>
        public static List<Currency> AvailableCurrency
        {
            get
            {
                if (availableCurrency == null)
                {
                    if (model == null)
                    {
                        model = GetModel(); 
                    }

                    availableCurrency = model.CurrencyList;
                }

                return availableCurrency;

            }
        }

        /// <summary>
        /// Configuration that returns the available currency list for the configured country
        /// </summary>
        public static string Country
        {
            get
            {
                if (string.IsNullOrEmpty(country))
                {
                    if (model == null)
                    {
                        model = GetModel();
                    }

                    country = model.GetCountry();
                }

                return country;

            }
        }

        /// <summary>
        /// Gets the model of the configured currency
        /// </summary>
        /// <returns></returns>
        private static BaseBills GetModel()
        {
            BaseBills model;
            switch (Currency)
            {
                case nameof(CurrencyIdentifier.MXN):
                    model = new MexicoModel();
                    break;
                case nameof(CurrencyIdentifier.US):
                    model = new UnitedStatesModel();
                    break;
                default:
                    string message = "The configuration for the Currency contains an invalid Currency";
                    Logger.log_message(message, nameof(LogLevels.Error));
                    throw new Exception(message);
            }

            return model;
        }
    }
}
