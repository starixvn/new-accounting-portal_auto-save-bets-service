using System.Collections.Generic;
using System.Data;
using Fanex.Common;
using Fanex.SaveBets.Builder.Helper;

namespace Fanex.SaveBets.Builder.Content
{
    public class ContentBetChoice606Imp : ContentBetChoice
    {
        private Dictionary<string, string> betteams = new Dictionary<string, string>()
        {
            { "h", "{h}" },
            { "a", "{a}" }
        };

        private Dictionary<int, string> betIds = new Dictionary<int, string>()
        {
            { 820, "1H Race to 20" },
            { 825, "1H Race to 25" },
            { 830, "1H Race to 30" },
            { 835, "1H Race to 35" },
        };

        public List<object> ContentBetChoiceBuilder(DataRow reader)
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