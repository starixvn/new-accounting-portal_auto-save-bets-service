using System.Linq;
using Fanex.SaveBets.Builder.Header;
using NUnit.Framework;

namespace Fanex.SaveBets.Builder.UnitTests.Header
{
    [TestFixture]
    public class Header1and3ColumnImpTests
    {
        [Test]
        public void BuildHeader_1Column_ShouldReturnHeaderTextIsTEAM()
        {
            var header = new Header1ColumnTeamImp();

            var headerText = header.BuildHeader().FirstOrDefault();
            var expectedResult = "TEAM";

            Assert.AreEqual(headerText, expectedResult);
        }

        [Test]
        public void BuildHeader_3Column_ShouldReturnHeaderTextIsTEAMWithFirstColumn()
        {
            var header = new Header3ColumnImp();

            var headerText = header.BuildHeader();
            var expectedResult = "TEAM";

            Assert.AreEqual(expectedResult, headerText[0]);
        }

        [Test]
        public void BuildHeader_3Column_ShouldReturnHeaderTextIsHDP1WithSecindColumn()
        {
            var header = new Header3ColumnImp();

            var headerText = header.BuildHeader();
            var expectedResult = "HDP1";

            Assert.AreEqual(expectedResult, headerText[1]);
        }

        [Test]
        public void BuildHeader_3Column_ShouldReturnHeaderTextIsHDP2WithSecindColumn()
        {
            var header = new Header3ColumnImp();

            var headerText = header.BuildHeader();
            var expectedResult = "HDP2";

            Assert.AreEqual(expectedResult, headerText[2]);
        }
    }
}