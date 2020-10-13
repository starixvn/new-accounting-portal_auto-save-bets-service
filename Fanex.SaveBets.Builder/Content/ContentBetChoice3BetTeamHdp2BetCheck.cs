using Fanex.Common;
using System.Collections.Generic;
using System.Data;

namespace Fanex.SaveBets.Builder.Content
{
    public class ContentBetChoice3BetTeamHdp2BetCheck : ContentBetChoice
    {
        public List<object> ContentBetChoiceBuilder(DataRow reader)
        {
            var betteam = Converter.To<string>(reader["betteam"]);
            var hdp2 = Converter.To<int>(reader["hdp2"]);
            var betcheck = Converter.To<string>(reader["betcheck"]);
            return new List<object>()
            {
                betteam,
                hdp2,
                betcheck
            };
        }
    }
}