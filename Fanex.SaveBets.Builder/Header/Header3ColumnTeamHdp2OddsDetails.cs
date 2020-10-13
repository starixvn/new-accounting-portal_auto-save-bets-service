using System.Collections.Generic;

namespace Fanex.SaveBets.Builder.Header
{
    public class Header3ColumnTeamHdp2OddsDetails : Header
    {
        public List<string> BuildHeader()
        {
            return new List<string>()
            {
                "TEAM",
                "HDP2",
                "ODDS DETAILS"
            };
        }
    }
}