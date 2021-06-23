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
    public class CityRepository : BaseRepository, ICityRepository
    {

        public CityRepository(CrowdfundingDbContext context) : base(context)
        {

        }

        public async Task Create(City city)
        {
            await Context.Cities.AddAsync(city);
            await Context.SaveChangesAsync();
        }

        public async Task<City> DeleteById(int id)
        {
            City city = null;
            if (Context.Cities.Any(c => c.Id.Equals(id)))
            {
                city = await Context.Cities.Where(c => c.Id.Equals(id)).FirstAsync();
                Context.Cities.Remove(Context.Cities
                .Where(c => c.Id.Equals(id))
                .FirstAsync().Result);
                await Context.SaveChangesAsync();
            }
            return await Task.Run(() => city);
        }

        public async Task<IEnumerable<City>> ReadAll()
        {
            return await Context.Cities.Include(c => c.Country).ToListAsync();
        }

        public async Task<City> ReadById(int id)
        {
            return await Context.Cities.Where(c => c.Id.Equals(id)).FirstAsync();
        }

        public async Task Update(City city)
        {
            Context.Entry(city).State = EntityState.Modified;
            await Context.SaveChangesAsync();
        }

        public async Task<bool> HasAny(int id)
        {
            return await Context.Cities.AnyAsync(c => c.Id.Equals(id));
        }
    }
}
