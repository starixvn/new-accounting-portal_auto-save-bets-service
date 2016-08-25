using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fanex.SaveBets.Builder.Header
{
    public class Header3ColumnImp : Header
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
