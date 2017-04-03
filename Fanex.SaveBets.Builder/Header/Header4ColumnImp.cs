using System.Collections.Generic;

namespace Fanex.SaveBets.Builder.Header
{
    class Header4ColumnImp : Header
    {
        public List<string> BuildHeader()
        {
            return new List<string>()
            {
                "BET OPTION",
                "BETTYPE NAME",
                "HDP1",
                "HDP2"
            };
        }
    }
}
