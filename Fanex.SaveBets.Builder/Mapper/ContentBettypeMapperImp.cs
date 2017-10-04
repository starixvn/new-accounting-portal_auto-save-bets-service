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
        private const string ContentBetChoice408Imp = "ContentBetChoice408Imp";
        private const string ContentBetChoice409Imp = "ContentBetChoice409Imp";
        private const string ContentBetChoice410Imp = "ContentBetChoice410Imp";
        private const string ContentBetChoice411Imp = "ContentBetChoice411Imp";
        private const string ContentBetChoice412Imp = "ContentBetChoice412Imp";
        private const string ContentBetChoice413Imp = "ContentBetChoice413Imp";
        private const string ContentBetChoice415Imp = "ContentBetChoice415Imp";
        private const string ContentBetChoice416Imp = "ContentBetChoice416Imp";
        private const string ContentBetChoice427Imp = "ContentBetChoice427Imp";
        private const string ContentBetChoice431Imp = "ContentBetChoice431Imp";
        private const string ContentBetChoice432Imp = "ContentBetChoice432Imp";
        private const string ContentBetChoice433Imp = "ContentBetChoice433Imp";
        private const string ContentBetChoice442Imp = "ContentBetChoice442Imp";
        private const string ContentBetChoice417Imp = "ContentBetChoice417Imp";
        private const string ContentBetChoice424Imp = "ContentBetChoice424Imp";
        private const string ContentBetChoice425Imp = "ContentBetChoice425Imp";
        private const string ContentBetChoice426Imp = "ContentBetChoice426Imp";
        private const string ContentBetChoice221Imp = "ContentBetChoice221Imp";
        private const string ContentBetChoice222Imp = "ContentBetChoice222Imp";
        private const string ContentBetChoice223Imp = "ContentBetChoice223Imp";
        private const string ContentBetChoice224Imp = "ContentBetChoice224Imp";
        private const string ContentBetChoice225Imp = "ContentBetChoice225Imp";
        private const string ContentBetChoice601_602_608Imp = "ContentBetChoice601_602_608Imp";
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
        private const string ContentBetChoice2703_2704Imp = "ContentBetChoice2703_2704Imp";
        private const string ContentBetChoice2705_2706Imp = "ContentBetChoice2705_2706Imp";

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
                { 408, ContentBetChoice408Imp },
                { 171, ContentBetChoice408Imp },
                { 409, ContentBetChoice409Imp },
                { 410, ContentBetChoice410Imp },
                { 411, ContentBetChoice411Imp },
                { 412, ContentBetChoice412Imp },

                { 413, ContentBetChoice413Imp },
                { 414, ContentBetChoice413Imp },

                { 415, ContentBetChoice415Imp },
                { 416, ContentBetChoice416Imp },

                { 427, ContentBetChoice427Imp },
                { 428, ContentBetChoice427Imp },

                { 431, ContentBetChoice431Imp },
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

                { 426, ContentBetChoice426Imp },
                { 429, ContentBetChoice426Imp },
                { 445, ContentBetChoice426Imp },
                { 446, ContentBetChoice426Imp },
                { 447, ContentBetChoice426Imp },

                { 221, ContentBetChoice221Imp },
                { 222, ContentBetChoice222Imp },
                { 223, ContentBetChoice223Imp },
                { 224, ContentBetChoice224Imp },
                { 225, ContentBetChoice225Imp },

                { 601, ContentBetChoice601_602_608Imp },
                { 602, ContentBetChoice601_602_608Imp },
                { 608, ContentBetChoice601_602_608Imp },

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
                { 615, ContentBetChoice615Imp },
                { 616, ContentBetChoice616Imp },
                { 617, ContentBetChoice617Imp },

                { 614, ContentBetChoice614Imp },

                { 2703, ContentBetChoice2703_2704Imp },
                { 2704, ContentBetChoice2703_2704Imp },

                { 2705, ContentBetChoice2705_2706Imp },
                { 2706, ContentBetChoice2705_2706Imp },
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