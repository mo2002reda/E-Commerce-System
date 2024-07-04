using SkelandStore.Core.Entities;
using SkelandStore.Core.Interface_sRepository;
using StackExchange.Redis;
using System.Text.Json;

namespace SkyLand.Repository.Data
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;//Reference Of database which Connected with IConnectionMultiplexer 

        //IConnectionMultiplexer : Responsible for Connection with InMemory To Can Process Basket Items in Redis
        public BasketRepository(IConnectionMultiplexer redis)
        {//Ask ClR For Object From Class Implement Interface IConnectionMultiplexer
            _database = redis.GetDatabase();
        }

        public async Task<bool> Deletebasket(string id)
        => await _database.KeyDeleteAsync(id);//deal with Basket Key(Id)

        public async Task<CustomerBasket?> GetBasketAsync(string Basketid)
        {
            var Basket = await _database.StringGetAsync(Basketid);
            return Basket.IsNull ? null : JsonSerializer.Deserialize<CustomerBasket>(Basket);
        }

        public async Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket Basket)
        {
            var JsonBasket = JsonSerializer.Serialize(Basket);//Serilize From Customer To Json To can Store in Redis
            return await _database.StringSetAsync(Basket.id, JsonBasket, TimeSpan.FromDays(1)) ? await GetBasketAsync(Basket.id) : null;
            //if Basket Exist => Update it with new items with it's Id ,else => Create One with new items 
        }
    }
}
