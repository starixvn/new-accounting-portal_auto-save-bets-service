using Fanex.SaveBets.Builder.Mapper;
using NUnit.Framework;

namespace Fanex.SaveBets.Builder.UnitTests.Mapper
{
    [TestFixture]
    public class BettypeMapperImpTests
    {
        [Test]
        [TestCase(401)]
        [TestCase(402)]
        [TestCase(403)]
        [TestCase(404)]
        [TestCase(411)]
        [TestCase(427)]
        [TestCase(428)]
        [TestCase(432)]
        [TestCase(433)]
        [TestCase(434)]
        [TestCase(435)]
        [TestCase(436)]
        [TestCase(437)]
        [TestCase(438)]
        [TestCase(439)]
        [TestCase(440)]
        [TestCase(441)]
        public void GetNumOfColumnOf_ShouldReturn1(int bettype)
        {
            var mapper = new HeaderBettypeMapperImp();
            var result = mapper.GetHeaderHandicapAndBetTeamOf(bettype);
            var expectedResult = "Header1ColumnTeamImp";

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        [TestCase(405)]
        [TestCase(406)]
        [TestCase(407)]
        [TestCase(408)]
        [TestCase(409)]
        [TestCase(410)]
        [TestCase(412)]
        [TestCase(413)]
        [TestCase(414)]
        [TestCase(415)]
        [TestCase(416)]
        [TestCase(431)]
        [TestCase(442)]
        [TestCase(443)]
        [TestCase(444)]
        public void GetNumOfColumnOf_ShouldReturn3(int bettype)
        {
            var mapper = new HeaderBettypeMapperImp();
            var result = mapper.GetHeaderHandicapAndBetTeamOf(bettype);
            var expectedResult = "Header3ColumnImp";

            Assert.AreEqual(expectedResult, result);
        }
    }
}