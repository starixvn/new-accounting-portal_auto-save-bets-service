using System.Collections.Generic;

namespace Fanex.SaveBets.Builder.Header
{
    public class Header2ColumnTeamOtherOptionsImp : Header
    {
        public List<string> BuildHeader()
        {
            return new List<string>
            {
                "TEAM",
                "OTHER OPTIONS"
            };
        }
    }
}