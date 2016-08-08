using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RemotingRef;

namespace Fanex.SaveBets.Builder.Helper
{
    public class CacheRef
    {
        public static string GetTeamName(string teamId)
        {
            return RefData.GetTeamName(teamId, "en-US");
        }
    }
}
