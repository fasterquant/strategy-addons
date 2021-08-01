using System.Collections.Generic;
using System.Collections.Concurrent;

namespace FasterQuant.PositionTracker
{
    internal sealed class PositionInfoTrackerManager
    {

        internal Dictionary<int, ConcurrentQueue<PositionInfo>> PositionInfos;
        internal Dictionary <int, int> MaxPositionCount;
        internal Dictionary<int, int> MaxPositionCountPerSymbol;

        private static readonly PositionInfoTrackerManager instance = new PositionInfoTrackerManager();

        static PositionInfoTrackerManager() { }

        private PositionInfoTrackerManager()
        {
            this.PositionInfos = new Dictionary<int, ConcurrentQueue<PositionInfo>>();
            this.MaxPositionCount = new Dictionary<int, int>();
            this.MaxPositionCountPerSymbol = new Dictionary<int, int>();
        }

        public static PositionInfoTrackerManager Instance
        {
            get
            {
                return instance;
            }
        }
    }
}
