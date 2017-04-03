using Fanex.Common;
using Fanex.SaveBets.Builder.Helper;
using System.Collections.Generic;
using System.Data;

namespace Fanex.SaveBets.Builder.Content
{
    public class ContentBetChoice612Imp : ContentBetChoice
    {
        private Dictionary<string, string> betteams = new Dictionary<string, string>()
        {
            { "h", "{h}" },
            { "a", "{a}" }
        };

        private Dictionary<int, string> betIds = new Dictionary<int, string>()
        {
            { 801, "Quarter 1 Moneyline" },
            { 802, "Quarter 2 Moneyline" },
            { 803, "Quarter 3 Moneyline" },
            { 804, "Quarter 4 Moneyline" }
        };

        public List<object> ContentBetChoiceBuilder(IDataReader reader)
        {
            var betteamKey = Converter.To<string>(reader["betteam"]).ToLower();
            var betteamValue = betteams.ContainsKey(betteamKey) ? betteams[betteamKey] : string.Empty;
            string homeId = Converter.To<string>(reader["homeid"]);
            string awayId = Converter.To<string>(reader["awayId"]);
            int betid = Converter.ToInt(reader["betid"]);
            string betTypeNameValue = betIds.ContainsKey(betid) ? betIds[betid] : string.Empty;

            betteamValue = betteamValue
             .Replace("{h}", CacheRef.GetTeamName(homeId))
             .Replace("{a}", CacheRef.GetTeamName(awayId));

            return new List<object>()
            {
                betteamValue,
                betTypeNameValue
            };
        }
    }
}
