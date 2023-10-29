using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Layer.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
         Task Add(T entity);
         Task update(T entity);
         Task Delete(T entity);
         Task<IEnumerable<T>> GetAllAsync();

         Task<T> GetById(int id);
    }
}
