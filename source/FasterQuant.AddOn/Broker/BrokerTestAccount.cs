using System.Collections.Generic;
using System.Linq;
using FasterQuant.AddOn;

namespace FasterQuant.AddOn
{
    public static class BrokerTestAccount
    {
        public static void Initialize(double accountCash)
        {
            var btam = BrokerTestAccountManager.Instance;
            btam.TradeInfos = new List<TradeInfo>();
            btam.AccountCash = accountCash;
            btam.AccountInitialMargin = 0;
        }

        public static void UpdateBrokerAccountCash(double amount)
        {
            var btam = BrokerTestAccountManager.Instance;
            btam.AccountCash += amount;
        }

        public static void UpdateBrokerAccountInitialMargin(double amount)
        {
            var btam = BrokerTestAccountManager.Instance;
            btam.AccountInitialMargin += amount;
        }

        public static double BrokerAccountCash()
        {
            var btam = BrokerTestAccountManager.Instance;
            return btam.AccountCash;
        }

        public static double BrokerAccountInitialMargin()
        {
            var btam = BrokerTestAccountManager.Instance;
            return btam.AccountInitialMargin;
        }

        public static void AddTradeInfo(string strategyName, int tradeIndex, double initialMargin)
        {
            var btam = BrokerTestAccountManager.Instance;
            btam.TradeInfos.Add(new TradeInfo(strategyName, tradeIndex, initialMargin));
        }

        public static double GetTradeInitialMargin(string strategyName, int tradeIndex)
        {
            var btam = BrokerTestAccountManager.Instance;
            TradeInfo tinfo = btam.TradeInfos.FirstOrDefault(ti => ti.StrategyName == strategyName && ti.TradeIndex == tradeIndex);
            return tinfo != null ? tinfo.InitialMargin : -1;
        }
    }
}
