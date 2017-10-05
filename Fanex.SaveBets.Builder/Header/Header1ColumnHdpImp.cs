using System.Collections.Generic;

namespace Fanex.SaveBets.Builder.Header
{
    public class Header1ColumnHdpImp : Header
    {
        public List<string> BuildHeader()
        {
            return new List<string>()
            {
                "HDP"
            };
        }
    }
}