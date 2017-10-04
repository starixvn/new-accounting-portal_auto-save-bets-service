using System.Collections.Generic;
using System.Data;
using Fanex.Common;
using Fanex.SaveBets.Builder.Helper;

namespace Fanex.SaveBets.Builder.Content
{
    public class ContentBetChoice411Imp : ContentBetChoice
    {
        public List<object> ContentBetChoiceBuilder(IDataReader reader)
        {
            string betteam = Converter.To<string>(reader["betteam"]).ToLower();
            string homeId = Converter.To<string>(reader["homeId"]);
            string awayId = Converter.To<string>(reader["awayId"]);
            string teamName = string.Empty;
            if (betteam == "h")
            {
                teamName = CacheRef.GetTeamName(homeId);
            }
            else
            {
                teamName = CacheRef.GetTeamName(awayId);
            }

            return new List<object>()
            {
                teamName
            };
        }
    }
}