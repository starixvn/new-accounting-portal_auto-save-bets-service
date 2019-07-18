using System.Collections.Generic;

namespace Fanex.SaveBets.Builder.Header
{
    public class Header1ColumnScoreBoxOddsImp : Header
    {
        public List<string> BuildHeader()
        {
            return new List<string>
            {
                "SCORE BOX ODDS"
            };
        }
    }
}