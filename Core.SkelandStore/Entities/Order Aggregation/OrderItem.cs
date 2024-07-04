namespace SkelandStore.Core.Entities.Order_Aggregation
{
    public class OrderItem : BaseEntity //To inherit id prop which refer to number of Order
    {
        public OrderItem()
        {

        }
        public OrderItem(ProductItemOrdered product, decimal price, int quentity)
        {
            Product = product;
            Price = price;
            Quentity = quentity;
        }

        public ProductItemOrdered Product { get; set; }
        public decimal Price { get; set; }
        public int Quentity { get; set; }

    }
}
