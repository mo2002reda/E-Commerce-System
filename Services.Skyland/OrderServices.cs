using SkelandStore.Core.Entities;
using SkelandStore.Core.Entities.Order_Aggregation;
using SkelandStore.Core.Interface_sRepository;
using SkelandStore.Core.Services;

namespace Skyland.Services
{
    public class OrderServices : IOrderServices
    {
        private readonly IBasketRepository _basketRepo;
        private readonly IUnitOfWork _unitOfWork;

        public OrderServices(IBasketRepository basketRepo,
                              IUnitOfWork unitOfWork
                              )
        {
            _basketRepo = basketRepo;
            _unitOfWork = unitOfWork;
        }

        public async Task<Order?> CreateOrderAsync(string BuyerEmail, string busketId, int DeliveryMethodId, Address ShippingAddress)
        {
            //1)Get Basket From Basket Repo
            var basket = await _basketRepo.GetBasketAsync(busketId);
            //2)Get Selected Items at Basket From Product Repo
            var OrderItems = new List<OrderItem>();
            if (basket?.Items.Count >= 0)
            {
                foreach (var item in basket.Items)//loop at Products in the basket 
                {   //Get Product From Database
                    var Product = await _unitOfWork.Repository<Product>().GetByIdAsync(item.id);
                    //Get all info about the Product
                    var ProductItemOrdered = new ProductItemOrdered(Product.Id, Product.Name, Product.PictureUrl);
                    //Form OrderItems
                    var OrderedItem = new OrderItem(ProductItemOrdered, Product.Price, item.Quentity);
                    OrderItems.Add(OrderedItem);
                }
            }
            //3)CalCulate Sub.Total
            var SubTotal = OrderItems.Sum(item => item.Quentity * item.Price);//will loop at list of Ordered Items ans sum all prices of it 
            //4)Get Delivery Method From DeliveryMethod Repo
            var GetDeliveryMethod = await _unitOfWork.Repository<DeleveryMethod>().GetByIdAsync(DeliveryMethodId);
            //5)Create Order
            var Order = new Order(BuyerEmail, ShippingAddress, GetDeliveryMethod, OrderItems, SubTotal);//this will be the Object which will added in database

            //6)Add Order Locally
            await _unitOfWork.Repository<Order>().AddAsync(Order);

            //7)Save Order At Database

            var Result = await _unitOfWork.CompletAsync();
            if (Result <= 0) return null;
            return Order;

        }

        public Task<IReadOnlyList<Order>> GetOrderForSpecificUserAsync(string BuyerEmail)
        {
            throw new NotImplementedException();
        }

        public Task<Order> GetOrderForSpecificUserbyIdAsync(string BuyerEmail, int Id)
        {
            throw new NotImplementedException();
        }
    }
}
