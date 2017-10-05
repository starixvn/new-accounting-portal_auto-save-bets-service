using System.Collections.Generic;

namespace Fanex.SaveBets.Builder.Header
{
    public class Header3ColumnBetOptionHandicapImp : Header
    {
        public List<string> BuildHeader()
        {
            return new List<string>()
            {
                "BET OPTION",
                "HDP1",
                "HDP2"
            };
        }
    }
}