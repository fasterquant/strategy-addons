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
            positionTrackerManager.MaxPositionCount = new Dictionary<int, int>();
            positionTrackerManager.MaxPositionCountPerSymbol = new Dictionary<int, int>();
        }

        public static void Initialize(int portfolioId)
        {
            var positionTrackerManager = GetPositionInfoTrackerManagerInstance();
            positionTrackerManager.PositionInfos = positionTrackerManager.PositionInfos == null
                                                    ? new List<PositionInfo>()
                                                    : positionTrackerManager.PositionInfos.Where(pi => pi.PortfolioId != portfolioId).ToList();
            positionTrackerManager.MaxPositionCount = new Dictionary<int, int>();
            positionTrackerManager.MaxPositionCountPerSymbol = new Dictionary<int, int>();
        }

        public static void Initialize(int portfolioId, int maxPositionCount, int maxPositionCountPerSymbol)
        {
            var positionTrackerManager = GetPositionInfoTrackerManagerInstance();
            positionTrackerManager.PositionInfos = new List<PositionInfo>();
            positionTrackerManager.MaxPositionCount = new Dictionary<int, int>();
            positionTrackerManager.MaxPositionCountPerSymbol = new Dictionary<int, int>();
            positionTrackerManager.MaxPositionCount[portfolioId] = maxPositionCount;
            positionTrackerManager.MaxPositionCountPerSymbol[portfolioId] = maxPositionCountPerSymbol;
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
            
            PositionInfo positionInfo = null;
            for (int x = 0; x < hExt.PositionInfos.Count; x++)
            {
                if (hExt.PositionInfos[x].PositionId == positionId && hExt.PositionInfos[x].StrategyId == strategyId && hExt.PositionInfos[x].PortfolioId == portfolioId)
                {
                    positionInfo = hExt.PositionInfos[x];
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
            for (int x = 0; x < hExt.PositionInfos.Count; x++)
            {
                if (hExt.PositionInfos[x].PositionId == positionId && hExt.PositionInfos[x].StrategyId == strategyId && hExt.PositionInfos[x].PortfolioId == portfolioId && hExt.PositionInfos[x].PortfolioDateTime == portfolioDateTime)
                {
                    positionInfo = hExt.PositionInfos[x];
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
            for (int x = 0; x < hExt.PositionInfos.Count; x++)
            {
                if (hExt.PositionInfos[x].StrategyId == strategyId && hExt.PositionInfos[x].PortfolioId == portfolioId && hExt.PositionInfos[x].Symbol == symbol && hExt.PositionInfos[x].Type == type && hExt.PositionInfos[x].Status == status)
                {
                    positionInfo = hExt.PositionInfos[x];
                    break;
                }
            }

            if (positionInfo != null)
            {
                positionInfo.PositionId = positionId;

                return;
            }

            hExt.PositionInfos.Add(new PositionInfo(positionId, portfolioId, strategyId, symbol, type, status));
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

        public static double GetSymbolQuantityInOpenLosingPositions(string symbol)
        {
            var hExt = GetPositionInfoTrackerManagerInstance();
            var opis = new List<PositionInfo>();

            for (int x = 0; x < hExt.PositionInfos.Count; x++)
            {
                if (hExt.PositionInfos[x].Symbol == symbol && hExt.PositionInfos[x].PriceChange < 0 && hExt.PositionInfos[x].Status == PositionStatus.Open)
                {
                    opis.Add(hExt.PositionInfos[x]);
                }
            }

            return opis.Sum(pi => pi.Quantity);
        }

        public static int GetOpenLosingPositionCount()
        {
            var hExt = GetPositionInfoTrackerManagerInstance();
            var opis = new List<PositionInfo>();

            for (int x = 0; x < hExt.PositionInfos.Count; x++)
            {
                if (hExt.PositionInfos[x].PriceChange < 0 && hExt.PositionInfos[x].Status == PositionStatus.Open)
                {
                    opis.Add(hExt.PositionInfos[x]);
                }
            }

            return opis.Count;
        }

        public static List<PositionInfo> GetOpenPositionInfos()
        {
            var hExt = GetPositionInfoTrackerManagerInstance();
            var opis = new List<PositionInfo>();

            for (int x = 0; x < hExt.PositionInfos.Count; x++)
            {
                if (hExt.PositionInfos[x].Status == PositionStatus.Open)
                {
                    opis.Add(hExt.PositionInfos[x]);
                }
            }

            return opis;
        }

        public static List<PositionInfo> GetOpenPositionInfos(int portfolioId, int strategyId)
        {
            var hExt = GetPositionInfoTrackerManagerInstance();
            var opis = new List<PositionInfo>();

            for (int x = 0; x < hExt.PositionInfos.Count; x++)
            {
                if (hExt.PositionInfos[x].Status == PositionStatus.Open && hExt.PositionInfos[x].PortfolioId == portfolioId && hExt.PositionInfos[x].StrategyId == strategyId)
                {
                    opis.Add(hExt.PositionInfos[x]);
                }
            }

            return opis;
        }

        public static List<PositionInfo> GetOpenPositionInfos(int portfolioId)
        {
            var hExt = GetPositionInfoTrackerManagerInstance();
            var opis = new List<PositionInfo>();

            for (int x = 0; x < hExt.PositionInfos.Count; x++)
            {
                if (hExt.PositionInfos[x].Status == PositionStatus.Open && hExt.PositionInfos[x].PortfolioId == portfolioId)
                {
                    opis.Add(hExt.PositionInfos[x]);
                }
            }

            return opis;
        }

        public static List<PositionInfo> GetPositionInfos()
        {
            var hExt = GetPositionInfoTrackerManagerInstance();
            return hExt.PositionInfos;
        }


        public static List<PositionInfo> GetPositionInfosByStatusAndSymbol(PositionStatus status, string symbol)
        {
            var hExt = GetPositionInfoTrackerManagerInstance();
            var opis = new List<PositionInfo>();

            for (int x = 0; x < hExt.PositionInfos.Count; x++)
            {
                if (hExt.PositionInfos[x].Status == status && hExt.PositionInfos[x].Symbol == symbol)
                {
                    opis.Add(hExt.PositionInfos[x]);
                }
            }

            return opis;
        }

        public static List<PositionInfo> GetPositionInfosForPositionStrategyPortfolio(int positionId, int strategyId, int portfolioId)
        {
            var hExt = GetPositionInfoTrackerManagerInstance();
            var opis = new List<PositionInfo>();

            for (int x = 0; x < hExt.PositionInfos.Count; x++)
            {
                if (hExt.PositionInfos[x].PositionId == positionId && hExt.PositionInfos[x].PortfolioId == portfolioId && hExt.PositionInfos[x].StrategyId == strategyId)
                {
                    opis.Add(hExt.PositionInfos[x]);
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
