using SkelandStore.Core.Entities;
using SkelandStore.Core.Specification;

namespace SkelandStore.Core
{
    public class ProductWithBrandAndTypeWithSpecification : BaseSpacifictions<Product>
    {//create this class to be more specific type (to be Product Type )To can Deal with Props of product
        //CTOR Of Get All Product
        public ProductWithBrandAndTypeWithSpecification(ProductSpecParams Params) :
            base
            (p =>
            (string.IsNullOrEmpty(Params.Search) || p.Name.ToLower().Contains(Params.Search))
            &&
            (!Params.BrandId.HasValue || p.ProductBrandId == Params.BrandId)
            &&
            (!Params.TypeId.HasValue || p.ProductTypeId == Params.TypeId)
            )
        {
            InCludes.Add(p => p.ProductType);
            InCludes.Add(p => p.ProductBrand);
            if (!string.IsNullOrEmpty(Params.Sort))
            {
                //there were 3 cases of Sorting {Sort by name (default if user not entered any Value)
                //                              ,Sort by Price{Asce OR Desc}
                switch (Params.Sort)
                {
                    case "PriceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "PriceDesc":
                        AddOrderByDesc(p => p.Price);
                        break;
                    default:
                        AddOrderBy(p => p.Name);
                        break;
                }
            }

            #region Apply Pagination
            //Products =>10
            //PageSize=>10
            //pageIndex(numOfPage Which required to show now)=>5
            //So if we want to Get page number(5) Skip 4pages (40 Products) and take 10 Products
            ApplyPagination(Params.PageSize * (Params.PageIndex - 1), Params.PageSize);
            #endregion
        }

        //CTOR of Get Product By Id
        public ProductWithBrandAndTypeWithSpecification(int id) : base(P => P.Id == id)
        {
            InCludes.Add(p => p.ProductType);
            InCludes.Add(p => p.ProductBrand);
        }

    }
}
