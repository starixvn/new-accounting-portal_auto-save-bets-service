using System.Collections.Generic;
using System.Data;
using Fanex.Common;

namespace Fanex.SaveBets.Builder.Content
{
    public class ContentBetChoice425Imp : ContentBetChoice
    {
        private Dictionary<string, string> betteams = new Dictionary<string, string>()
        {
            { "h", "Home" },
            { "a", "Away" },
        };

        public List<object> ContentBetChoiceBuilder(IDataReader reader)
        {
            var betteam = Converter.To<string>(reader["betteam"]).ToLower();
            var beteamValue = betteams.ContainsKey(betteam) ? betteams[betteam] : string.Empty;
            return new List<object>()
            {
                beteamValue
            };
        }
    }
}