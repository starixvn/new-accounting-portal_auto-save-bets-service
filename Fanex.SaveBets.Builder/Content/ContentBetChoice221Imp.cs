using System.Collections.Generic;
using System.Data;
using Fanex.Common;

namespace Fanex.SaveBets.Builder.Content
{
    public class ContentBetChoice221Imp : ContentBetChoice
    {
        public List<object> ContentBetChoiceBuilder(IDataReader reader)
        {
            var hdp1 = Converter.To<int>(reader["hdp1"]);
            var hdp2 = Converter.To<int>(reader["hdp2"]);
            var betteam = Converter.To<string>(reader["betteam"]);

            var betchoice = GetBetOptions(hdp2, betteam);
            var interval = GetSpecificIntervals(hdp1);

            return new List<object> { betchoice, interval };
        }

        private string GetBetOptions(int hdp2, string betteam)
        {
            var betOptions = new Dictionary<string, string>
            {
                { "2-2", "Goal" },
                { "4-4", "Corner" },
                { "8-8", "Free Kick" },
                { "16-16", "Goal Kick" },
                { "32-32", "Throw In" },
            };

            var key = $"{hdp2}-{betteam}";

            return betOptions.ContainsKey(key) ? betOptions[key] : string.Empty;
        }

        private string GetSpecificIntervals(int hdp1)
        {
            var interval = hdp1 < 10 ? $"0{hdp1}" : hdp1.ToString();

            return $"{interval}:00-{interval}:59";
        }
    }
}