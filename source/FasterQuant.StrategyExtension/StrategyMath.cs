using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FasterQuant.StrategyExtension
{
    public static class StrategyMath
    {
        public static double CalculatePointValueByMultiplier(double value, double multiplier)
        {
            return value * multiplier;
        }

        public static double CalculateMarginRequirement(double price, double pointValue, double marginPercent, int quantity)
        {
            return pointValue * (price * quantity) * (marginPercent / 100);
        }

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

        public static double CalculatePositionSizeForMaxAccountAllocationPercent(double accountCash, double accountAvailableFunds, double costPerUnit, double maxAllocationPercentPerTrade)
        {
            var allocationAmountForTrade = CalculateAllocationAmountForPosition(accountAvailableFunds, accountCash, maxAllocationPercentPerTrade);
            if (costPerUnit > allocationAmountForTrade)
            {
                return 0;
            }

            return (int)(allocationAmountForTrade / costPerUnit);
        }

        public static double CalculatePositionSizeForMaxPercentRisk(double accountCash, double accountAvailableFunds, double costPerUnit, double percentRiskPerTrade, double pointValue, double stopPoints)
        {
            var allocationAmountForTrade = CalculateAllocationAmountForPosition(accountAvailableFunds, accountCash, costPerUnit, percentRiskPerTrade, pointValue, stopPoints);
            if (costPerUnit > allocationAmountForTrade)
            {
                return 0;
            }

            return (int)(allocationAmountForTrade / costPerUnit);
        }

        private static double CalculateAllocationAmountForPosition(double accountAvailableFunds, double accountCash, double costPerUnit, double percentRiskPerTrade, double pointValue, double stopPoints)
        {
            var maxAmountToRisk = ((percentRiskPerTrade / 100) * accountCash);
            var stopDollarAmtPerContract = pointValue * stopPoints;
            var maxTradeAllocation = (maxAmountToRisk / stopDollarAmtPerContract) * costPerUnit;
            return accountAvailableFunds < maxTradeAllocation ? accountAvailableFunds : maxTradeAllocation;
        }

        private static double CalculateAllocationAmountForPosition(double accountAvailableFunds, double accountCash, double maxAllocationPercentPerTrade)
        {
            var maxTradeAllocation = ((maxAllocationPercentPerTrade / 100) * accountCash);
            return accountAvailableFunds < maxTradeAllocation ? accountAvailableFunds : maxTradeAllocation;
        }
    }
}
