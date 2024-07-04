namespace SkelandStore.Core.Entities.Order_Aggregation
{
    public class Order : BaseEntity
    {
        public Order()
        {

        }
        public Order(string buyerEmail, Address shippingAddress, DeleveryMethod deleveryMethod, ICollection<OrderItem> items, decimal subTotal)
        {
            BuyerEmail = buyerEmail;
            ShippingAddress = shippingAddress;
            DeleveryMethod = deleveryMethod;
            Items = items;
            SubTotal = subTotal;
        }

        public string BuyerEmail { get; set; }
        public DateTimeOffset OredrDate { get; set; } = DateTimeOffset.Now;
        public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;
        public Address ShippingAddress { get; set; }
        public DeleveryMethod DeleveryMethod { get; set; }//this relation will be One(DeleveryMethod) To many(Order)
        public ICollection<OrderItem> Items { get; set; } = new HashSet<OrderItem>();
        public decimal SubTotal { get; set; }
        public decimal Total()
        => SubTotal * DeleveryMethod.Cost;
        public string PaymentIntentId { get; set; } = string.Empty;
    }
}
