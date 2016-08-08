using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Fanex.Common;

namespace Fanex.SaveBets.Builder.Content
{
    public class ContentBetChoice2Imp : ContentBetChoice
    {
        public List<object> ContentBetChoiceBuilder(IDataReader reader)
        {
            var hdp1 = Converter.To<decimal>(reader["hdp1"]);
            return new List<object>()
            {
                hdp1
            };
        }
    }
}
