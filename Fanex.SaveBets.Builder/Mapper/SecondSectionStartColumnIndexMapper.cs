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
            { 606, 20 },
            { 607, 20 },
            { 609, 20 },
            { 610, 20 },
            { 611, 20 },
            { 612, 20 },
            { 613, 20 },
            { 615, 20 },
            { 616, 20 },
            { 617, 20 },
            { 707, 20 },
            { 708, 20 },
            { 712, 20 },
            { 713, 20 },
            { 709, 20 },
            { 710, 20 },
            { 383, 20 },
            { 228, 21 },

            { 9410, 20 },
            { 9411, 20 },
            { 9412, 20 },
            { 9413, 20 },
            { 9414, 20 },
            { 9415, 20 },
            { 9416, 20 },
            { 9417, 20 },
            { 9418, 20 },
            { 9419, 20 },
            { 9420, 20 },
            { 9421, 20 },
            { 9422, 20 },
            { 9423, 20 },
            { 9428, 20 },
            { 9429, 20 },
            { 9430, 20 },
            { 9431, 20 },
            { 9432, 20 },
            { 9433, 20 },
            { 9607, 20 },
            { 9608, 20 },
            { 9609, 20 },
            { 9610, 20 },
            { 9611, 20 },
            { 9612, 20 },
            { 9613, 20 },
            { 9614, 20 },

            { 9504, 20 },
            { 9505, 20 },
            { 9506, 20 },
            { 9507, 20 },
            { 9508, 20 },
            { 9509, 20 },
            { 9510, 20 },
            { 9511, 20 },
            { 9512, 20 },
            { 9513, 20 },
            { 9514, 20 },
            { 9515, 20 },
            { 9516, 20 },
            { 9517, 20 },
            { 9518, 20 },
            { 9519, 20 },
            { 229, 21 },
        };

        public int GetStartColumnIndexOfSecondSection(int bettype)
        {
            int startColumnIndex = 19;
            startColumnIndex = groupBettypeStartColumnIndex.ContainsKey(bettype) ? groupBettypeStartColumnIndex[bettype] : startColumnIndex;

            return startColumnIndex;
        }
    }
}