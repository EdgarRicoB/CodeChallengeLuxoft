using CodeChallengeLuxoft.Enums;
using CodeChallengeLuxoft.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeChallengeLuxoft.Models
{
    public class MexicoModel : BaseBills
    {
        /// <summary>
        /// Class constructor
        /// </summary>
        public MexicoModel() : base(CurrencyIdentifier.MXN.ToString())
        {
        }

        /// <summary>
        /// Obtains the country of the currency
        /// </summary>
        /// <returns>returns the country</returns>
        public override string GetCountry()
        {
            return "Mexico";
        }
    }
}
