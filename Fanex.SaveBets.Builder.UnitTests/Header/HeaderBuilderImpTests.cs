using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fanex.SaveBets.Builder.Header;
using NUnit.Framework;

namespace Fanex.SaveBets.Builder.UnitTests.Header
{
    [TestFixture]
    public class HeaderBuilderImpTests
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
        public void GetHeaderByBettype_ShouldReturn1Column(int bettype)
        {
            HeaderBuilder builder = new HeaderBuilderImp();
            var result = builder.GetHeaderByBettype(bettype);
            var expectedResult = new List<string> { "TEAM" };

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
        public void GetHeaderByBettype_ShouldReturn3Column(int bettype)
        {
            HeaderBuilder builder = new HeaderBuilderImp();
            var result = builder.GetHeaderByBettype(bettype);
            var expectedResult = new List<string> { "TEAM", "HDP1", "HDP2" };                

            Assert.AreEqual(expectedResult, result);
        }
    }
}
