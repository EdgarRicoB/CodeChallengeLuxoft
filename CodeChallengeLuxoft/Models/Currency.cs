using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeChallengeLuxoft.Models
{
    public class Currency
    {
        /// <summary>
        /// Property that stores the value of the currency
        /// </summary>
        public decimal Value { get; set; }

        /// <summary>
        /// Property that store the quantity of the currency
        /// </summary>
        public int Quantity { get; set; } = 0;
    }
}
