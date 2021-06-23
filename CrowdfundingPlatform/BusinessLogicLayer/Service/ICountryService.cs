using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.DTO;

namespace BusinessLogicLayer.Service
{
    public interface ICountryService : ICrudService<CountryDTO>, IExistenceService
    {
        
    }
}
