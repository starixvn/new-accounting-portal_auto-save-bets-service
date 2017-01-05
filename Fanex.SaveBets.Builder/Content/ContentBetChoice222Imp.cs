using System.Collections.Generic;
using System.Data;
using Fanex.Common;

namespace Fanex.SaveBets.Builder.Content
{
    public class ContentBetChoice222Imp : ContentBetChoice
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
                { "2-2", "Goal-Yes" },
                { "2-0", "Goal-No" },
                { "4-4", "Corner-Yes" },
                { "4-0", "Corner-No" },
                { "128-128", "Penalty-Yes" },
                { "128-0", "Penalty-No" },
            };

            var key = $"{hdp2}-{betteam}";

            return betOptions.ContainsKey(key) ? betOptions[key] : string.Empty;
        }

        private string GetSpecificIntervals(int hdp1)
        {
            var minute1 = hdp1 < 10 ? $"0{hdp1}" : hdp1.ToString();
            var gap = hdp1 + 4;
            var minute2 = gap < 10 ? $"0{gap}" : gap.ToString();

            return $"{minute1}:00-{minute2}:59";
        }
    }
}