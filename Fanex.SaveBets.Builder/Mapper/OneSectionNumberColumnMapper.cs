using System.Collections.Generic;

namespace Fanex.SaveBets.Builder.Mapper
{
    public interface OneSectionNumberColumnMapper
    {
        int GetNumberColumns(int bettype);
    }

    public class OneSectionNumberColumnMapperImp : OneSectionNumberColumnMapper
    {
        private Dictionary<int, int> oneSectionBettypeNumberColumns = new Dictionary<int, int>()
        {
            { 614, 21 }
        };

        public int GetNumberColumns(int bettype)
        {
            int numberColumn = 20;
            numberColumn = oneSectionBettypeNumberColumns.ContainsKey(bettype) ? oneSectionBettypeNumberColumns[bettype] : numberColumn;

            return numberColumn;
        }
    }
}