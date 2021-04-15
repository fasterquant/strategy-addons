using System;

namespace FasterQuant.SimulatedBroker
{
    public class PendingOrder
    {
        public DateTime EntryDateTime { get; }
        public int StrategyNumber { get; }
        public int OrderIndex { get; }

        public PendingOrder(DateTime entryDateTime, int orderIndex, int strategyNumber)
        {
            this.EntryDateTime = entryDateTime;
            this.OrderIndex = orderIndex;
            this.StrategyNumber = strategyNumber;
        }
    }
}
