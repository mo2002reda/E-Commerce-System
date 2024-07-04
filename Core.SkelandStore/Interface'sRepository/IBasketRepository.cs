using SkelandStore.Core.Entities;

namespace SkelandStore.Core.Interface_sRepository
{
    public interface IBasketRepository
    {
        //1)Get Basket
        Task<CustomerBasket?> GetBasketAsync(string id);

        //2)Update basket
        Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket Basket);
        //3)Delete Basket
        Task<bool> Deletebasket(string id);

    }
}
