using System.Collections.Generic;

namespace FasterQuant.PositionTracker
{
    internal sealed class PositionInfoTrackerManager
    {

        internal List<PositionInfo> PositionInfos;

        private static readonly PositionInfoTrackerManager instance = new PositionInfoTrackerManager();

        static PositionInfoTrackerManager() { }

        private PositionInfoTrackerManager()
        {
            this.PositionInfos = new List<PositionInfo>();
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
