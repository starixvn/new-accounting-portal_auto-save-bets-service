using System.Collections.Generic;

namespace Fanex.SaveBets.Builder.Header
{
    public class Header1ColumnPointImp : Header
    {
        public List<string> BuildHeader()
        {
            return new List<string>()
            {
                "POINTS"
            };
        }
    }
}
