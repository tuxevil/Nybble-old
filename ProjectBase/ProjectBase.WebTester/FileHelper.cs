using System;
using System.Data;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using FileHelpers;

namespace ProjectBase
{
    public class MoneyConverter : ConverterBase
    {
        private int mDecimals = 3;

        public override object StringToField(string from)
        {
            Regex decimalmatch = new Regex(@"^([1-9]\d{0,2}(\,\d{3})*|([0-9]\d*))(\.\d+)?$");
            Regex decimalmatch2 = new Regex(@"^([1-9]\d{0,2}(\.\d{3})*|([0-9]\d*))(\,\d+)?$");
            if (decimalmatch.IsMatch(from))
            {
                string value = from;
                value = value.Replace(",", "");
                value = value.Replace(".", ",");

                return Convert.ToDecimal(value);
            }
            if (decimalmatch2.IsMatch(from))
            {
                string value = from;
                value = value.Replace(".", "");
                return Convert.ToDecimal(value);
            }
            return 0;
        }


        public override string FieldToString(object fieldValue)
        {
            Decimal v = Convert.ToDecimal(fieldValue);

            // ugly but works =) 
            string res = Decimal.ToUInt32(Decimal.Truncate(v)).ToString();
            res += Decimal.Round(Decimal.Remainder(v, 1), mDecimals).ToString(".###").Replace(",", "").Replace(".", "").PadLeft(mDecimals, '0');

            return res;

            // a more elegant option that also works 
            // return Convert.ToInt32(Convert.ToDecimal(fieldValue) * (10 ^ mDecimals)).ToString();  
        }

    } 
}
