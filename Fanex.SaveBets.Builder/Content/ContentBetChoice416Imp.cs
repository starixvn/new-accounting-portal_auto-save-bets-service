using System.Collections.Generic;
using System.Data;
using Fanex.Common;

namespace Fanex.SaveBets.Builder.Content
{
    public class ContentBetChoice416Imp : ContentBetChoice
    {
        public List<object> ContentBetChoiceBuilder(IDataReader reader)
        {
            var betteam = Converter.To<string>(reader["betteam"]);

            var hdp1 = Converter.To<decimal>(reader["hdp1"]);
            var hdp2 = Converter.To<decimal>(reader["hdp2"]);

            return new List<object>()
            {
                betteam,
                hdp1,
                hdp2
            };
        }
    }
}