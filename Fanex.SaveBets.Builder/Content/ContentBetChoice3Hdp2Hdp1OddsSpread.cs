using Fanex.Common;
using System.Collections.Generic;
using System.Data;

namespace Fanex.SaveBets.Builder.Content
{
    public class ContentBetChoice3Hdp2Hdp1OddsSpread : ContentBetChoice
    {
        public List<object> ContentBetChoiceBuilder(DataRow reader)
        {
            var hdp2 = Converter.To<int>(reader["hdp2"]);
            var hdp1 = Converter.To<int>(reader["hdp1"]);
            var oddsspread = Converter.To<decimal>(reader["oddsspread"]);
            return new List<object>()
            {
                hdp2,
                hdp1,
                oddsspread
            };
        }
    }
}