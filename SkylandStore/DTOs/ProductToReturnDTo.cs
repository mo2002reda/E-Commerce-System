using SkelandStore.Core.Entities;

namespace SkylandStore.DTOs
{
    public class ProductToReturnDTo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }
        public int ProductBrandId { get; set; }//Fk 
        public string ProductBrand { get; set; }//Carry Brand name
        public int ProductTypeId { get; set; }//Fk
        public string ProductType { get; set; }//Carry Type Name
    }
}
