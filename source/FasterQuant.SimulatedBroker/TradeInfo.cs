
namespace FasterQuant.SimulatedBroker
{
    internal class TradeInfo
    {
        internal string StrategyName;
        internal int TradeIndex;
        internal double InitialMargin;

        internal TradeInfo(string strategyName, int tradeIndex, double initialMargin)
        {
            this.StrategyName = strategyName;
            this.TradeIndex = tradeIndex;
            this.InitialMargin = initialMargin;
        }
    }
}
