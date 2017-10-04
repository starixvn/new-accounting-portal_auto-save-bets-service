using System.Collections.Generic;

namespace Fanex.SaveBets.Builder.Header
{
    public interface HeaderBuilder
    {
        List<string> GetHeaderHandicapAndBetteamOf(int bettype);
    }
}