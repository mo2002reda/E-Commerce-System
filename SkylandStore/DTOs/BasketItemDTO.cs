using System.ComponentModel.DataAnnotations;

namespace SkylandStore.DTOs
{
    public class BasketItemDTO
    {
        [Required]
        public int id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string PictureURL { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        [Range(0.1, double.MaxValue, ErrorMessage = "Price Can be not Zero")]
        public decimal Price { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quientity Must be One Item At Least")]
        public int Quentity { get; set; }

    }
}