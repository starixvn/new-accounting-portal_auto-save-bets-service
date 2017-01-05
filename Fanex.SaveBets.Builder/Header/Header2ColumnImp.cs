using System.Collections.Generic;

namespace Fanex.SaveBets.Builder.Header
{
    public class Header2ColumnImp : Header
    {
        public List<string> BuildHeader()
        {
            return new List<string>()
            {
                "TEAM", "INTERVAL"
            };
        }
    }
}