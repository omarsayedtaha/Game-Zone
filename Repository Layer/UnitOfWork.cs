using Core_Layer;
using Core_Layer.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository_Layer
{
    public class UnitOfWork : IUnitOfWork
    {
        public IGenericRepository<Game> _gamesRepo { get; set; }
        public IGenericRepository<Category> _categoriesRepo { get; set; }
        public IGenericRepository<Device> _DevicesRepo { get; set; }

        private readonly StoreContext _dbContext;

        public UnitOfWork(StoreContext dbContext)
        {
            _dbContext = dbContext;
        }

        Hashtable _repsitory;
        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            if (_repsitory is null)
                _repsitory = new Hashtable();

            var type = typeof(TEntity).Name;


            if (!_repsitory.ContainsKey(type))
            {
                var repository = new GenericRepository<TEntity>(_dbContext);
                _repsitory.Add(type, repository);
            }
            return _repsitory[type] as IGenericRepository<TEntity>;
        }
        public async Task<int> CompleteAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
        public async ValueTask DisposeAsync()
        {
            await _dbContext.DisposeAsync();
        }


    }
}
