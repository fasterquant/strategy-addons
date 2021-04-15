using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FasterQuant.SimulatedBroker
{
    internal sealed class SimulatedBrokerAccountManager
    {
        internal double AccountCash;
        internal double AccountInitialMargin;
        internal List<TradeInfo> TradeInfos;

        private static readonly SimulatedBrokerAccountManager instance = new SimulatedBrokerAccountManager();

        static SimulatedBrokerAccountManager() { }

        private SimulatedBrokerAccountManager() { }

        public static SimulatedBrokerAccountManager Instance
        {
            get
            {
                return instance;
            }
        }
    }
}
