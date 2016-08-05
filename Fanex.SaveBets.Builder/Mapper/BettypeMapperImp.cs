using System.Collections.Generic;

namespace Fanex.SaveBets.Builder.Mapper
{
    public class BettypeMapperImp : BettypeMapper
    {
        private Dictionary<int, int> groupBettypes;

        public BettypeMapperImp()
        {
            groupBettypes = new Dictionary<int, int>()
            {
                { 401, 1 },
                { 402, 1 },
                { 403, 1 },
                { 404, 1 },

                { 405, 3 },
                { 406, 3 },
                { 407, 3 },
                { 408, 3 },
                { 409, 3 },
                { 410, 3 },

                { 411, 1 },

                { 412, 3 },
                { 413, 3 },
                { 414, 3 },
                { 415, 3 },
                { 416, 3 },

                { 427, 1 },
                { 428, 1 },

                { 431, 3 },

                { 432, 1 },
                { 433, 1 },
                { 434, 1 },
                { 435, 1 },
                { 436, 1 },
                { 437, 1 },
                { 438, 1 },
                { 439, 1 },
                { 440, 1 },
                { 441, 1 },

                { 442, 3 },
                { 443, 3 },
                { 444, 3 },
            };
        }

        public int GetNumOfColumnOf(int bettype)
        {
            int bettypeGroup = 0;
            if (groupBettypes.ContainsKey(bettype))
            {
                bettypeGroup = groupBettypes[bettype];
            }

            return bettypeGroup;
        }
    }
}