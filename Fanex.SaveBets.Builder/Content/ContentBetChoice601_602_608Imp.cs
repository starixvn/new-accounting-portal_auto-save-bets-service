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
            { "h1-2", "{h} to Win by 1 to 2 points" },
            { "h3-6", "{h} to Win by 3 to 6 points" },
            { "h7-9", "{h} to Win by 7 to 9 points" },
            { "h10-13", "{h} to Win by 10 to 13 points" },
            { "h14-16", "{h} to Win by 14 to 16 points" },
            { "h17-20", "{h} to Win by 17 to 20 points" },
            { "h21+", "{h} to Win by 21+ points" },
            { "a1-2", "{a} to Win by 1 to 2 points" },
            { "a3-6", "{a} to Win by 3 to 6 points" },
            { "a7-9", "{a} to Win by 7 to 9 points" },
            { "a10-13", "{a} to Win by 10 to 13 points" },
            { "a14-16", "{a} to Win by 14 to 16 points" },
            { "a17-20", "{a} to Win by 17 to 20 points" },
            { "a21+", "{a} to Win by 21+ points" },
            { "h1-5", "{h} to Win by 1 to 5 points" },
            { "h6-10", "{h} to Win by 6 to 10 points" },
            { "h11-15", "{h} to Win by 11 to 15 points" },
            { "h16-20", "{h} to Win by 16 to 20 points" },
            { "h21-25", "{h} to Win by 21 to 25 points" },
            { "h26+", "{h} to Win by 26+ points" },
            { "a1-5", "{a} to Win by 1 to 5 points" },
            { "a6-10", "{a} to Win by 6 to 10 points" },
            { "a11-15", "{a} to Win by 11 to 15 points" },
            { "a16-20", "{a} to Win by 16 to 20 points" },
            { "a21-25", "{a} to Win by 21 to 25 points" },
            { "a26+", "{a} to Win by 26+ points" },
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
