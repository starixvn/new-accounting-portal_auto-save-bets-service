using System.Collections.Generic;

namespace Fanex.SaveBets.Builder.Mapper
{
    public class SectionBettypeMapperImp : SectionBettypeMapper
    {
        private const int OneSection = 1;
        private const int TwoSection = 2;
        private const int ThreeSection = 3;

        private Dictionary<int, int> bettypeGroup = new Dictionary<int, int>()
        {
            { 4, OneSection },
            { 30, OneSection },
            { 6, OneSection },
            { 126, OneSection },
            { 13, OneSection },
            { 14, OneSection },
            { 127, OneSection },
            { 16, OneSection },
            { 24, OneSection },
            { 26, OneSection },
            { 27, OneSection },
            { 28, OneSection },
            { 405, OneSection },
            { 406, OneSection },
            { 407, OneSection },
            { 408, OneSection },
            { 171, OneSection },
            { 409, OneSection },
            { 410, OneSection },
            { 412, OneSection },
            { 413, OneSection },
            { 414, OneSection },
            { 415, OneSection },
            { 416, OneSection },
            { 431, OneSection },
            { 442, OneSection },
            { 443, OneSection },
            { 444, OneSection },
            { 417, OneSection },
            { 418, OneSection },
            { 419, OneSection },
            { 420, OneSection },
            { 421, OneSection },
            { 422, OneSection },
            { 423, OneSection },
            { 424, OneSection },
            { 426, OneSection },
            { 429, OneSection },
            { 445, OneSection },
            { 446, OneSection },
            { 447, OneSection },
            { 221, OneSection },
            { 222, OneSection },
            { 223, OneSection },
            { 224, OneSection },
            { 225, OneSection },
            { 1, TwoSection },
            { 7, TwoSection },
            { 2, TwoSection },
            { 12, TwoSection },
            { 3, TwoSection },
            { 8, TwoSection },
            { 20, TwoSection },
            { 401, TwoSection },
            { 402, TwoSection },
            { 403, TwoSection },
            { 404, TwoSection },
            { 411, TwoSection },
            { 427, TwoSection },
            { 428, TwoSection },
            { 432, TwoSection },
            { 433, TwoSection },
            { 434, TwoSection },
            { 435, TwoSection },
            { 436, TwoSection },
            { 437, TwoSection },
            { 438, TwoSection },
            { 439, TwoSection },
            { 440, TwoSection },
            { 441, TwoSection },
            { 425, TwoSection },
            { 301, TwoSection },
            { 302, TwoSection },
            { 303, TwoSection },
            { 304, TwoSection },
            { 5, ThreeSection },
            { 15, ThreeSection },
            { 430, ThreeSection },
        };

        public int GetNumberSectionOf(int bettype)
        {
            int section = OneSection;
            section = bettypeGroup.ContainsKey(bettype) ? bettypeGroup[bettype] : section;

            return section;
        }

        public bool IsMyanmarBettypeGroup(int bettype)
        {
            List<int> myanmarBettypes = new List<int>()
            {
                301,
                302,
                303,
                304
            };

            return myanmarBettypes.Contains(bettype);
        }
    }
}