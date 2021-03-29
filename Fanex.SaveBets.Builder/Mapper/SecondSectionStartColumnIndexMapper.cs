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
            { 606, 19 },
            { 607, 19 },
            { 609, 19 },
            { 610, 19 },
            { 611, 19 },
            { 612, 19 },
            { 613, 19 },
            { 615, 19 },
            { 616, 19 },
            { 617, 19 },

            { 707, 19 },
            { 708, 19 },
            { 712, 19 },
            { 713, 19 },
            { 709, 19 },
            { 710, 19 },
            { 383, 19 },
            { 228, 20 }

            { 9410, 13 },
            { 9411, 13 },
            { 9412, 13 },
            { 9413, 13 },
            { 9414, 13 },
            { 9415, 13 },
            { 9416, 13 },
            { 9417, 13 },
            { 9418, 13 },
            { 9419, 13 },
            { 9420, 13 },
            { 9421, 13 },
            { 9422, 13 },
            { 9423, 13 },
            { 9428, 13 },
            { 9429, 13 },
            { 9430, 13 },
            { 9431, 13 },
            { 9432, 13 },
            { 9433, 13 },

            { 9504, 13 },
            { 9505, 13 },
            { 9506, 13 },
            { 9507, 13 },
            { 9508, 13 },
            { 9509, 13 },
            { 9510, 13 },
            { 9511, 13 },
            { 9512, 13 },
            { 9513, 13 },
            { 9514, 13 },
            { 9515, 13 },
            { 9516, 13 },
            { 9517, 13 },
            { 9518, 13 },
            { 9519, 13 }
        };

        public int GetStartColumnIndexOfSecondSection(int bettype)
        {
            int startColumnIndex = 19;
            startColumnIndex = groupBettypeStartColumnIndex.ContainsKey(bettype) ? groupBettypeStartColumnIndex[bettype] : startColumnIndex;

            return startColumnIndex;
        }
    }
}