using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Fanex.SaveBets.Builder.Mapper
{
    public class HeaderBettypeMapperImp : HeaderBettypeMapper
    {
        private const string Header1ColumnTeamImp = "Header1ColumnTeamImp";
        private const string Header1ColumnHdpImp = "Header1ColumnHdpImp";
        private const string Header1ColumnMyanmarOddBettype = "Header1ColumnMyanmarOddBettype";
        private const string Header3ColumnImp = "Header3ColumnImp";
        private const string Header0Column = "Header0Column";
        private const string Header2ColumnImp = "Header2ColumnImp";
        private const string Header1ColumnBetOptionImp = "Header1ColumnBetOptionImp";
        private const string Header2ColumnBetOptionBetTypeName = "Header2ColumnBetOptionBetTypeName";
        private const string Header3ColumnBetOptionHandicapImp = "Header3ColumnBetOptionHandicapImp";
        private const string Header4ColumnImp = "Header4ColumnImp";
        private const string Header3ColumnTeamIntervalHandicapImp = "Header3ColumnTeamIntervalHandicapImp";
        private const string Header1ColumnPointImp = "Header1ColumnPointImp";
        private const string Header1ColumnScoreBoxOddsImp = "Header1ColumnScoreBoxOddsImp";

        private readonly Dictionary<int, string> groupBettypes;

        [SuppressMessage("StyleCop.CSharp.StylecopPlus.Rules", "SP2101:MethodMustNotContainMoreLinesThan", Justification = "Reviewed. Suppression is OK here.")]
        public HeaderBettypeMapperImp()
        {
            groupBettypes = new Dictionary<int, string>()
            {
                { 1, Header1ColumnHdpImp },
                { 7, Header1ColumnHdpImp },
                { 2, Header1ColumnHdpImp },
                { 12, Header1ColumnHdpImp },
                { 3, Header1ColumnHdpImp },
                { 8, Header1ColumnHdpImp },
                { 20, Header1ColumnHdpImp },
                { 465, Header1ColumnHdpImp },
                { 466, Header1ColumnHdpImp },
                { 500, Header1ColumnHdpImp },
                { 501, Header1ColumnHdpImp },

                { 401, Header1ColumnTeamImp },
                { 402, Header1ColumnTeamImp },
                { 403, Header1ColumnTeamImp },
                { 404, Header1ColumnTeamImp },
                { 461, Header1ColumnTeamImp },
                { 462, Header1ColumnTeamImp },
                { 463, Header1ColumnTeamImp },
                { 464, Header1ColumnTeamImp },
                { 411, Header1ColumnTeamImp },
                { 427, Header1ColumnTeamImp },
                { 428, Header1ColumnTeamImp },
                { 432, Header1ColumnTeamImp },
                { 433, Header1ColumnTeamImp },
                { 434, Header1ColumnTeamImp },
                { 435, Header1ColumnTeamImp },
                { 436, Header1ColumnTeamImp },
                { 437, Header1ColumnTeamImp },
                { 438, Header1ColumnTeamImp },
                { 439, Header1ColumnTeamImp },
                { 440, Header1ColumnTeamImp },
                { 441, Header1ColumnTeamImp },
                { 425, Header1ColumnTeamImp },

                { 468, Header1ColumnScoreBoxOddsImp },
                { 469, Header1ColumnScoreBoxOddsImp },

                { 301, Header1ColumnMyanmarOddBettype },
                { 302, Header1ColumnMyanmarOddBettype },
                { 303, Header1ColumnMyanmarOddBettype },
                { 304, Header1ColumnMyanmarOddBettype },

                { 4, Header3ColumnImp },
                { 30, Header3ColumnImp },
                { 6, Header3ColumnImp },
                { 126, Header3ColumnImp },
                { 13, Header3ColumnImp },
                { 14, Header3ColumnImp },
                { 127, Header3ColumnImp },
                { 16, Header3ColumnImp },
                { 24, Header3ColumnImp },
                { 25, Header3ColumnImp },
                { 27, Header3ColumnImp },
                { 28, Header3ColumnImp },
                { 405, Header3ColumnImp },
                { 406, Header3ColumnImp },
                { 407, Header3ColumnImp },
                { 408, Header3ColumnImp },
                { 409, Header3ColumnImp },
                { 410, Header3ColumnImp },
                { 412, Header3ColumnImp },
                { 413, Header3ColumnImp },
                { 414, Header3ColumnImp },
                { 415, Header3ColumnImp },
                { 416, Header3ColumnImp },
                { 431, Header3ColumnImp },
                { 442, Header3ColumnImp },
                { 443, Header3ColumnImp },
                { 444, Header3ColumnImp },
                { 417, Header3ColumnImp },
                { 418, Header3ColumnImp },
                { 419, Header3ColumnImp },
                { 420, Header3ColumnImp },
                { 421, Header3ColumnImp },
                { 422, Header3ColumnImp },
                { 423, Header3ColumnImp },
                { 424, Header3ColumnImp },
                { 426, Header3ColumnImp },
                { 429, Header3ColumnImp },
                { 445, Header3ColumnImp },
                { 446, Header3ColumnImp },
                { 447, Header3ColumnImp },

                { 5, Header0Column },
                { 15, Header0Column },
                { 430, Header0Column },
                { 458, Header0Column },
                { 459, Header0Column },

                { 221, Header3ColumnTeamIntervalHandicapImp },
                { 222, Header3ColumnTeamIntervalHandicapImp },
                { 223, Header2ColumnImp },
                { 224, Header2ColumnImp },
                { 225, Header2ColumnImp },
                { 226, Header2ColumnImp },
                { 227, Header2ColumnImp },

                { 601, Header3ColumnBetOptionHandicapImp },
                { 602, Header3ColumnBetOptionHandicapImp },
                { 608, Header3ColumnBetOptionHandicapImp },

                { 603, Header1ColumnBetOptionImp },
                { 604, Header1ColumnBetOptionImp },
                { 605, Header1ColumnBetOptionImp },

                { 606, Header2ColumnBetOptionBetTypeName },
                { 607, Header2ColumnBetOptionBetTypeName },
                { 609, Header2ColumnBetOptionBetTypeName },
                { 610, Header2ColumnBetOptionBetTypeName },
                { 611, Header2ColumnBetOptionBetTypeName },
                { 612, Header2ColumnBetOptionBetTypeName },
                { 613, Header2ColumnBetOptionBetTypeName },
                { 615, Header2ColumnBetOptionBetTypeName },
                { 616, Header2ColumnBetOptionBetTypeName },
                { 617, Header2ColumnBetOptionBetTypeName },

                { 614, Header4ColumnImp },

                { 2701, Header0Column },
                { 2702, Header0Column },
                { 2703, Header1ColumnHdpImp },
                { 2704, Header1ColumnHdpImp },
                { 2705, Header1ColumnHdpImp },
                { 2706, Header1ColumnHdpImp },
                { 2707, Header3ColumnImp },
                { 2801, Header1ColumnHdpImp },
                { 2802, Header0Column },
                { 2803, Header1ColumnHdpImp },
                { 2804, Header1ColumnHdpImp },
                { 2805, Header1ColumnHdpImp },
                { 2806, Header1ColumnHdpImp },
                { 2807, Header3ColumnImp },
                { 2808, Header3ColumnImp },
                { 2809, Header1ColumnPointImp },
                { 2811, Header1ColumnHdpImp },
                { 2812, Header1ColumnHdpImp },

                { 9002, Header1ColumnHdpImp },
                { 9008, Header1ColumnHdpImp },
                { 9012, Header1ColumnHdpImp },
                { 9018, Header1ColumnHdpImp },
                { 9024, Header1ColumnHdpImp },
                { 9028, Header1ColumnHdpImp },
                { 9034, Header1ColumnHdpImp },
                { 9040, Header1ColumnHdpImp },
                { 9046, Header1ColumnHdpImp },
                { 9052, Header1ColumnHdpImp },
                { 9059, Header1ColumnHdpImp },
                { 9076, Header1ColumnHdpImp },
                { 9077, Header1ColumnHdpImp },

                { 9001, Header1ColumnHdpImp },
                { 9004, Header1ColumnHdpImp },
                { 9006, Header1ColumnHdpImp },
                { 9007, Header1ColumnHdpImp },
                { 9010, Header1ColumnHdpImp },
                { 9011, Header1ColumnHdpImp },
                { 9014, Header1ColumnHdpImp },
                { 9015, Header1ColumnHdpImp },
                { 9016, Header1ColumnHdpImp },
                { 9017, Header1ColumnHdpImp },
                { 9020, Header1ColumnHdpImp },
                { 9021, Header1ColumnHdpImp },
                { 9022, Header1ColumnHdpImp },
                { 9023, Header1ColumnHdpImp },
                { 9026, Header1ColumnHdpImp },
                { 9027, Header1ColumnHdpImp },
                { 9030, Header1ColumnHdpImp },
                { 9031, Header1ColumnHdpImp },
                { 9032, Header1ColumnHdpImp },
                { 9033, Header1ColumnHdpImp },
                { 9036, Header1ColumnHdpImp },
                { 9037, Header1ColumnHdpImp },
                { 9038, Header1ColumnHdpImp },
                { 9039, Header1ColumnHdpImp },
                { 9042, Header1ColumnHdpImp },
                { 9043, Header1ColumnHdpImp },
                { 9044, Header1ColumnHdpImp },
                { 9045, Header1ColumnHdpImp },
                { 9048, Header1ColumnHdpImp },
                { 9049, Header1ColumnHdpImp },
                { 9050, Header1ColumnHdpImp },
                { 9051, Header1ColumnHdpImp },
                { 9054, Header1ColumnHdpImp },
                { 9055, Header1ColumnHdpImp },
                { 9056, Header1ColumnHdpImp },
                { 9057, Header1ColumnHdpImp },
                { 9062, Header1ColumnHdpImp },
                { 9063, Header1ColumnHdpImp },
                { 9064, Header1ColumnHdpImp },
                { 9065, Header1ColumnHdpImp },
                { 9066, Header1ColumnHdpImp },
                { 9067, Header1ColumnHdpImp },
                { 9068, Header1ColumnHdpImp },
                { 9069, Header1ColumnHdpImp },
                { 9072, Header1ColumnHdpImp },
                { 9073, Header1ColumnHdpImp },
                { 9075, Header1ColumnHdpImp },


                { 9005, Header1ColumnHdpImp },
                { 9061, Header1ColumnHdpImp },
                { 9071, Header1ColumnHdpImp },
                { 9078, Header1ColumnHdpImp },
                { 9079, Header1ColumnHdpImp },
                { 9080, Header1ColumnHdpImp },
                { 9081, Header1ColumnHdpImp },
                { 9082, Header1ColumnHdpImp },
                { 9083, Header1ColumnHdpImp },
                { 9084, Header1ColumnHdpImp },
                { 9085, Header1ColumnHdpImp },
                { 9086, Header1ColumnHdpImp },

                { 9003, Header1ColumnHdpImp },
                { 9009, Header1ColumnHdpImp },
                { 9013, Header1ColumnHdpImp },
                { 9019, Header1ColumnHdpImp },
                { 9025, Header1ColumnHdpImp },
                { 9029, Header1ColumnHdpImp },
                { 9035, Header1ColumnHdpImp },
                { 9041, Header1ColumnHdpImp },
                { 9047, Header1ColumnHdpImp },
                { 9053, Header1ColumnHdpImp },
                { 9058, Header1ColumnHdpImp },
                { 9060, Header1ColumnHdpImp },
                { 9070, Header1ColumnHdpImp },
                { 9074, Header1ColumnHdpImp }
            };
        }

        public string GetHeaderHandicapAndBetTeamOf(int bettype)
        {
            string bettypeGroup = "HeaderDefault1Section3ColumnImp";
            if (groupBettypes.ContainsKey(bettype))
            {
                bettypeGroup = groupBettypes[bettype];
            }

            return bettypeGroup;
        }
    }
}