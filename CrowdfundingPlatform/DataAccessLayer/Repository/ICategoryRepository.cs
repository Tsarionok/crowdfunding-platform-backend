using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Entity;

namespace DataAccessLayer.Repository
{
    public interface ICategoryRepository : ICrudRepository<Category>
    {
        public Task<bool> HasAny(int id);
    }
}
