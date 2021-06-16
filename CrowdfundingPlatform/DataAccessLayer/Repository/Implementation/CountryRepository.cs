using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Context;
using DataAccessLayer.Entity;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repository.Implementation
{
    public class CountryRepository : BaseRepository, ICountryRepository
    {
        public CountryRepository(CrowdfundingDbContext context) : base(context)
        {

        }

        public void Create(Country country)
        {
            Context.Countries.Add(country);
            Context.SaveChanges();
        }

        public void DeleteById(int id)
        {
            if (Context.Countries.Any(c => c.Id.Equals(id)))
            {
                Context.Countries.Remove(Context.Countries
                .Where(c => c.Id.Equals(id))
                .First());
                Context.SaveChanges();
            }
        }

        public ICollection<Country> ReadAll()
        {
            return Context.Countries.ToList();
        }

        public Country ReadById(int id)
        {
            return Context.Countries.Where(c => c.Id.Equals(id)).First();
        }

        public void Update(Country country)
        {
            Context.Entry(country).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
