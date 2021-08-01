using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;


namespace FasterQuant.PositionTracker
{
    public class PositionInfoTracker
    {
        public static void Initialize(int portfolioId)
        {
            var positionTrackerManager = GetPositionInfoTrackerManagerInstance();
            positionTrackerManager.PositionInfos[portfolioId] = new ConcurrentQueue<PositionInfo>();
        }

        public static void Initialize(int portfolioId, int maxPositionCount, int maxPositionCountPerSymbol)
        {
            var positionTrackerManager = GetPositionInfoTrackerManagerInstance();
            positionTrackerManager.PositionInfos[portfolioId] = new ConcurrentQueue<PositionInfo>();
            positionTrackerManager.MaxPositionCount[portfolioId] = maxPositionCount;
            positionTrackerManager.MaxPositionCountPerSymbol[portfolioId] = maxPositionCountPerSymbol;
        }

        public static void AddPositionInfo(int positionId, double averageEntryPrice, double currentPrice, DateTime entryDateTime, int portfolioId, double quantity, int strategyId,
            string symbol, PositionType type)
        {
            var positionTrackerManager = GetPositionInfoTrackerManagerInstance();
            positionTrackerManager.PositionInfos[portfolioId].Enqueue(new PositionInfo(positionId, averageEntryPrice, currentPrice, entryDateTime, portfolioId, quantity, strategyId, symbol, type));
        }

        public static void AddPositionInfo(int positionId, double averageEntryPrice, double currentPrice, DateTime entryDateTime, int portfolioId, double quantity, int strategyId,
            string symbol, PositionType type, long portfolioDateTime)
        {
            var positionTrackerManager = GetPositionInfoTrackerManagerInstance();
            positionTrackerManager.PositionInfos[portfolioId].Enqueue(new PositionInfo(positionId, averageEntryPrice, currentPrice, entryDateTime, portfolioId, quantity, strategyId, symbol, type, portfolioDateTime));
        }

        public static void AddPositionInfo(int positionId, double averageEntryPrice, double currentPrice, DateTime entryDateTime, DateTime exitDateTime, double priceChange, double profitLoss, int portfolioId, double quantity, int strategyId,
           string symbol, PositionType type, long portfolioDateTime)
        {
            var positionTrackerManager = GetPositionInfoTrackerManagerInstance();
            positionTrackerManager.PositionInfos[portfolioId].Enqueue(new PositionInfo(positionId, averageEntryPrice, currentPrice, entryDateTime, exitDateTime, priceChange, profitLoss,  portfolioId, quantity, strategyId, symbol, type, portfolioDateTime));
        }

        public static void AddPositionInfo(int positionId, double averageEntryPrice, double currentPrice, DateTime entryDateTime, double priceChange, double profitLoss, int portfolioId, double quantity, int strategyId,
           string symbol, PositionType type, long portfolioDateTime)
        {
            var positionTrackerManager = GetPositionInfoTrackerManagerInstance();
            positionTrackerManager.PositionInfos[portfolioId].Enqueue(new PositionInfo(positionId, averageEntryPrice, currentPrice, entryDateTime, priceChange, profitLoss, portfolioId, quantity, strategyId, symbol, type, portfolioDateTime));
        }

        public static void UpdatePositionInfo(int positionId, double averageEntryPrice, double currentPrice, DateTime exitDateTime, double priceChange, double profitLoss, int portfolioId, double quantity,
            PositionStatus status, int strategyId)
        {
            var hExt = GetPositionInfoTrackerManagerInstance();
            
            PositionInfo positionInfo = null;

            foreach (var pi in hExt.PositionInfos[portfolioId])
            {
                if (pi.PositionId == positionId && pi.StrategyId == strategyId && pi.PortfolioId == portfolioId)
                {
                    positionInfo = pi;
                    break;
                }
            }

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
           
            PositionInfo positionInfo = null;
           
            foreach (var pi in hExt.PositionInfos[portfolioId])
            {
                if (pi.PositionId == positionId && pi.StrategyId == strategyId && pi.PortfolioId == portfolioId && pi.PortfolioDateTime == portfolioDateTime)
                {
                    positionInfo = pi;
                    break;
                }
            }

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

        public static void UpsertPositionInfo(int positionId, int portfolioId, int strategyId, string symbol, PositionType type, PositionStatus status)
        {
            var hExt = GetPositionInfoTrackerManagerInstance();
          
            PositionInfo positionInfo = null;
           
            foreach (var pi in hExt.PositionInfos[portfolioId])
            {
                if (pi.StrategyId == strategyId && pi.PortfolioId == portfolioId && pi.Symbol == symbol && pi.Type == type && pi.Status == status)
                {
                    positionInfo = pi;
                    break;
                }
            }

            if (positionInfo != null)
            {
                positionInfo.PositionId = positionId;

                return;
            }

            hExt.PositionInfos[portfolioId].Enqueue(new PositionInfo(positionId, portfolioId, strategyId, symbol, type, status));
        }

        public static void SetPositionInfoToClosed(PositionInfo positionInfo)
        {
            positionInfo.Status = PositionStatus.Closed;
        }

        public static int GetMaxPositionCount(int portfolioId)
        {
            var hExt = GetPositionInfoTrackerManagerInstance();
            return hExt.MaxPositionCount.ContainsKey(portfolioId) ? hExt.MaxPositionCount[portfolioId] : 0;
        }

        public static int GetMaxPositionCountPerSymbol(int portfolioId)
        {
            var hExt = GetPositionInfoTrackerManagerInstance();
            return hExt.MaxPositionCountPerSymbol.ContainsKey(portfolioId) ? hExt.MaxPositionCountPerSymbol[portfolioId] : 0;
        }

        public static double GetSymbolQuantityInOpenLosingPositions(string symbol, int portfolioId)
        {
            var hExt = GetPositionInfoTrackerManagerInstance();
            var opis = new List<PositionInfo>();
            
            foreach (var pi in hExt.PositionInfos[portfolioId])
            {
                if (pi.Symbol == symbol && pi.PriceChange < 0 && pi.Status == PositionStatus.Open)
                {
                    opis.Add(pi);
                }
            }

            return opis.Sum(pi => pi.Quantity);
        }

        public static int GetOpenLosingPositionCount(int portfolioId)
        {
            var hExt = GetPositionInfoTrackerManagerInstance();
            var opis = new List<PositionInfo>();

            foreach (var pi in hExt.PositionInfos[portfolioId])
            {
                if (pi.PriceChange < 0 && pi.Status == PositionStatus.Open)
                {
                    opis.Add(pi);
                }
            }

            return opis.Count;
        }

        public static List<PositionInfo> GetOpenPositionInfos(int portfolioId)
        {
            var hExt = GetPositionInfoTrackerManagerInstance();
            var opis = new List<PositionInfo>();

            foreach (var pi in hExt.PositionInfos[portfolioId])
            {
                if (pi.Status == PositionStatus.Open)
                {
                    opis.Add(pi);
                }
            }

            return opis;
        }

        public static List<PositionInfo> GetOpenPositionInfos(int portfolioId, int strategyId)
        {
            var hExt = GetPositionInfoTrackerManagerInstance();
            var opis = new List<PositionInfo>();

            foreach (var pi in hExt.PositionInfos[portfolioId])
            {
                if (pi.Status == PositionStatus.Open && pi.PortfolioId == portfolioId && pi.StrategyId == strategyId)
                {
                    opis.Add(pi);
                }
            }

            return opis;
        }

        public static List<PositionInfo> GetPositionInfos(int portfolioId)
        {
            var hExt = GetPositionInfoTrackerManagerInstance();

            return hExt.PositionInfos[portfolioId].ToList<PositionInfo>();
        }


        public static List<PositionInfo> GetPositionInfosByStatusAndSymbol(PositionStatus status, string symbol, int portfolioId)
        {
            var hExt = GetPositionInfoTrackerManagerInstance();
            var opis = new List<PositionInfo>();

            foreach (var pi in hExt.PositionInfos[portfolioId])
            {
                if (pi.Status == status && pi.Symbol == symbol)
                {
                    opis.Add(pi);
                }
            }

            return opis;
        }

        public static List<PositionInfo> GetPositionInfosForPositionStrategyPortfolio(int positionId, int strategyId, int portfolioId)
        {
            var hExt = GetPositionInfoTrackerManagerInstance();
            var opis = new List<PositionInfo>();

            foreach (var pi in hExt.PositionInfos[portfolioId])
            {
                if (pi.PositionId == positionId && pi.PortfolioId == portfolioId && pi.StrategyId == strategyId)
                {
                    opis.Add(pi);
                }
            }

            return opis;
        }

        private static PositionInfoTrackerManager GetPositionInfoTrackerManagerInstance()
        {
            return PositionInfoTrackerManager.Instance;
        }
    }
}
