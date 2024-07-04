using Microsoft.EntityFrameworkCore;
using SkelandStore.Core.Entities;
using SkelandStore.Core.Interface_sRepository;
using SkelandStore.Core.Specification;
using SkyLand.Repository.Data;

namespace SkyLand.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreDbContext _context;

        public GenericRepository(StoreDbContext context)
        {
            _context = context;
        }

        #region WithOut Specifications
        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            #region With Out Spacification
            //if (typeof(T) == typeof(Product))
            //{
            //    return (IEnumerable<T>)await _context.Products.Include(p => p.ProductType).Include(p => p.ProductBrand).ToListAsync();
            //}
            #endregion

            return await _context.Set<T>().ToListAsync();
        }


        public async Task<T> GetByIdAsync(int id)
        {
            #region WithOut Specification
            //if (typeof(T) == typeof(Product))
            //{
            //    //return await _context.Products.Where(p=>p.Id==id).Include(p=>p.ProductBrand).Include(P=>P.ProductType);
            //} 
            #endregion

            return await _context.Set<T>().Where(p => p.Id == id).FirstOrDefaultAsync();

        }

        #endregion
        public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> spec)
        => await ApplySpecification(spec).ToListAsync();

        public async Task<T> GetByIdWithSpecAsync(ISpecification<T> spec)
        => await ApplySpecification(spec).FirstOrDefaultAsync();

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)//to don't dublicate the code 
        {
            return SpecificationEvaluator<T>.GetQuery(_context.Set<T>(), spec);
        }

        public async Task<int> GetCountWithSpecAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).CountAsync();
            //it will get Products Firstly with ApplySpecification Then Get Count
        }

        public async Task AddAsync(T entity)
        => await _context.Set<T>().AddAsync(entity);

    }
}
