using System.Collections.Generic;
using System.Data;
using Fanex.Common;

namespace Fanex.SaveBets.Builder.Content
{
    public class ContentBetChoice2705_2706Imp : ContentBetChoice
    {
        public List<object> ContentBetChoiceBuilder(DataRow reader)
        {
            var betteam = Converter.To<string>(reader["betteam"]).ToLower();
            var hdp1 = Converter.To<decimal>(reader["hdp1"]);
            var hdp2 = Converter.To<decimal>(reader["hdp2"]);
            var hdp = 0m;
            if (betteam == "h")
            {
                hdp = hdp2 - hdp1;
            }
            else
            {
                hdp = hdp1 - hdp2;
            }

            return new List<object>
            {
                hdp
            };
        }
    }
}