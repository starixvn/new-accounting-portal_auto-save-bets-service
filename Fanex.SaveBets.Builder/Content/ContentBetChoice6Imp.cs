using System.Collections.Generic;
using System.Data;
using Fanex.Common;

namespace Fanex.SaveBets.Builder.Content
{
    public class ContentBetChoice6Imp : ContentBetChoice
    {
        public List<object> ContentBetChoiceBuilder(IDataReader reader)
        {
            var betId = Converter.To<int>(reader["betid"]);
            return new List<object>()
            {
                betId
            };
        }
    }
}
