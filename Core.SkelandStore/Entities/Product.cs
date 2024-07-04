namespace SkelandStore.Core.Entities
{
    public class Product : BaseEntity
    {

        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }
        public int ProductBrandId { get; set; }//Fk 
        public ProductBrand ProductBrand { get; set; }//Brand have many Product Relation (1=>M)
        public int ProductTypeId { get; set; }//Fk
        public ProductType ProductType { get; set; }//one Type have many Product 
    }
}
