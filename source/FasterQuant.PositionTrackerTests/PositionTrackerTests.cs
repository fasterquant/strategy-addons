using System;
using System.Collections.Generic;
using System.Linq;
using FasterQuant.PositionTracker;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FasterQuant.HedgeExtensionTests
{
    [TestClass]
    public class PositionTrackerTests
    {
        [TestMethod]
        public void PositionInfoWithBarEndDateTime()
        {
            var currentDate = DateTime.Now;
            PositionInfoTracker.Initialize();
            PositionInfoTracker.AddPositionInfo(1, 33, 34, DateTime.Now, 0, 23, 1, "XYZ", PositionType.Long, 123);
            PositionInfoTracker.AddPositionInfo(1, 38, 39, DateTime.Now, 0, 23, 1, "XYZ", PositionType.Long, 124);
            PositionInfoTracker.AddPositionInfo(2, 22, 27, DateTime.Now, 0, 23, 1, "ABC", PositionType.Long, 123);
            PositionInfoTracker.AddPositionInfo(2, 25, 29, DateTime.Now, 0, 23, 1, "ABC", PositionType.Long, 124);

            PositionInfoTracker.UpdatePositionInfo(2, 25, 30, DateTime.Now, -23, -23, 0, 23, PositionStatus.Open, 124);

            // Should be value in PositionTracker indicator of DataStartDateTime
            var barstartDt = 124;

            // --------------Start PositionTracker indicator code---------------------
            var positionInfos = PositionInfoTracker.GetPositionInfos().Where(pi => pi.PortfolioDateTime <= barstartDt).OrderByDescending(pi => pi.PortfolioDateTime);

            // Perform sums on this collection
            var currentPositionInfos = new List<PositionInfo>();

            var positionInfoKeys = new Dictionary<string, bool>();
            foreach (var pi in positionInfos)
            {
                var key = $"{pi.PositionId}{pi.PortfolioId}{pi.StrategyId}";
                if (!positionInfoKeys.ContainsKey(key))
                {
                    positionInfoKeys[key] = true;
                    currentPositionInfos.Add(pi);
                }
            }

            // --------------End PositionTracker indicator code---------------------
            Assert.AreEqual(currentPositionInfos.Where(pi => pi.PortfolioDateTime == barstartDt).ToList().Count, 2);
        }
    }
}
