using Newtonsoft.Json;

namespace FasterQuant.AddOn
{
    public class OrderComment
    {
        public long AssociatedTradeId;
        public int AssociatedTradeIndex;
        public string Comment;

        [JsonConstructor]
        public OrderComment(long associatedTradeId, int associatedTradeIndex, string comment)
        {
            this.AssociatedTradeId = associatedTradeId;
            this.AssociatedTradeIndex = associatedTradeIndex;
            this.Comment = comment;
        }

        public OrderComment(string comment)
        {
            this.AssociatedTradeId = -1;
            this.AssociatedTradeIndex = -1;
            this.Comment = comment;
        }
    }
}
