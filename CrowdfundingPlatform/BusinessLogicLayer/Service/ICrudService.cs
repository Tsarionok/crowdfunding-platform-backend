using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.DTO;

namespace BusinessLogicLayer.Service
{
    public interface ICrudService<T> where T : BaseDTO 
    {
        public Task Create(T dto);

        public Task<T> ReadById(int id);

        public Task<ICollection<T>> ReadAll();

        public Task Update(T dto);

        public Task<T> DeleteById(int id);
    }
}
