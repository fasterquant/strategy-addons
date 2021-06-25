using System;

namespace FasterQuant.PositionTracker
{
    public class PositionInfo
    {
        public int PositionId { get; internal set; }
        public double AverageEntryPrice { get; internal set; }
        public long PortfolioDateTime { get; }
        public double CurrentPrice { get; internal set; }
        public DateTime EntryDateTime { get; }
        public DateTime ExitDateTime { get; internal set; }
        public int PortfolioId { get; internal set; }
        public double PriceChange { get; internal set; }
        public double ProfitLoss { get; internal set; }
        public double Quantity { get; internal set; }
        public PositionStatus Status { get; internal set; }
        public int StrategyId { get; }
        public string Symbol { get; }
        public PositionType Type { get; }

        public PositionInfo(int positionId, double averageEntryPrice, double currentPrice, DateTime entryDateTime, int portfolioId, double quantity, int strategyId, string symbol, PositionType type)
        {
            PositionId = positionId;
            CurrentPrice = currentPrice;
            AverageEntryPrice = averageEntryPrice;
            EntryDateTime = entryDateTime;
            Quantity = quantity;
            PortfolioId = portfolioId;
            StrategyId = strategyId;
            Symbol = symbol;
            Status = PositionStatus.Open;
            Type = type;
        }

        public PositionInfo(int positionId, double averageEntryPrice, double currentPrice, DateTime entryDateTime, int portfolioId, double quantity, int strategyId, string symbol, PositionType type, long barStartDateTime)
        {
            PositionId = positionId;
            CurrentPrice = currentPrice;
            AverageEntryPrice = averageEntryPrice;
            EntryDateTime = entryDateTime;
            Quantity = quantity;
            PortfolioId = portfolioId;
            StrategyId = strategyId;
            Symbol = symbol;
            Status = PositionStatus.Open;
            Type = type;
            PortfolioDateTime = barStartDateTime;
        }

        public PositionInfo(int positionId, double averageEntryPrice, double currentPrice, DateTime entryDateTime, double priceChange, double profitLoss, int portfolioId, double quantity, int strategyId, string symbol, PositionType type, long barStartDateTime)
        {
            PositionId = positionId;
            CurrentPrice = currentPrice;
            AverageEntryPrice = averageEntryPrice;
            EntryDateTime = entryDateTime;
            Quantity = quantity;
            PriceChange = priceChange;
            ProfitLoss = profitLoss;
            PortfolioId = portfolioId;
            StrategyId = strategyId;
            Symbol = symbol;
            Status = PositionStatus.Open;
            Type = type;
            PortfolioDateTime = barStartDateTime;
        }

        public PositionInfo(int positionId, double averageEntryPrice, double currentPrice, DateTime entryDateTime, DateTime exitDateTime, double priceChange, double profitLoss, int portfolioId, double quantity, int strategyId, string symbol, PositionType type, long barStartDateTime)
        {
            PositionId = positionId;
            CurrentPrice = currentPrice;
            AverageEntryPrice = averageEntryPrice;
            EntryDateTime = entryDateTime;
            ExitDateTime = exitDateTime;
            Quantity = quantity;
            PriceChange = priceChange;
            ProfitLoss = profitLoss;
            PortfolioId = portfolioId;
            StrategyId = strategyId;
            Symbol = symbol;
            Status = PositionStatus.Closed;
            Type = type;
            PortfolioDateTime = barStartDateTime;
        }

        public PositionInfo(int positionId, int strategyId, string symbol, PositionType type, PositionStatus status)
        {
            PositionId = positionId;
            StrategyId = strategyId;
            Symbol = symbol;
            Type = type;
            Status = status;
        }
    }
}
