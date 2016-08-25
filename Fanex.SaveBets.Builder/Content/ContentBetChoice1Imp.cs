using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Fanex.Common;

namespace Fanex.SaveBets.Builder.Content
{
    public class ContentBetChoice1Imp : ContentBetChoice
    {
        public List<object> ContentBetChoiceBuilder(IDataReader reader)
        {
            var betteam = Converter.To<string>(reader["betteam"]).ToLower();
            var hdp1 = Converter.To<decimal>(reader["hdp1"]);
            var hdp2 = Converter.To<decimal>(reader["hdp2"]);
            decimal hdp = 0;
            if (betteam.Equals("h"))
            {
                hdp = hdp2 - hdp1;
            }
            else
            {
                hdp1 = hdp1 - hdp2;
            }

            return new List<object>()
            {
                hdp
            };
        }
    }
}
