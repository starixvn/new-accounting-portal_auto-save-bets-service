using System;
using System.Collections.Generic;
using System.Data;
using Fanex.Common;
using Fanex.SaveBets.Builder.Helper;

namespace Fanex.SaveBets.Builder.Content
{
    public class ContentBetChoice417Imp : ContentBetChoice
    {
        private Dictionary<string, string> betteams = new Dictionary<string, string>()
        {
            ////417
            { "yh", "Yes/{h}" },
            { "yd", "Yes/Draw" },
            { "ya", "Yes/{a}" },
            { "nh", "No/{h}" },
            { "nd", "No/Draw" },
            { "na", "No/{a}" },

            ////418
            { "yo", "Yes & Over {hdp1}" },
            { "yu", "Yes & Under {hdp1}" },
            { "no", "No & Over {hdp1}" },
            { "nu", "No & Over {hdp1}" },

            ////419, 420, 421
            { "1h", "1st Half" },
            { "2h", "2nd Half" },
            { "n", "Neither" },

            ////422, 423
            { "h", "Home" },
            { "a", "Away" },
        };

        public List<object> ContentBetChoiceBuilder(IDataReader reader)
        {
            var betteamKey = Converter.To<string>(reader["betteam"]).ToLower();
            var betteamValue = betteams.ContainsKey(betteamKey) ? betteams[betteamKey] : string.Empty;
            var homeId = Converter.To<string>(reader["homeId"]);
            var awayId = Converter.To<string>(reader["awayId"]);
            var hdp1 = Converter.To<decimal>(reader["hdp1"]);
            var hdp2 = Converter.To<decimal>(reader["hdp2"]);

            betteamValue = betteamValue
                .Replace("{h}", CacheRef.GetTeamName(homeId))
                .Replace("{a}", CacheRef.GetTeamName(awayId))
                .Replace("{hdp1}", Math.Round(hdp1, 2).ToString());

            return new List<object>()
            {
                betteamValue,
                hdp1,
                hdp2
            };
        }
    }
}