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
        void Create(T dto);

        T ReadById(int id);

        ICollection<T> ReadAll();

        void Update(T dto);

        void DeleteById(int id);
    }
}
