using Core_Layer;
using Core_Layer.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository_Layer
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext _dbcontext;

        public GenericRepository(StoreContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task Add(T entity)
        {
           await _dbcontext.Set<T>().AddAsync(entity);
        }

        public async Task Delete(T entity)
        {
            _dbcontext.Set<T>().Remove(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            if (typeof(T)==typeof(Game))
            {
                return (IEnumerable<T>)await _dbcontext.Games.Include(o => o.Category)
                    .Include(p => p.Devices).ThenInclude(d=>d.Device).ToListAsync() ;
            }
            return (IEnumerable<T>) await _dbcontext.Set<T>().ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
         
            return (T) await _dbcontext.Set<T>().FindAsync(id);
        }

        public async Task update(T entity)
        {
            _dbcontext.Set<T>().Update(entity);
        }
    }
}
