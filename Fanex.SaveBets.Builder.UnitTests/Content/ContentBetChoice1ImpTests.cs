using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fanex.SaveBets.Builder.Content;
using Fanex.SaveBets.Builder.UnitTests.Helper;
using NSubstitute;
using NUnit.Framework;

namespace Fanex.SaveBets.Builder.UnitTests.Content
{
    [TestFixture]
    public class ContentBetChoice1ImpTests
    {
        [Test]
        public void ContentBetChoice1Imp_WithBetteamIsHome_ShouldReturnValueByformulaHdp2MinusHdp1()
        {
            List<Dictionary<string, object>> itemsList = new List<Dictionary<string, object>>();
            var items = new Dictionary<string, object>()
            {
                { "betteam", "h" },
                { "hdp1", 0.125 },
                { "hdp2", 0 }
            };
            itemsList.Add(items);
            IDataReader reader = new MockDataReader(itemsList);

            ContentBetChoice choice = new ContentBetChoice1Imp();
            var result = choice.ContentBetChoiceBuilder(reader);
            //var expectedResult = 
        }
    }
}
