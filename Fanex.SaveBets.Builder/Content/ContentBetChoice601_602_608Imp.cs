using Fanex.Common;
using Fanex.SaveBets.Builder.Helper;
using System.Collections.Generic;
using System.Data;

namespace Fanex.SaveBets.Builder.Content
{
    public class ContentBetChoice601_602_608Imp : ContentBetChoice
    {
        private Dictionary<string, string> betteams = new Dictionary<string, string>()
        {
            { "h1-2", "{h} to Win by 1 - 2 Points" },
            { "h3-6", "{h} to Win by 3 - 6 Points" },
            { "h7-9", "{h} to Win by 7 - 9 Points" },
            { "h10-13", "{h} to Win by 10 - 13 Points" },
            { "h14-16", "{h} to Win by 14 - 16 Points" },
            { "h17-20", "{h} to Win by 17 - 20 Points" },
            { "h21+", "{h} to Win by 21+ Points" },
            { "a1-2", "{a} to Win by 1 - 2 Points" },
            { "a3-6", "{a} to Win by 3 - 6 Points" },
            { "a7-9", "{a} to Win by 7 - 9 Points" },
            { "a10-13", "{a} to Win by 10 - 13 Points" },
            { "a14-16", "{a} to Win by 14 - 16 Points" },
            { "a17-20", "{a} to Win by 17 - 20 Points" },
            { "a21+", "{a} to Win by 21+ Points" },
            { "h1-5", "{h} to Win by 1 - 5 Points" },
            { "h6-10", "{h} to Win by 6 - 10 Points" },
            { "h11-15", "{h} to Win by 11 - 15 Points" },
            { "h16-20", "{h} to Win by 16 - 20 Points" },
            { "h21-25", "{h} to Win by 21 - 25 Points" },
            { "h26+", "{h} to Win by 26+ Points" },
            { "a1-5", "{a} to Win by 1 - 5 Points" },
            { "a6-10", "{a} to Win by 6 - 10 Points" },
            { "a11-15", "{a} to Win by 11 - 15 Points" },
            { "a16-20", "{a} to Win by 16 - 20 Points" },
            { "a21-25", "{a} to Win by 21 - 25 Points" },
            { "a26+", "{a} to Win by 26+ Points" },
            { "d", "Draw" }
        };

        public List<object> ContentBetChoiceBuilder(IDataReader reader)
        {
            var betteamKey = Converter.To<string>(reader["betteam"]).ToLower();
            var betteamValue = betteams.ContainsKey(betteamKey) ? betteams[betteamKey] : string.Empty;
            string homeId = Converter.To<string>(reader["homeid"]);
            string awayId = Converter.To<string>(reader["awayId"]);

            betteamValue = betteamValue
                .Replace("{h}", CacheRef.GetTeamName(homeId))
                .Replace("{a}", CacheRef.GetTeamName(awayId));

            var hdp1 = Converter.To<decimal>(reader["hdp1"]);
            var hdp2 = Converter.To<decimal>(reader["hdp2"]);

            return new List<object>()
            {
                betteamValue,
                hdp1,
                hdp2
            };
        }
    }
}
