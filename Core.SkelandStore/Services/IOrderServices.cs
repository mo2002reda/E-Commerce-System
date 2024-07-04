using SkelandStore.Core.Entities.Order_Aggregation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkelandStore.Core.Services
{
    public interface IOrderServices
    {
        Task<Order?> CreateOrderAsync(string BuyerEmail, string busketId, int DeliveryMethodId, Address ShippingAddress);
        Task<IReadOnlyList<Order>> GetOrderForSpecificUserAsync(string BuyerEmail);
        Task<Order> GetOrderForSpecificUserbyIdAsync(string BuyerEmail, int Id);
    }
}
