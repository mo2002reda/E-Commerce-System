using System.ComponentModel.DataAnnotations;

namespace SkylandStore.DTOs
{
    public class OrderDTO
    {
        [Required]
        public string basketId { get; set; }
        public int DeliveryMethodID { get; set; }
        public AddressDTO ShippingAddress { get; set; }
    }
}
