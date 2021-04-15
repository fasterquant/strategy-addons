using System;
using FasterQuant.AddOn;
using Newtonsoft.Json;

namespace FasterQuant.AddOn
{
    public static class TradingStrategyHelper
    {
        public static double GetPointValueByMultiplier(double value, double multiplier)
        {
            return value * multiplier;
        }

        public static double GetMarginRequirement(double price, double pointValue, double marginPercent, int quantity)
        {
            return pointValue * (price * quantity) * (marginPercent / 100);
        }

        public static double GetTradeQuantityForMaxPercentRisk(double accountCash, double accountAvailableFunds, double costPerUnit, double percentRiskPerTrade, double pointValue, double stopPoints)
        {
            var allocationAmountForTrade = getAllocationAmountForTrade(accountAvailableFunds, accountCash, costPerUnit, percentRiskPerTrade, pointValue, stopPoints);
            if (costPerUnit > allocationAmountForTrade)
            {
                return 0;
            }
            return (int)(allocationAmountForTrade / costPerUnit);
        }

        public static int GetTradeIndexFromOrderComment(string orderComment)
        {
            if (string.IsNullOrEmpty(orderComment)) {
                return -1;
            }
            var oc = JsonConvert.DeserializeObject<OrderComment>(orderComment);
            return oc.AssociatedTradeIndex;

        }

        private static double getAllocationAmountForTrade(double accountAvailableFunds, double accountCash, double costPerUnit, double percentRiskPerTrade, double pointValue, double stopPoints)
        {
            var maxAmountToRisk = ((percentRiskPerTrade / 100) * accountCash);
            var stopDollarAmtPerContract = pointValue * stopPoints;
            var maxTradeAllocation = (maxAmountToRisk / stopDollarAmtPerContract) * costPerUnit;
            return accountAvailableFunds < maxTradeAllocation ? accountAvailableFunds : maxTradeAllocation;
        }


    }
}
