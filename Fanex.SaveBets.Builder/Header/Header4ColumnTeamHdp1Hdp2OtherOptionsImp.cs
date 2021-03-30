using System.Collections.Generic;

namespace Fanex.SaveBets.Builder.Header
{
    internal class Header4ColumnTeamHdp1Hdp2OtherOptionsImp : Header
    {
        public List<string> BuildHeader()
        {
            return new List<string>()
            {
                "TEAM",
                "HDP1",
                "HDP2",
                "OTHER OPTIONS",
            };
        }
    }
}