using System;
using System.Collections.Generic;
using System.Linq;


namespace FasterQuant.PositionTracker
{
    public class PositionInfoTracker
    {
        public static void Initialize()
        {
            var positionTrackerManager = GetPositionInfoTrackerManagerInstance();
            positionTrackerManager.PositionInfos = new List<PositionInfo>();
        }

        public static void Initialize(int portfolioId)
        {
            var positionTrackerManager = GetPositionInfoTrackerManagerInstance();
            positionTrackerManager.PositionInfos = positionTrackerManager.PositionInfos == null
                                                    ? new List<PositionInfo>()
                                                    : positionTrackerManager.PositionInfos.Where(pi => pi.PortfolioId != portfolioId).ToList();
        }

        public static void Initialize(int maxPositionCount, int maxPositionCountPerSymbol)
        {
            var positionTrackerManager = GetPositionInfoTrackerManagerInstance();
            positionTrackerManager.PositionInfos = new List<PositionInfo>();
            positionTrackerManager.MaxPositionCount = maxPositionCount;
            positionTrackerManager.MaxPositionCountPerSymbol = maxPositionCountPerSymbol;
        }

        public static void AddPositionInfo(int positionId, double averageEntryPrice, double currentPrice, DateTime entryDateTime, int portfolioId, double quantity, int strategyId,
            string symbol, PositionType type)
        {
            var positionTrackerManager = GetPositionInfoTrackerManagerInstance();
            positionTrackerManager.PositionInfos.Add(new PositionInfo(positionId, averageEntryPrice, currentPrice, entryDateTime, portfolioId, quantity, strategyId, symbol, type));
        }

        public static void AddPositionInfo(int positionId, double averageEntryPrice, double currentPrice, DateTime entryDateTime, int portfolioId, double quantity, int strategyId,
            string symbol, PositionType type, long portfolioDateTime)
        {
            var positionTrackerManager = GetPositionInfoTrackerManagerInstance();
            positionTrackerManager.PositionInfos.Add(new PositionInfo(positionId, averageEntryPrice, currentPrice, entryDateTime, portfolioId, quantity, strategyId, symbol, type, portfolioDateTime));
        }

        public static void AddPositionInfo(int positionId, double averageEntryPrice, double currentPrice, DateTime entryDateTime, DateTime exitDateTime, double priceChange, double profitLoss, int portfolioId, double quantity, int strategyId,
           string symbol, PositionType type, long portfolioDateTime)
        {
            var positionTrackerManager = GetPositionInfoTrackerManagerInstance();
            positionTrackerManager.PositionInfos.Add(new PositionInfo(positionId, averageEntryPrice, currentPrice, entryDateTime, exitDateTime, priceChange, profitLoss,  portfolioId, quantity, strategyId, symbol, type, portfolioDateTime));
        }

        public static void AddPositionInfo(int positionId, double averageEntryPrice, double currentPrice, DateTime entryDateTime, double priceChange, double profitLoss, int portfolioId, double quantity, int strategyId,
           string symbol, PositionType type, long portfolioDateTime)
        {
            var positionTrackerManager = GetPositionInfoTrackerManagerInstance();
            positionTrackerManager.PositionInfos.Add(new PositionInfo(positionId, averageEntryPrice, currentPrice, entryDateTime, priceChange, profitLoss, portfolioId, quantity, strategyId, symbol, type, portfolioDateTime));
        }

        public static void UpdatePositionInfo(int positionId, double averageEntryPrice, double currentPrice, DateTime exitDateTime, double priceChange, double profitLoss, int portfolioId, double quantity,
            PositionStatus status, int strategyId)
        {
            var hExt = GetPositionInfoTrackerManagerInstance();
            var positionInfo = hExt.PositionInfos.FirstOrDefault(pi =>
                pi.PositionId == positionId && pi.StrategyId == strategyId && pi.PortfolioId == portfolioId);

            if (positionInfo == null)
            {
                return;
            }

            positionInfo.AverageEntryPrice = averageEntryPrice;
            positionInfo.CurrentPrice = currentPrice;
            positionInfo.ExitDateTime = exitDateTime;
            positionInfo.PriceChange = priceChange;
            positionInfo.ProfitLoss = profitLoss;
            positionInfo.Quantity = quantity;
            positionInfo.Status = status;
        }

        public static void UpdatePositionInfo(int positionId, double averageEntryPrice, double currentPrice, DateTime exitDateTime, double priceChange, double profitLoss, int portfolioId, double quantity,
            PositionStatus status, int strategyId, long portfolioDateTime)
        {
            var hExt = GetPositionInfoTrackerManagerInstance();
            var positionInfo = hExt.PositionInfos.FirstOrDefault(pi =>
                pi.PositionId == positionId && pi.StrategyId == strategyId && pi.PortfolioId == portfolioId && pi.PortfolioDateTime == portfolioDateTime);

            if (positionInfo == null)
            {
                return;
            }

            positionInfo.AverageEntryPrice = averageEntryPrice;
            positionInfo.CurrentPrice = currentPrice;
            positionInfo.ExitDateTime = exitDateTime;
            positionInfo.PriceChange = priceChange;
            positionInfo.ProfitLoss = profitLoss;
            positionInfo.Quantity = quantity;
            positionInfo.Status = status;
        }

        public static void UpsertPositionInfo(int positionId, int strategyId, string symbol, PositionType type, PositionStatus status)
        {
            var hExt = GetPositionInfoTrackerManagerInstance();
            var positionInfo = hExt.PositionInfos.FirstOrDefault(pi =>
                pi.StrategyId == strategyId && pi.Symbol == symbol && pi.Type == type && pi.Status == status);

            if (positionInfo != null)
            {
                positionInfo.PositionId = positionId;
                positionInfo.Status = status;

                return;
            }

            hExt.PositionInfos.Add(new PositionInfo(positionId, strategyId, symbol, type, status));
        }

        public static void UpdatePositionInfo(int positionId, int strategyId, PositionStatus status)
        {
            var hExt = GetPositionInfoTrackerManagerInstance();
            var positionInfo = hExt.PositionInfos.FirstOrDefault(pi =>
                pi.StrategyId == strategyId && pi.PositionId == positionId);

            if (positionInfo != null)
            {
                positionInfo.Status = status;
            }
        }

        public static int GetMaxPositionCount()
        {
            var hExt = GetPositionInfoTrackerManagerInstance();
            return hExt.MaxPositionCount;
        }

        public static int GetMaxPositionCountPerSymbol()
        {
            var hExt = GetPositionInfoTrackerManagerInstance();
            return hExt.MaxPositionCountPerSymbol;
        }

        public static double GetSymbolQuantityInOpenLosingPositions(string symbol)
        {
            var hExt = GetPositionInfoTrackerManagerInstance();
            return hExt.PositionInfos.Where(pi => pi.Symbol == symbol && pi.PriceChange < 0 && pi.Status == PositionStatus.Open)
                .Sum(pi => pi.Quantity);

        }

        public static int GetOpenLosingPositionCount()
        {
            var hExt = GetPositionInfoTrackerManagerInstance();
            return hExt.PositionInfos.Where(pi => pi.PriceChange < 0 && pi.Status == PositionStatus.Open)
                .Count();

        }

        public static List<PositionInfo> GetOpenPositionInfos()
        {
            var hExt = GetPositionInfoTrackerManagerInstance();
            return hExt.PositionInfos.Where(pi => pi.Status == PositionStatus.Open).ToList();

        }

        public static List<PositionInfo> GetPositionInfos()
        {
            var hExt = GetPositionInfoTrackerManagerInstance();
            return hExt.PositionInfos;

        }


        public static List<PositionInfo> GetPositionInfosByStatusAndSymbol(PositionStatus status, string symbol)
        {
            var hExt = GetPositionInfoTrackerManagerInstance();
            return hExt.PositionInfos.Where(pi => pi.Symbol == symbol && pi.Status == status).ToList();

        }

        public static List<PositionInfo> GetPositionInfosForPositionStrategyPortfolio(int positionId, int strategyId, int portfolioId)
        {
            var hExt = GetPositionInfoTrackerManagerInstance();
            return hExt.PositionInfos.Where(pi => pi.PositionId == positionId && pi.StrategyId == strategyId && pi.PortfolioId == portfolioId).ToList();
        }

        private static PositionInfoTrackerManager GetPositionInfoTrackerManagerInstance()
        {
            return PositionInfoTrackerManager.Instance;
        }
    }
}
