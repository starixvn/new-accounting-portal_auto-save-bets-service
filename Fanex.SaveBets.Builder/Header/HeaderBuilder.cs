using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fanex.SaveBets.Builder.Header
{
    public interface HeaderBuilder
    {
        List<string> GetHeaderHandicapAndBetteamOf(int bettype);
    }
}
