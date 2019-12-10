using Fanex.Common;
using System.Collections.Generic;
using System.Data;

namespace Fanex.SaveBets.Builder.Content
{
    public class ContentBetChoice707Imp : ContentBetChoice
    {
        public List<object> ContentBetChoiceBuilder(DataRow reader)
        {
            var betteam = Converter.To<string>(reader["betteam"]).ToLower();
            var betId = Converter.To<int>(reader["betid"]);
            var hdp1 = Converter.To<decimal>(reader["hdp1"]);
            var hdp2 = Converter.To<decimal>(reader["hdp2"]);
            var dic = new Dictionary<string, decimal>
            {
                { "h", hdp2 - hdp1 },
                { "a", hdp1 - hdp2 },
            };

            var hdp = dic.ContainsKey(betteam) ? dic[betteam] : 0m;

            return new List<object>
            {
                hdp,
                betId
            };
        }
    }
}