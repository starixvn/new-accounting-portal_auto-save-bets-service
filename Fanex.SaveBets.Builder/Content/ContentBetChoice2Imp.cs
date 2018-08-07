using System.Collections.Generic;
using System.Data;
using Fanex.Common;

namespace Fanex.SaveBets.Builder.Content
{
    public class ContentBetChoice2Imp : ContentBetChoice
    {
        public List<object> ContentBetChoiceBuilder(DataRow reader)
        {
            var hdp1 = Converter.To<decimal>(reader["hdp1"]);
            return new List<object>()
            {
                hdp1
            };
        }
    }
}