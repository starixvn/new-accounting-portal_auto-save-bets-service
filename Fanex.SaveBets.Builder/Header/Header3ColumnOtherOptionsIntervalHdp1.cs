using System.Collections.Generic;

namespace Fanex.SaveBets.Builder.Header
{
    public class Header3ColumnOtherOptionsIntervalHdp1 : Header
    {
        public List<string> BuildHeader()
        {
            return new List<string>()
            {
                "OTHER OPTIONS",
                "INTERVAL",
                "HDP1"
            };
        }
    }
}