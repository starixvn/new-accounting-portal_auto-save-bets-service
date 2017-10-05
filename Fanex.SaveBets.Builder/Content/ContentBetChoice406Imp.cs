using System.Collections.Generic;
using System.Data;
using Fanex.Common;

namespace Fanex.SaveBets.Builder.Content
{
    public class ContentBetChoice406Imp : ContentBetChoice
    {
        private readonly Dictionary<string, string> betteams = new Dictionary<string, string>()
        {
            { "g0", "0 Goals" },
            { "g1", "1 Goal" },
            { "g2", "2 Goals" },
            { "g3", "3 Goals" },
            { "g4", "4 Goals" },
            { "g5", "5 Goals" },
            { "g6", "6+" }
        };

        public List<object> ContentBetChoiceBuilder(IDataReader reader)
        {
            var betteamKey = Converter.To<string>(reader["betteam"]).ToLower();
            var betteamValue = betteams.ContainsKey(betteamKey) ? betteams[betteamKey] : string.Empty;
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