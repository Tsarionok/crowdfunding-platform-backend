using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Context;
using DataAccessLayer.Entity;

namespace DataAccessLayer.Repository.Implementation
{
    public class CountryRepository : BaseRepository, ICountryRepository
    {
        public CountryRepository(CrowdfundingDbContext context) : base(context)
        {

        }

        // TODO method's realization
        public void Create(Country entity)
        {
            throw new NotImplementedException();
        }

        public void DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public ICollection<Country> ReadAll()
        {
            throw new NotImplementedException();
        }

        public Country ReadById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Country entity)
        {
            throw new NotImplementedException();
        }
    }
}
