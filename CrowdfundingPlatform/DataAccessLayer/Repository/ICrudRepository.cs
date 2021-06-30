using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Entity;

namespace DataAccessLayer.Repository
{
    public interface ICrudRepository<T> : IExistenceRepository where T : class
    {
        public Task Create(T entity);

        public Task<T> ReadById(int id);

        public Task<ICollection<T>> ReadAll();

        public Task Update(T entity);

        public Task<T> DeleteById(int id);
    }
}
