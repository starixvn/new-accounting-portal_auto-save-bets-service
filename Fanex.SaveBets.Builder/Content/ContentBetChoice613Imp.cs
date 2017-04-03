using Fanex.Common;
using Fanex.SaveBets.Builder.Helper;
using System.Collections.Generic;
using System.Data;

namespace Fanex.SaveBets.Builder.Content
{
    public class ContentBetChoice613Imp : ContentBetChoice
    {
        private Dictionary<string, string> betteams = new Dictionary<string, string>()
        {
            { "h", "{h}" },
            { "a", "{a}" }
        };

        private Dictionary<int, string> betIds = new Dictionary<int, string>()
        {
            { 80110, "Quarter 1 Race to 10" },
            { 80115, "Quarter 1 Race to 15" },
            { 80120, "Quarter 1 Race to 20" },
            { 80210, "Quarter 2 Race to 10" },
            { 80215, "Quarter 2 Race to 15" },
            { 80220, "Quarter 2 Race to 20" },
            { 80310, "Quarter 3 Race to 10" },
            { 80315, "Quarter 3 Race to 15" },
            { 80320, "Quarter 3 Race to 20" },
            { 80410, "Quarter 4 Race to 10" },
            { 80415, "Quarter 4 Race to 15" },
            { 80420, "Quarter 4 Race to 20" }
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
