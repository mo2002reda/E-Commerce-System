using Microsoft.EntityFrameworkCore;
using SkelandStore.Core.Entities;
using SkelandStore.Core.Specification;

namespace SkyLand.Repository
{
    public static class SpecificationEvaluator<T> where T : BaseEntity
    {//this will be a container for Function that build a Query Exprission
        //_dbContext.Set<T>().Where(p=>p.Id==id).Include(x=>x.ProductBrand).Include(p=>p.ProductType)
        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> Spec)
        {
            var Query = inputQuery;//_dbContext.Set<T>()
            if (Spec.Criteris != null)
            {
                Query = Query.Where(Spec.Criteris);//_dbContext.Set<T>().Where(p=>p.Id==id)

            }
            if (Spec.Orderby != null)
            {
                Query = Query.OrderBy(Spec.Orderby);
            }
            if (Spec.OrderByDesc != null)
            {
                Query = Query.OrderByDescending(Spec.OrderByDesc);
            }
            if (Spec.IsPaginationEnabled)//if it True This mean that ApplyPagination Function and set True On IsPagination
            {
                Query = Query.Skip(Spec.Skip).Take(Spec.Take);
            }

            Query = Spec.InCludes.Aggregate(Query, (CurrentQuery, IncludeExpression) => CurrentQuery.Include(IncludeExpression));

            #region Aggregate:
            /* will add To Query Variable
               currentQuery => item to loop at all Queries
               InCluedeExpression => List Of inClude
               1)First loop :
               _dbContext.Set<T>().Where(p=>p.Id==id).Include(x=>x.ProductBrand)
              2) Second loop : add Second Include To old expression 
               _dbContext.Set<T>().Where(p=>p.Id==id).Include(x=>x.ProductBrand).Include(p=>p.ProductType)*/
            #endregion
            return Query;
        }
    }
}
