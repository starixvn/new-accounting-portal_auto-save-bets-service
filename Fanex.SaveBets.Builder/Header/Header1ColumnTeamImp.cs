using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fanex.SaveBets.Builder.Header
{
    public class Header1ColumnTeamImp : Header
    {
        public List<string> BuildHeader()
        {
            return new List<string>()
            {
                "TEAM"
            };
        }
    }
}
