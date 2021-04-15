
using System.Collections.Generic;
using FasterQuant.AddOn;

namespace FasterQuant.AddOn
{
    internal sealed class BrokerTestAccountManager
    {
        internal double AccountCash;
        internal double AccountInitialMargin;
        internal List<TradeInfo> TradeInfos;

        private static readonly BrokerTestAccountManager instance = new BrokerTestAccountManager();

        static BrokerTestAccountManager() { }

        private BrokerTestAccountManager() { }

        public static BrokerTestAccountManager Instance
        {
            get
            {
                return instance;
            }
        }
    }
}
