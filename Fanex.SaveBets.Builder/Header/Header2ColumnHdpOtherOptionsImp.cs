using System.Collections.Generic;

namespace Fanex.SaveBets.Builder.Header
{
    public class Header2ColumnHdpOtherOptionsImp : Header
    {
        public List<string> BuildHeader()
        {
            return new List<string>
            {
                "HDP",
                "OTHER OPTIONS"
            };
        }
    }
}