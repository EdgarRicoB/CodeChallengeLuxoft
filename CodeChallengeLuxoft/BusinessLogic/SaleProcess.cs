using CodeChallengeLuxoft.Enums;
using CodeChallengeLuxoft.Models;
using CodeChallengeLuxoft.Settings;
using CodeChallengeLuxoft.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeChallengeLuxoft.BusinessLogic
{
    public class SaleProcess
    {
        /// <summary>
        /// Method to get a list of the different bills and the quantity for each
        /// </summary>
        /// <param name="money"></param>
        /// <returns></returns>
        public static List<Currency> GetPayment(string money)
        {
            var list = new List<Currency>();
            var moneyList = money.Split(',');
            foreach (var item in moneyList)
            {
                var values = item.Split('$');
                if (values.Length < 2 || !decimal.TryParse(values[1], out decimal value) || !int.TryParse(values[0], out int quantity))
                {
                    string message = $"The input given is not formatted correctly for the following: {item}";
                    Logger.log_message(message, nameof(LogLevels.Error));
                    throw new Exception(message);
                }
                if (!Configurations.AvailableCurrency.Any(x => x.Value == value))
                {
                    string message = $"The bill or coing given is not accepted for this country: {value}";
                    Logger.log_message(message, nameof(LogLevels.Error));
                    throw new Exception(message);
                }

                list.Add(new Currency() { Value = value, Quantity = quantity });
            }

            return list;
        }

        /// <summary>
        /// Validates that the total is in a valid currency and is a valid number
        /// </summary>
        /// <param name="total"></param>
        /// <returns></returns>
        public static decimal ValidateTotal(string total)
        {
            if (!decimal.TryParse(total, out decimal value))
            {
                string message = $"The input total given is not a valid number: {value}";
                Logger.log_message(message, nameof(LogLevels.Error));
                throw new Exception(message);
            }
            if (value % Configurations.AvailableCurrency.Select(x => x.Value).Min() != 0)
            {
                string message = $"The input total given contains bills that are not accepted in this country, please verify: {value}";
                Logger.log_message(message, nameof(LogLevels.Error));
                throw new Exception(message);
            }

            return value;
        }

        /// <summary>
        /// Validates that the payment amounts are equal or greater than the total
        /// </summary>
        /// <param name="total"></param>
        /// <param name="payment"></param>
        public static void ValidateAmounts(decimal total, List<Currency> payment)
        {
            ValidatePaymentsList(payment);

            if (payment.Sum(x => x.Value * x.Quantity) < total)
            {
                string message = $"The amount given by the customer is less than the total";
                Logger.log_message(message, nameof(LogLevels.Error));
                throw new Exception(message);
            }
        }


        /// <summary>
        /// Calcualtes the optimal change to return
        /// </summary>
        /// <param name="total"></param>
        /// <param name="payment"></param>
        /// <returns></returns>
        public static List<Currency> GetChange(decimal total, List<Currency> payment)
        {
            ValidatePaymentsList(payment);
            var list = new List<Currency>();
            decimal totalChange = payment.Sum(x => x.Value * x.Quantity) - total;
            while (totalChange != 0)
            {
                var billValue = Configurations.AvailableCurrency.Where(y => y.Value <= totalChange)?.Select(x => x.Value).Max();
                if (!billValue.HasValue)
                {
                    string message = $"There are no bills to give change for the amount given";
                    Logger.log_message(message, nameof(LogLevels.Exception));
                    throw new Exception(message);
                }

                var times = totalChange / billValue;
                if (!times.HasValue)
                {
                    string message = $"The division did not returned results";
                    Logger.log_message(message, nameof(LogLevels.Exception));
                    throw new Exception(message);
                }

                int quantity = decimal.ToInt32(times.Value);

                list.Add(new Currency() { Value = billValue.Value, Quantity = quantity });

                var leftChange = totalChange % billValue;
                if (!leftChange.HasValue)
                {
                    string message = $"The division did not returned results";
                    Logger.log_message(message, nameof(LogLevels.Exception));
                    throw new Exception(message);
                }
                totalChange = leftChange.Value;
            }

            return list;
        }

        /// <summary>
        /// Validates that the payments list is not null or empty
        /// </summary>
        /// <param name="payment"></param>
        private static void ValidatePaymentsList(List<Currency> payment)
        {
            if (payment == null || !payment.Any())
            {
                string message = $"The payments list is empty";
                Logger.log_message(message, nameof(LogLevels.Exception));
                throw new Exception(message);
            }
        }

    }
}
