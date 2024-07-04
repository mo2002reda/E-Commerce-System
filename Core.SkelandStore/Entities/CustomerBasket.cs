namespace SkelandStore.Core.Entities
{
    public class CustomerBasket
    {

        public string id { get; set; }
        public List<BasketItem> Items { get; set; }
        public CustomerBasket(string Id)
        {
            id = Id;
        }
    }
}
