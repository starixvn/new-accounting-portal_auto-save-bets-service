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

        private readonly Dictionary<int, string> groupBettypes;

        [SuppressMessage("StyleCop.CSharp.StylecopPlus.Rules", "SP2101:MethodMustNotContainMoreLinesThan", Justification = "Reviewed. Suppression is OK here.")]
        public ContentBettypeMapperImp()
        {
            groupBettypes = new Dictionary<int, string>()
            {
                { 1, ContentBetChoice1Imp },
                { 7, ContentBetChoice1Imp },

                { 2, ContentBetChoice2Imp },
                { 12, ContentBetChoice2Imp },
                { 3, ContentBetChoice2Imp },
                { 8, ContentBetChoice2Imp },
                { 20, ContentBetChoice2Imp },
                { 501, ContentBetChoice2Imp },

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
                { 2812, ContentBetChoice2Imp }
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