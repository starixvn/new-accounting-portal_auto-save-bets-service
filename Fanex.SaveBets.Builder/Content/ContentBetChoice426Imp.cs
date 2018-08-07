using System.Collections.Generic;
using System.Data;
using Fanex.Common;
using Fanex.SaveBets.Builder.Helper;

namespace Fanex.SaveBets.Builder.Content
{
    public class ContentBetChoice426Imp : ContentBetChoice
    {
        private Dictionary<string, string> betteams = new Dictionary<string, string>()
        {
            ////426
            { "h1", "{h} to win by 1 Goal" },
            { "h2+", "{h} to win by 2+ Goals" },
            { "d", "Any Score Draw (Exclude 0-0)" },
            { "a1", "{a} to win by 1 Goal" },
            { "a2+", "{a}  to win by 2+ Goals" },
            { "ng", "No Goal" },

            ////429
            { "0", "No Goal" },
            { "1", "1 Goal" },
            { "2", "2 Goals" },
            { "3-over", "3+" },

            ////445, 446, 447
            { "yy", "Yes/Yes" },
            { "yn", "Yes/No" },
            { "ny", "No/Yes" },
            { "nn", "No/No" },
        };

        public List<object> ContentBetChoiceBuilder(DataRow reader)
        {
            var betteamKey = Converter.To<string>(reader["betteam"]).ToLower();
            var betteamValue = betteams.ContainsKey(betteamKey) ? betteams[betteamKey] : string.Empty;
            var homeId = Converter.To<string>(reader["homeId"]);
            var awayId = Converter.To<string>(reader["awayId"]);
            var hdp1 = Converter.To<decimal>(reader["hdp1"]);
            var hdp2 = Converter.To<decimal>(reader["hdp2"]);

            betteamValue = betteamValue
                .Replace("{h}", CacheRef.GetTeamName(homeId))
                .Replace("{a}", CacheRef.GetTeamName(awayId));

            return new List<object>()
            {
                betteamValue,
                hdp1,
                hdp2
            };
        }
    }
}