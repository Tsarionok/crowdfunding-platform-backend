using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Entity;

namespace DataAccessLayer.Repository
{
    public interface ICrudRepository<T> where T : BaseEntity
    {
        public void Create(T entity);

        public T ReadById(int id);

        public ICollection<T> ReadAll();

        public void Update(T entity);

        public void DeleteById(int id);
    }
}
