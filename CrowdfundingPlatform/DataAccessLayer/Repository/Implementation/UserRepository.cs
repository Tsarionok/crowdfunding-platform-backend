using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Context;
using DataAccessLayer.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repository.Implementation
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(CrowdfundingDbContext context) : base(context)
        {
        }

        public async Task Create(User user)
        {
            await Context.Users.AddAsync(user);
            await Context.SaveChangesAsync();
        }

        public async Task<User> DeleteById(string id)
        {
            User user = null;
            if (await Context.Users.AnyAsync(c => c.Id.Equals(id)))
            {
                user = await Context.Users.Where(u => u.Id.Equals(id)).FirstAsync();
                Context.Users.Remove(Context.Users
                    .Where(u => u.Id.Equals(id))
                    .FirstAsync().Result);
                await Context.SaveChangesAsync();
            }
            return await Task.Run(() => user);
        }

        public async Task<ICollection<User>> ReadAll()
        {
            return await Context.Users
                .Include(u => u.City.Country)
                .ToListAsync();
        }

        public async Task<User> ReadById(string id)
        {
            return await Context.Users.Where(c => c.Id.Equals(id))
                .Include(u => u.City.Country)
                .FirstAsync();
        }

        public async Task Update(User user)
        {
            Context.Entry(user).State = EntityState.Modified;
            await Context.SaveChangesAsync();
        }
    }
}
