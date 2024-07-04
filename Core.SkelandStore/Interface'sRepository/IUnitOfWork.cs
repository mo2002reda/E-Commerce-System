
using SkelandStore.Core.Entities;

namespace SkelandStore.Core.Interface_sRepository
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        //1)Function to detect The Repository To avoid Create all Repositories when Call UnitOfWork
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;
        //2)Function To Save Changes on database
        Task<int> CompletAsync();
    }
}
