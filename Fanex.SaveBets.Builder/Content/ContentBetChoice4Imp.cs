using System.Collections.Generic;
using System.Data;
using Fanex.Common;

namespace Fanex.SaveBets.Builder.Content
{
    public class ContentBetChoice4Imp : ContentBetChoice
    {
        public List<object> ContentBetChoiceBuilder(DataRow reader)
        {
            var betteam = Converter.To<string>(reader["betteam"])
                .Replace("-over", "+")
                .Replace("-Over", "+")
                .Replace("&over", "+")
                .Replace("G6", "6+")
                .Replace("G3", "3+");
            var hdp1 = Converter.To<decimal>(reader["hdp1"]);
            var hdp2 = Converter.To<decimal>(reader["hdp2"]);

            return new List<object>()
            {
                betteam,
                hdp1,
                hdp2
            };
        }
    }
}