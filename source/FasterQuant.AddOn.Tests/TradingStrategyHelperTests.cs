
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FasterQuant.AddOn;
using Newtonsoft.Json;

namespace FasterQuant.AddOnTests
{
    [TestClass]
    public class TradingStrategyHelperTests
    {
        [TestMethod]
        public void GetTradeIndexFromString_WorksCorrectlyWhenTradeIndexSet()
        {
            var orderComment = new OrderComment(117171, 12, "Some Order Comment");
            var actual = TradingStrategyHelper.GetTradeIndexFromOrderComment(JsonConvert.SerializeObject(orderComment));
            var expected = 12;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetTradeIndexFromString_WorksCorrectlyWhenTradeIndexNotSet()
        {
            var orderComment = new OrderComment("Some Order Comment");
            var actual = TradingStrategyHelper.GetTradeIndexFromOrderComment(JsonConvert.SerializeObject(orderComment));
            var expected = -1;
            Assert.AreEqual(expected, actual);
        }
    }
}
