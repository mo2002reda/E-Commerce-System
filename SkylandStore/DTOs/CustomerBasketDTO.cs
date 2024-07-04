using SkelandStore.Core.Entities;

namespace SkylandStore.DTOs
{
    public class CustomerBasketDTO
    {
        public string id { get; set; }
        public List<BasketItemDTO> BasketItemDTO { get; set; }
        public CustomerBasketDTO(string Id)
        {
            id = Id;
        }
    }
}
