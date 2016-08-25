using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fanex.SaveBets.Builder.Header
{
    public class Header1ColumnMyanmarOddBettype : Header
    {
        public List<string> BuildHeader()
        {
            return new List<string>()
            {
                "HDP(% ODDS)"
            };
        }
    }
}
