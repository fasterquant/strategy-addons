
using System.Collections.Generic;

namespace FasterQuant.AddOn
{
    public static class MathHelper
    {
        public static double RoundToNearestTick(double value, double tick)
        {
            var tickCount = (int)(1 / tick);
            var tickInvls = new Dictionary<int, double[]>();
            var valueWoDec = (double)(int)value;
            for (var x = 0; x < tickCount; x++)
            {
                tickInvls[x] = new double[] { tick * x, tick * (x + 1) };
            }
            for (var x = 0; x < tickInvls.Count; x++)
            {
                var bInterval = valueWoDec + tickInvls[x][0];
                var tInterval = valueWoDec + tickInvls[x][1];
                if (value >= bInterval && value <= tInterval)
                {
                    if (value - bInterval >= tInterval - value)
                    {
                        return tInterval;
                    }
                    return bInterval;
                }
            }
            return value;
        }
    }
}
