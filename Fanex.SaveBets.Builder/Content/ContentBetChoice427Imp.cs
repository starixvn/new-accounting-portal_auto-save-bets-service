using System.Collections.Generic;
using System.Data;
using Fanex.Common;

namespace Fanex.SaveBets.Builder.Content
{
    public class ContentBetChoice427Imp : ContentBetChoice
    {
        private Dictionary<string, string> betteams = new Dictionary<string, string>()
        {
            { "y", "Yes" },
            { "n", "No" },
            { "o", "Odd" },
            { "e", "Even" },
        };

        public List<object> ContentBetChoiceBuilder(DataRow reader)
        {
            var betteamKey = Converter.To<string>(reader["betteam"]).ToLower();
            var betteamValue = betteams.ContainsKey(betteamKey) ? betteams[betteamKey] : string.Empty;

            return new List<object>()
            {
                betteamValue
            };
        }
    }
}