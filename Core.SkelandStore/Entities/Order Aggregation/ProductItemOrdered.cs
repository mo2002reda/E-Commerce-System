namespace SkelandStore.Core.Entities.Order_Aggregation
{
    public class ProductItemOrdered
    {
        public ProductItemOrdered()
        {

        }
        public ProductItemOrdered(int productId, string productName, string picureURL)
        {
            ProductId = productId;
            ProductName = productName;
            PicureURL = picureURL;
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string PicureURL { get; set; }
    }
}
