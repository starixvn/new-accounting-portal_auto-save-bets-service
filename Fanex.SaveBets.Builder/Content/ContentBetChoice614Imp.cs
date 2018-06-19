using System.Collections.Generic;
using System.Data;
using Fanex.Common;

namespace Fanex.SaveBets.Builder.Content
{
    public class ContentBetChoice614Imp : ContentBetChoice
    {
        private Dictionary<int, string> betIds = new Dictionary<int, string>()
        {
            { 801, "Quarter 1 Winning Margin 7 Way" },
            { 802, "Quarter 2 Winning Margin 7 Way" },
            { 803, "Quarter 3 Winning Margin 7 Way" },
            { 804, "Quarter 4 Winning Margin 7 Way" }
        };

        public List<object> ContentBetChoiceBuilder(DataRow reader)
        {
            var betteamValue = Converter.To<string>(reader["betteam"]).ToLower();
            int betid = Converter.ToInt(reader["betid"]);
            string betTypeNameValue = betIds.ContainsKey(betid) ? betIds[betid] : string.Empty;

            var hdp1 = Converter.To<decimal>(reader["hdp1"]);
            var hdp2 = Converter.To<decimal>(reader["hdp2"]);

            return new List<object>()
            {
                betteamValue,
                betTypeNameValue,
                hdp1,
                hdp2
            };
        }
    }
}