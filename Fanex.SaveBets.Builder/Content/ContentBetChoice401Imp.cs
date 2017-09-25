using System;
using System.Collections.Generic;
using System.Data;
using Fanex.Common;

namespace Fanex.SaveBets.Builder.Content
{
    public class ContentBetChoice401Imp : ContentBetChoice
    {
        private readonly Dictionary<string, string> betteams = new Dictionary<string, string>()
        {
            { "o", "Over" },
            { "u", "Under" }
        };

        public List<object> ContentBetChoiceBuilder(IDataReader reader)
        {
            var betteamKey = Converter.To<string>(reader["betteam"]).ToLower();
            var betteamValue = betteams.ContainsKey(betteamKey) ? betteams[betteamKey] : string.Empty;
            var hdp1 = Converter.ToDecimal(reader["hdp1"]);
            hdp1 = Math.Round(hdp1, 2);

            return new List<object>()
            {
                betteamValue + " " + hdp1
            };
        }
    }
}