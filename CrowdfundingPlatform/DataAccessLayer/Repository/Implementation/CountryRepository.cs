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

        public async Task Create(Country country)
        {
            await Context.Countries.AddAsync(country);
            await Context.SaveChangesAsync();
        }

        public async Task<Country> DeleteById(int id)
        {
            Country country = null;
            if (Context.Countries.Any(c => c.Id.Equals(id)))
            {
                country = await Context.Countries.Where(c => c.Id.Equals(id)).FirstAsync();
                Context.Countries.Remove(Context.Countries
                .Where(c => c.Id.Equals(id))
                .FirstAsync().Result);
                await Context.SaveChangesAsync();
            }
            return await Task.Run(() => country);
        }

        public async Task<IEnumerable<Country>> ReadAll()
        {
            return await Context.Countries.ToListAsync();
        }

        public async Task<Country> ReadById(int id)
        {
            return await Context.Countries.Where(c => c.Id.Equals(id)).FirstAsync();
        }

        public async Task Update(Country country)
        {
            Context.Entry(country).State = EntityState.Modified;
            await Context.SaveChangesAsync();
        }

        public async Task<bool> HasAny(int id)
        {
            return await Context.Countries.AnyAsync(c => c.Id.Equals(id));
        }
    }
}
