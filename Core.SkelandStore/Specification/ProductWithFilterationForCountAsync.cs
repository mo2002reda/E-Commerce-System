using SkelandStore.Core.Entities;
using System.Linq.Expressions;

namespace SkelandStore.Core.Specification
{
    public class ProductWithFilterationForCountAsync : BaseSpacifictions<Product>
    {
        public ProductWithFilterationForCountAsync(ProductSpecParams Params) :
            base(p =>
            (
            (string.IsNullOrEmpty(Params.Search) || p.Name.ToLower().Contains(Params.Search))
                &&
            (!Params.BrandId.HasValue || p.ProductBrandId == Params.BrandId)
                &&
            (!Params.TypeId.HasValue || p.ProductTypeId == Params.TypeId))
            )
        {
        }
    }
}
