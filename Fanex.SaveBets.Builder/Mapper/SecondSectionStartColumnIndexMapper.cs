using System.Collections.Generic;

namespace Fanex.SaveBets.Builder.Mapper
{
    public interface SecondSectionStartColumnIndexMapper
    {
        int GetStartColumnIndexOfSecondSection(int bettype);
    }

    public class SecondSectionStartColumnIndexMapperImp : SecondSectionStartColumnIndexMapper
    {
        private readonly Dictionary<int, int> groupBettypeStartColumnIndex = new Dictionary<int, int>()
        {
            { 606, 13 },
            { 607, 13 },
            { 609, 13 },
            { 610, 13 },
            { 611, 13 },
            { 612, 13 },
            { 613, 13 },
            { 615, 13 },
            { 616, 13 },
            { 617, 13 },

            { 707, 13 },
            { 708, 13 },
            { 712, 13 },
            { 713, 13 },
            { 709, 13 },
            { 710, 13 },
            { 383, 13 },
            { 228, 14 }
        };

        public int GetStartColumnIndexOfSecondSection(int bettype)
        {
            int startColumnIndex = 12;
            startColumnIndex = groupBettypeStartColumnIndex.ContainsKey(bettype) ? groupBettypeStartColumnIndex[bettype] : startColumnIndex;

            return startColumnIndex;
        }
    }
}