using System.Collections.Generic;
using System.Linq;

namespace FasterQuant.SimulatedBroker
{
    public class SimulatedBrokerAccount
    {
        public static void Initialize(double accountCash)
        {
            var btam = GetSimulatedBrokerAccountManagerInstance();
            btam.TradeInfos = new List<TradeInfo>();
            btam.AccountCash = accountCash;
            btam.AccountInitialMargin = 0;
        }

        public static void UpdateBrokerAccountCash(double amount)
        {
            var btam = GetSimulatedBrokerAccountManagerInstance();
            btam.AccountCash += amount;
        }

        public static void UpdateBrokerAccountInitialMargin(double amount)
        {
            var btam = GetSimulatedBrokerAccountManagerInstance();
            btam.AccountInitialMargin += amount;
        }

        public static double BrokerAccountCash()
        {
            var btam = GetSimulatedBrokerAccountManagerInstance();
            return btam.AccountCash;
        }

        public static double BrokerAccountInitialMargin()
        {
            var btam = GetSimulatedBrokerAccountManagerInstance();
            return btam.AccountInitialMargin;
        }

        public static void AddTradeInfo(string strategyName, int tradeIndex, double initialMargin)
        {
            var btam = GetSimulatedBrokerAccountManagerInstance();
            btam.TradeInfos.Add(new TradeInfo(strategyName, tradeIndex, initialMargin));
        }

        public static double GetTradeInitialMargin(string strategyName, int tradeIndex)
        {
            var btam = GetSimulatedBrokerAccountManagerInstance();
            TradeInfo tinfo = btam.TradeInfos.FirstOrDefault(ti => ti.StrategyName == strategyName && ti.TradeIndex == tradeIndex);
            return tinfo != null ? tinfo.InitialMargin : -1;
        }

        private static SimulatedBrokerAccountManager GetSimulatedBrokerAccountManagerInstance()
        {
            return SimulatedBrokerAccountManager.Instance;
        }

    }
}
