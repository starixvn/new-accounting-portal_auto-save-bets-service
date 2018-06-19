using System.Collections.Generic;
using System.Data;
using Fanex.Common;

namespace Fanex.SaveBets.Builder.Content
{
    public class ContentBetChoice611Imp : ContentBetChoice
    {
        private Dictionary<string, string> betteams = new Dictionary<string, string>()
        {
            { "o", "Odd" },
            { "e", "Even" }
        };

        private Dictionary<int, string> betIds = new Dictionary<int, string>()
        {
            { 801, "Quarter 1 Odd Even" },
            { 802, "Quarter 2 Odd Even" },
            { 803, "Quarter 3 Odd Even" },
            { 804, "Quarter 4 Odd Even" }
        };

        public List<object> ContentBetChoiceBuilder(DataRow reader)
        {
            var betteamKey = Converter.To<string>(reader["betteam"]).ToLower();
            var betteamValue = betteams.ContainsKey(betteamKey) ? betteams[betteamKey] : string.Empty;
            int betid = Converter.ToInt(reader["betid"]);
            string betTypeNameValue = betIds.ContainsKey(betid) ? betIds[betid] : string.Empty;

            return new List<object>()
            {
                betteamValue,
                betTypeNameValue
            };
        }
    }
}