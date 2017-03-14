using System.Collections.Generic;

namespace Fanex.SaveBets.Builder.Header
{
    class Header2ColumnBetOptionBetTypeName : Header
    {
        public List<string> BuildHeader()
        {
            return new List<string>()
            {
                "BET OPTION",
                "BETYPE NAME"
            };
        }
    }
}
