using SkelandStore.Core.Entities;
using SkelandStore.Core.Interface_sRepository;
using SkyLand.Repository.Data;
using System.Collections;

namespace SkyLand.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreDbContext _dbContext;
        private readonly Hashtable _repositories;

        public UnitOfWork(StoreDbContext _dbContext)
        {
            this._dbContext = _dbContext;
            _repositories = new Hashtable();//To initilize hashTable when Create the Object From This Class
        }

        public async Task<int> CompletAsync()
        => await _dbContext.SaveChangesAsync();

        public async ValueTask DisposeAsync()
        => await _dbContext.DisposeAsync();


        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            var type = typeof(TEntity).Name;//will carry the name of entity ex : name of Product - name Of deliverymethod -name of order item 
            //Search Firstly about the Required it in the hashtable
            if (!_repositories.ContainsKey(type))//not exist => Create one the add to hashtable
            {
                var Repository = new GenericRepository<TEntity>(_dbContext);//For Creating an Object From The demond Entity
                _repositories.Add(type, Repository);
            }
            return (IGenericRepository<TEntity>)_repositories[type]; //do Explicitely casting cause the items stores in hashTable as (Key & value) Objects and it will Return the key as Object not as IGenericRepository<TEntity>

        }
    }
}
