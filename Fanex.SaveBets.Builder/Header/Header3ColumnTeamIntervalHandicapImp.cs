using System.Collections.Generic;

namespace Fanex.SaveBets.Builder.Header
{
    public class Header3ColumnTeamIntervalHandicapImp : Header
    {
        public List<string> BuildHeader()
        {
            return new List<string>()
            {
                "TEAM",
                "INTERVAL",
                "HDP1"
            };
        }
    }
}
