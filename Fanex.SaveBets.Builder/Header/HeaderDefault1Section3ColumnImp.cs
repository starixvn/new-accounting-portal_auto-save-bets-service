using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fanex.SaveBets.Builder.Header
{
    public class HeaderDefault1Section3ColumnImp : Header
    {
        public List<string> BuildHeader()
        {
            return new List<string>()
            {
                "TEAM",
                "HDP1",
                "HDP2"
            };
        }
    }
}
