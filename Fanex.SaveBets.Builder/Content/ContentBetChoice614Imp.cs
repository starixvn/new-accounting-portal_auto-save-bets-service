using Fanex.Common;
using Fanex.SaveBets.Builder.Helper;
using System.Collections.Generic;
using System.Data;

namespace Fanex.SaveBets.Builder.Content
{
    public class ContentBetChoice614Imp : ContentBetChoice
    {
        private Dictionary<string, string> betteams = new Dictionary<string, string>()
        {
            { "h1-4", "{h} to Win by 1 - 4 Points" },
            { "h5-8", "{h} to Win by 5 - 8 Points" },
            { "h9+", "{h} to Win by 9+ Points" },
            { "a1-4", "{a} to Win by 1 - 4 Points" },
            { "a5-8", "{a} to Win by 5 - 8 Points" },
            { "a9+", "{a} to Win by 9+ Points" },
            { "d", "Draw" }
        };

        private Dictionary<int, string> betIds = new Dictionary<int, string>()
        {
            { 801, "Quarter 1 Winning Margin 7 Way" },
            { 802, "Quarter 2 Winning Margin 7 Way" },
            { 803, "Quarter 3 Winning Margin 7 Way" },
            { 804, "Quarter 4 Winning Margin 7 Way" }
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
