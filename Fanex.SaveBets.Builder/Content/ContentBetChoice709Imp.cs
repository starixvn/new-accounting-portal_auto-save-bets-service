using Fanex.Common;
using System.Collections.Generic;
using System.Data;

namespace Fanex.SaveBets.Builder.Content
{
    public class ContentBetChoice709Imp : ContentBetChoice
    {
        public List<object> ContentBetChoiceBuilder(DataRow reader)
        {
            var hdp1 = Converter.To<decimal>(reader["hdp1"]);
            var betId = Converter.To<int>(reader["betid"]);
            return new List<object>()
            {
                hdp1,
                betId
            };
        }
    }
}