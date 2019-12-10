using Fanex.Common;
using System.Collections.Generic;
using System.Data;

namespace Fanex.SaveBets.Builder.Content
{
    public class ContentBetChoice711Imp : ContentBetChoice
    {
        public List<object> ContentBetChoiceBuilder(DataRow reader)
        {
            var betteam = Converter.To<string>(reader["betteam"]).ToLower();
            var betId = Converter.To<int>(reader["betid"]);

            return new List<object>
            {
                betteam,
                betId
            };
        }
    }
}