using Fanex.Common;
using System.Collections.Generic;
using System.Data;

namespace Fanex.SaveBets.Builder.Content
{
    public class ContentBetChoice616Imp : ContentBetChoice
    {
        private Dictionary<string, string> betteams = new Dictionary<string, string>()
        {
            { "o", "Over {hdp1}" },
            { "u", "Under {hdp1}" }
        };

        private Dictionary<int, string> betIds = new Dictionary<int, string>()
        {
            { 801, "Quarter 1 Away  Team Over Under" },
            { 802, "Quarter 2 Away  Team Over Under" },
            { 803, "Quarter 3 Away  Team Over Under" },
            { 804, "Quarter 4 Away  Team Over Under" }
        };

        public List<object> ContentBetChoiceBuilder(IDataReader reader)
        {
            var betteamKey = Converter.To<string>(reader["betteam"]).ToLower();
            var betteamValue = betteams.ContainsKey(betteamKey) ? betteams[betteamKey] : string.Empty;
            int betid = Converter.ToInt(reader["betid"]);
            string betTypeNameValue = betIds.ContainsKey(betid) ? betIds[betid] : string.Empty;
            var hdp1 = Converter.To<decimal>(reader["hdp1"]);

            betteamValue = betteamValue.Replace("{hdp1}", $"{hdp1}:#,##0.00");
            return new List<object>()
            {
                betteamValue,
                betTypeNameValue
            };
        }
    }
}
