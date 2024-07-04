using SkelandStore.Core.Entities;
using SkelandStore.Core.Specification;
using System.Collections.Generic;

namespace SkelandStore.Core.Interface_sRepository
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        #region WithOut Specifications
        //Get All Products
        public Task<IReadOnlyList<T>> GetAllAsync();

        //Get By Id
        public Task<T> GetByIdAsync(int id);
        #endregion

        public Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> spec);
        public Task<T> GetByIdWithSpecAsync(ISpecification<T> spec);//Take Object From class which implement Ispecification Interaface

        public Task<int> GetCountWithSpecAsync(ISpecification<T> spec);//Using Specification to get Count after Filteration Or any Query Required
        public Task AddAsync(T entity);
    }
}
