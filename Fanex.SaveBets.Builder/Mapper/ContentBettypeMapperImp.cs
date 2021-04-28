using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Fanex.SaveBets.Builder.Mapper
{
    public class ContentBettypeMapperImp : ContentBettypeMapper
    {
        private const string ContentBetChoice1Imp = "ContentBetChoice1Imp";
        private const string ContentBetChoice2Imp = "ContentBetChoice2Imp";
        private const string ContentBetChoice5Imp = "ContentBetChoice5Imp";
        private const string ContentBetChoice4Imp = "ContentBetChoice4Imp";
        private const string ContentBetChoice401Imp = "ContentBetChoice401Imp";
        private const string ContentBetChoice405Imp = "ContentBetChoice405Imp";
        private const string ContentBetChoice406Imp = "ContentBetChoice406Imp";
        private const string ContentBetChoice407Imp = "ContentBetChoice407Imp";
        private const string ContentBetChoice409Imp = "ContentBetChoice409Imp";
        private const string ContentBetChoice411Imp = "ContentBetChoice411Imp";
        private const string ContentBetChoice412Imp = "ContentBetChoice412Imp";
        private const string ContentBetChoice413Imp = "ContentBetChoice413Imp";
        private const string ContentBetChoice416Imp = "ContentBetChoice416Imp";
        private const string ContentBetChoice427Imp = "ContentBetChoice427Imp";
        private const string ContentBetChoice432Imp = "ContentBetChoice432Imp";
        private const string ContentBetChoice433Imp = "ContentBetChoice433Imp";
        private const string ContentBetChoice442Imp = "ContentBetChoice442Imp";
        private const string ContentBetChoice417Imp = "ContentBetChoice417Imp";
        private const string ContentBetChoice424Imp = "ContentBetChoice424Imp";
        private const string ContentBetChoice425Imp = "ContentBetChoice425Imp";
        private const string ContentBetChoice426Imp = "ContentBetChoice426Imp";
        private const string ContentBetChoice603_604_605Imp = "ContentBetChoice603_604_605Imp";
        private const string ContentBetChoice606Imp = "ContentBetChoice606Imp";
        private const string ContentBetChoice607Imp = "ContentBetChoice607Imp";
        private const string ContentBetChoice609Imp = "ContentBetChoice609Imp";
        private const string ContentBetChoice610Imp = "ContentBetChoice610Imp";
        private const string ContentBetChoice611Imp = "ContentBetChoice611Imp";
        private const string ContentBetChoice612Imp = "ContentBetChoice612Imp";
        private const string ContentBetChoice613Imp = "ContentBetChoice613Imp";
        private const string ContentBetChoice614Imp = "ContentBetChoice614Imp";
        private const string ContentBetChoice615Imp = "ContentBetChoice615Imp";
        private const string ContentBetChoice616Imp = "ContentBetChoice616Imp";
        private const string ContentBetChoice617Imp = "ContentBetChoice617Imp";
        private const string ContentBetChoice221_222Imp = "ContentBetChoice221_222Imp";
        private const string ContentBetChoice223To227Imp = "ContentBetChoice223To227Imp";
        private const string ContentBetChoice6Imp = "ContentBetChoice6Imp";
        private const string ContentBetChoice467Imp = "ContentBetChoice467Imp";
        private const string ContentBetChoice468Imp = "ContentBetChoice468Imp";
        private const string ContentBetChoice703Imp = "ContentBetChoice703Imp";
        private const string ContentBetChoice711Imp = "ContentBetChoice711Imp";
        private const string ContentBetChoice707Imp = "ContentBetChoice707Imp";
        private const string ContentBetChoice709Imp = "ContentBetChoice709Imp";

        private const string ContentBetChoice3Hdp2Hdp1OddsSpread = "ContentBetChoice3Hdp2Hdp1OddsSpread";
        private const string ContentBetChoice3BetTeamHdp2BetCheck = "ContentBetChoice3BetTeamHdp2BetCheck";
        private const string ContentBetChoice4ColumnImp = "ContentBetChoice4ColumnImp";

        private readonly Dictionary<int, string> groupBettypes;

        [SuppressMessage("StyleCop.CSharp.StylecopPlus.Rules", "SP2101:MethodMustNotContainMoreLinesThan", Justification = "Reviewed. Suppression is OK here.")]
        public ContentBettypeMapperImp()
        {
            groupBettypes = new Dictionary<int, string>()
            {
                { 1, ContentBetChoice1Imp },
                { 7, ContentBetChoice1Imp },
                { 20, ContentBetChoice1Imp },
                { 465, ContentBetChoice1Imp },
                { 466, ContentBetChoice1Imp },
                { 701, ContentBetChoice1Imp },
                { 704, ContentBetChoice1Imp },

                { 2, ContentBetChoice2Imp },
                { 12, ContentBetChoice2Imp },
                { 3, ContentBetChoice2Imp },
                { 8, ContentBetChoice2Imp },
                { 500, ContentBetChoice2Imp },
                { 501, ContentBetChoice2Imp },
                { 470, ContentBetChoice2Imp },
                { 471, ContentBetChoice2Imp },
                { 472, ContentBetChoice2Imp },
                { 482, ContentBetChoice2Imp },
                { 483, ContentBetChoice2Imp },
                { 484, ContentBetChoice2Imp },
                { 485, ContentBetChoice2Imp },
                { 702, ContentBetChoice2Imp },
                { 705, ContentBetChoice2Imp },
                { 706, ContentBetChoice2Imp },

                { 5, ContentBetChoice5Imp },
                { 15, ContentBetChoice5Imp },

                { 4, ContentBetChoice4Imp },
                { 30, ContentBetChoice4Imp },
                { 6, ContentBetChoice4Imp },
                { 126, ContentBetChoice4Imp },
                { 13, ContentBetChoice4Imp },
                { 14, ContentBetChoice4Imp },
                { 127, ContentBetChoice4Imp },
                { 16, ContentBetChoice4Imp },
                { 24, ContentBetChoice4Imp },
                { 25, ContentBetChoice4Imp },
                { 26, ContentBetChoice4Imp },
                { 27, ContentBetChoice4Imp },
                { 28, ContentBetChoice4Imp },

                { 401, ContentBetChoice401Imp },
                { 402, ContentBetChoice401Imp },
                { 403, ContentBetChoice401Imp },
                { 404, ContentBetChoice401Imp },
                { 461, ContentBetChoice401Imp },
                { 462, ContentBetChoice401Imp },
                { 463, ContentBetChoice401Imp },
                { 464, ContentBetChoice401Imp },

                { 405, ContentBetChoice405Imp },
                { 406, ContentBetChoice406Imp },
                { 407, ContentBetChoice407Imp },
                { 409, ContentBetChoice409Imp },
                { 411, ContentBetChoice411Imp },
                { 412, ContentBetChoice412Imp },

                { 413, ContentBetChoice413Imp },
                { 414, ContentBetChoice413Imp },

                { 416, ContentBetChoice416Imp },

                { 427, ContentBetChoice427Imp },
                { 428, ContentBetChoice427Imp },

                { 432, ContentBetChoice432Imp },

                { 433, ContentBetChoice433Imp },
                { 434, ContentBetChoice433Imp },
                { 435, ContentBetChoice433Imp },
                { 436, ContentBetChoice433Imp },
                { 437, ContentBetChoice433Imp },
                { 438, ContentBetChoice433Imp },
                { 439, ContentBetChoice433Imp },
                { 440, ContentBetChoice433Imp },
                { 441, ContentBetChoice433Imp },

                { 442, ContentBetChoice442Imp },
                { 443, ContentBetChoice442Imp },
                { 444, ContentBetChoice442Imp },

                { 417, ContentBetChoice417Imp },
                { 418, ContentBetChoice417Imp },
                { 419, ContentBetChoice417Imp },
                { 420, ContentBetChoice417Imp },
                { 421, ContentBetChoice417Imp },
                { 422, ContentBetChoice417Imp },
                { 423, ContentBetChoice417Imp },

                { 424, ContentBetChoice424Imp },

                { 425, ContentBetChoice425Imp },

                { 429, ContentBetChoice426Imp },
                { 445, ContentBetChoice426Imp },
                { 446, ContentBetChoice426Imp },
                { 447, ContentBetChoice426Imp },

                { 467, ContentBetChoice467Imp },
                { 468, ContentBetChoice468Imp },
                { 469, ContentBetChoice468Imp },

                { 703, ContentBetChoice703Imp },

                { 711, ContentBetChoice711Imp },
                { 714, ContentBetChoice711Imp },

                { 707, ContentBetChoice707Imp },
                { 708, ContentBetChoice707Imp },
                { 712, ContentBetChoice707Imp },
                { 713, ContentBetChoice707Imp },

                { 709, ContentBetChoice709Imp },
                { 710, ContentBetChoice709Imp },

                { 221, ContentBetChoice221_222Imp },
                { 222, ContentBetChoice221_222Imp },
                { 223, ContentBetChoice223To227Imp },
                { 224, ContentBetChoice223To227Imp },
                { 225, ContentBetChoice223To227Imp },
                { 226, ContentBetChoice223To227Imp },
                { 227, ContentBetChoice223To227Imp },

                { 603, ContentBetChoice603_604_605Imp },
                { 604, ContentBetChoice603_604_605Imp },
                { 605, ContentBetChoice603_604_605Imp },

                { 606, ContentBetChoice606Imp },
                { 607, ContentBetChoice607Imp },
                { 609, ContentBetChoice609Imp },
                { 610, ContentBetChoice610Imp },
                { 611, ContentBetChoice611Imp },
                { 612, ContentBetChoice612Imp },
                { 613, ContentBetChoice613Imp },
                { 614, ContentBetChoice614Imp },
                { 615, ContentBetChoice615Imp },
                { 616, ContentBetChoice616Imp },
                { 617, ContentBetChoice617Imp },

                { 2703, ContentBetChoice2Imp },
                { 2704, ContentBetChoice2Imp },

                { 2705, ContentBetChoice1Imp },
                { 2706, ContentBetChoice1Imp },
                { 2707, ContentBetChoice4Imp },
                { 2801, ContentBetChoice2Imp },
                { 2803, ContentBetChoice2Imp },
                { 2804, ContentBetChoice2Imp },
                { 2805, ContentBetChoice1Imp },
                { 2806, ContentBetChoice1Imp },
                { 2807, ContentBetChoice4Imp },
                { 2808, ContentBetChoice4Imp },
                { 2809, ContentBetChoice6Imp },
                { 2811, ContentBetChoice2Imp },
                { 2812, ContentBetChoice2Imp },

                { 9002, ContentBetChoice1Imp },
                { 9008, ContentBetChoice1Imp },
                { 9012, ContentBetChoice1Imp },
                { 9018, ContentBetChoice1Imp },
                { 9024, ContentBetChoice1Imp },
                { 9028, ContentBetChoice1Imp },
                { 9034, ContentBetChoice1Imp },
                { 9040, ContentBetChoice1Imp },
                { 9046, ContentBetChoice1Imp },
                { 9052, ContentBetChoice1Imp },
                { 9059, ContentBetChoice1Imp },
                { 9076, ContentBetChoice1Imp },
                { 9077, ContentBetChoice1Imp },

                { 9001, ContentBetChoice2Imp },
                { 9004, ContentBetChoice2Imp },
                { 9006, ContentBetChoice2Imp },
                { 9007, ContentBetChoice2Imp },
                { 9010, ContentBetChoice2Imp },
                { 9011, ContentBetChoice2Imp },
                { 9014, ContentBetChoice2Imp },
                { 9015, ContentBetChoice2Imp },
                { 9016, ContentBetChoice2Imp },
                { 9017, ContentBetChoice2Imp },
                { 9020, ContentBetChoice2Imp },
                { 9021, ContentBetChoice2Imp },
                { 9022, ContentBetChoice2Imp },
                { 9023, ContentBetChoice2Imp },
                { 9026, ContentBetChoice2Imp },
                { 9027, ContentBetChoice2Imp },
                { 9030, ContentBetChoice2Imp },
                { 9031, ContentBetChoice2Imp },
                { 9032, ContentBetChoice2Imp },
                { 9033, ContentBetChoice2Imp },
                { 9036, ContentBetChoice2Imp },
                { 9037, ContentBetChoice2Imp },
                { 9038, ContentBetChoice2Imp },
                { 9039, ContentBetChoice2Imp },
                { 9042, ContentBetChoice2Imp },
                { 9043, ContentBetChoice2Imp },
                { 9044, ContentBetChoice2Imp },
                { 9045, ContentBetChoice2Imp },
                { 9048, ContentBetChoice2Imp },
                { 9049, ContentBetChoice2Imp },
                { 9050, ContentBetChoice2Imp },
                { 9051, ContentBetChoice2Imp },
                { 9054, ContentBetChoice2Imp },
                { 9055, ContentBetChoice2Imp },
                { 9056, ContentBetChoice2Imp },
                { 9057, ContentBetChoice2Imp },
                { 9062, ContentBetChoice2Imp },
                { 9063, ContentBetChoice2Imp },
                { 9064, ContentBetChoice2Imp },
                { 9065, ContentBetChoice2Imp },
                { 9066, ContentBetChoice2Imp },
                { 9067, ContentBetChoice2Imp },
                { 9068, ContentBetChoice2Imp },
                { 9069, ContentBetChoice2Imp },
                { 9072, ContentBetChoice2Imp },
                { 9073, ContentBetChoice2Imp },
                { 9075, ContentBetChoice2Imp },
                { 9005, ContentBetChoice2Imp },
                { 9061, ContentBetChoice2Imp },
                { 9071, ContentBetChoice2Imp },
                { 9078, ContentBetChoice2Imp },
                { 9079, ContentBetChoice2Imp },
                { 9080, ContentBetChoice2Imp },
                { 9081, ContentBetChoice2Imp },
                { 9082, ContentBetChoice2Imp },
                { 9083, ContentBetChoice2Imp },
                { 9084, ContentBetChoice2Imp },
                { 9085, ContentBetChoice2Imp },
                { 9086, ContentBetChoice2Imp },
                { 9003, ContentBetChoice2Imp },
                { 9009, ContentBetChoice2Imp },
                { 9013, ContentBetChoice2Imp },
                { 9019, ContentBetChoice2Imp },
                { 9025, ContentBetChoice2Imp },
                { 9029, ContentBetChoice2Imp },
                { 9035, ContentBetChoice2Imp },
                { 9041, ContentBetChoice2Imp },
                { 9047, ContentBetChoice2Imp },
                { 9053, ContentBetChoice2Imp },
                { 9058, ContentBetChoice2Imp },
                { 9060, ContentBetChoice2Imp },
                { 9070, ContentBetChoice2Imp },
                { 9074, ContentBetChoice2Imp },

                { 393, ContentBetChoice2Imp },
                { 394, ContentBetChoice2Imp },
                { 400, ContentBetChoice2Imp },
                { 386, ContentBetChoice2Imp },
                { 387, ContentBetChoice2Imp },
                { 388, ContentBetChoice2Imp },
                { 389, ContentBetChoice2Imp },

                { 381, ContentBetChoice711Imp },
                { 382, ContentBetChoice711Imp },
                { 384, ContentBetChoice711Imp },
                { 385, ContentBetChoice711Imp },
                { 395, ContentBetChoice711Imp },
                { 396, ContentBetChoice711Imp },

                { 390, ContentBetChoice703Imp },
                { 391, ContentBetChoice703Imp },
                { 392, ContentBetChoice703Imp },
                { 397, ContentBetChoice703Imp },
                { 398, ContentBetChoice703Imp },
                { 399, ContentBetChoice703Imp },
                { 383, ContentBetChoice707Imp },

                { 618, ContentBetChoice703Imp },
                { 619, ContentBetChoice703Imp },
                { 620, ContentBetChoice703Imp },
                { 621, ContentBetChoice703Imp },
                { 622, ContentBetChoice703Imp },
                { 623, ContentBetChoice703Imp },
                { 624, ContentBetChoice703Imp },
                { 625, ContentBetChoice703Imp },
                { 626, ContentBetChoice703Imp },
                { 632, ContentBetChoice703Imp },
                { 633, ContentBetChoice703Imp },
                { 634, ContentBetChoice703Imp },
                { 640, ContentBetChoice703Imp },
                { 642, ContentBetChoice703Imp },
                { 643, ContentBetChoice703Imp },
                { 644, ContentBetChoice703Imp },
                { 645, ContentBetChoice703Imp },
                { 627, ContentBetChoice711Imp },
                { 628, ContentBetChoice711Imp },
                { 629, ContentBetChoice711Imp },
                { 630, ContentBetChoice711Imp },
                { 631, ContentBetChoice711Imp },
                { 646, ContentBetChoice467Imp },
                { 635, ContentBetChoice1Imp },
                { 636, ContentBetChoice1Imp },
                { 637, ContentBetChoice1Imp },
                { 638, ContentBetChoice2Imp },
                { 639, ContentBetChoice2Imp },
                { 641, ContentBetChoice2Imp },
                { 228, ContentBetChoice3Hdp2Hdp1OddsSpread },
                { 376, ContentBetChoice3BetTeamHdp2BetCheck },

                { 9400, ContentBetChoice1Imp },
                { 9401, ContentBetChoice1Imp },

                { 9404, ContentBetChoice2Imp },
                { 9405, ContentBetChoice2Imp },
                { 9406, ContentBetChoice2Imp },
                { 9407, ContentBetChoice2Imp },
                { 9408, ContentBetChoice2Imp },
                { 9409, ContentBetChoice2Imp },
                { 9446, ContentBetChoice2Imp },
                { 9447, ContentBetChoice2Imp },
                { 9449, ContentBetChoice2Imp },
                { 9450, ContentBetChoice2Imp },
                { 9452, ContentBetChoice2Imp },
                { 9453, ContentBetChoice2Imp },
                { 9454, ContentBetChoice2Imp },
                { 9455, ContentBetChoice2Imp },
                { 9460, ContentBetChoice2Imp },
                { 9461, ContentBetChoice2Imp },
                { 9604, ContentBetChoice2Imp },

                { 9410, ContentBetChoice709Imp },
                { 9411, ContentBetChoice709Imp },
                { 9412, ContentBetChoice709Imp },
                { 9413, ContentBetChoice709Imp },
                { 9414, ContentBetChoice709Imp },
                { 9415, ContentBetChoice709Imp },
                { 9416, ContentBetChoice709Imp },
                { 9417, ContentBetChoice709Imp },
                { 9418, ContentBetChoice709Imp },
                { 9419, ContentBetChoice709Imp },
                { 9420, ContentBetChoice709Imp },
                { 9421, ContentBetChoice709Imp },
                { 9422, ContentBetChoice709Imp },
                { 9423, ContentBetChoice709Imp },
                { 9428, ContentBetChoice709Imp },
                { 9429, ContentBetChoice709Imp },
                { 9430, ContentBetChoice709Imp },
                { 9431, ContentBetChoice709Imp },
                { 9432, ContentBetChoice709Imp },
                { 9433, ContentBetChoice709Imp },
                { 9504, ContentBetChoice709Imp },
                { 9505, ContentBetChoice709Imp },
                { 9506, ContentBetChoice709Imp },
                { 9507, ContentBetChoice709Imp },
                { 9508, ContentBetChoice709Imp },
                { 9509, ContentBetChoice709Imp },
                { 9510, ContentBetChoice709Imp },
                { 9511, ContentBetChoice709Imp },
                { 9512, ContentBetChoice709Imp },
                { 9513, ContentBetChoice709Imp },
                { 9514, ContentBetChoice709Imp },
                { 9515, ContentBetChoice709Imp },
                { 9516, ContentBetChoice709Imp },
                { 9517, ContentBetChoice709Imp },
                { 9518, ContentBetChoice709Imp },
                { 9519, ContentBetChoice709Imp },

                { 9434, ContentBetChoice711Imp },
                { 9435, ContentBetChoice711Imp },
                { 9436, ContentBetChoice711Imp },
                { 9437, ContentBetChoice711Imp },
                { 9438, ContentBetChoice711Imp },
                { 9439, ContentBetChoice711Imp },
                //{ 9484, ContentBetChoice711Imp },
                //{ 9485, ContentBetChoice711Imp },
                { 9490, ContentBetChoice711Imp },
                { 9491, ContentBetChoice711Imp },

                { 9496, ContentBetChoice2Imp },
                { 9497, ContentBetChoice2Imp },
                { 9498, ContentBetChoice2Imp },
                { 9499, ContentBetChoice2Imp },
                { 9500, ContentBetChoice2Imp },
                { 9501, ContentBetChoice2Imp },
                { 9502, ContentBetChoice2Imp },
                { 9503, ContentBetChoice2Imp },
                { 9527, ContentBetChoice2Imp },
                { 9528, ContentBetChoice2Imp },
                { 9530, ContentBetChoice2Imp },
                { 9532, ContentBetChoice2Imp },
                { 9533, ContentBetChoice2Imp },
                { 9605, ContentBetChoice2Imp },

                { 9538, ContentBetChoice703Imp },
                { 9539, ContentBetChoice703Imp },
                { 9540, ContentBetChoice703Imp },

                { 9541, ContentBetChoice467Imp },
                { 9542, ContentBetChoice468Imp },
                { 9543, ContentBetChoice468Imp },
                { 9544, ContentBetChoice467Imp },
                { 9545, ContentBetChoice468Imp },
                { 9546, ContentBetChoice468Imp },
                { 9566, ContentBetChoice467Imp },
                { 9567, ContentBetChoice468Imp },
                { 9569, ContentBetChoice468Imp },
                { 9570, ContentBetChoice467Imp },
                { 9572, ContentBetChoice468Imp },
                { 9573, ContentBetChoice468Imp },
                { 9574, ContentBetChoice467Imp },
                { 9575, ContentBetChoice468Imp },
                { 9580, ContentBetChoice468Imp },
                { 9581, ContentBetChoice467Imp },
                { 9606, ContentBetChoice468Imp },

                { 9547, ContentBetChoice4ColumnImp },
                { 9548, ContentBetChoice4ColumnImp },
                { 9549, ContentBetChoice4ColumnImp },
                { 9550, ContentBetChoice4ColumnImp },
                { 9551, ContentBetChoice4ColumnImp },
                { 9552, ContentBetChoice4ColumnImp },
                { 9553, ContentBetChoice4ColumnImp },
                { 9554, ContentBetChoice4ColumnImp },
                { 9555, ContentBetChoice4ColumnImp },
                { 9556, ContentBetChoice4ColumnImp },
                { 9557, ContentBetChoice4ColumnImp },
                { 9558, ContentBetChoice4ColumnImp },
                { 9559, ContentBetChoice4ColumnImp },
                { 9560, ContentBetChoice4ColumnImp },
                { 9561, ContentBetChoice4ColumnImp },
                { 9562, ContentBetChoice4ColumnImp },
                { 9563, ContentBetChoice4ColumnImp },
                { 9564, ContentBetChoice4ColumnImp },
                { 229, ContentBetChoice3Hdp2Hdp1OddsSpread },
            };
        }

        public string GetContentBetchoiceToShowOf(int bettype)
        {
            string bettypeGroup = "ContentBetChoiceDefault1Section3ColumnImp";
            if (groupBettypes.ContainsKey(bettype))
            {
                bettypeGroup = groupBettypes[bettype];
            }

            return bettypeGroup;
        }
    }
}