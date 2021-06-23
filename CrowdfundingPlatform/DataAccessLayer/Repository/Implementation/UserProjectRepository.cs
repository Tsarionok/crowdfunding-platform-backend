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
    public class UserProjectRepository : BaseRepository, IUserProjectRepository
    {
        public UserProjectRepository(CrowdfundingDbContext context) : base(context)
        {
        }

        public async Task Create(UserProject userProject)
        {
            await Context.UserProjects.AddAsync(userProject);
            await Context.SaveChangesAsync();
        }

        public async Task<UserProject> DeleteById(int id)
        {
            UserProject userProject = null;
            if (HasAnyItem(id).Result)
            {
                userProject = await Context.UserProjects.Where(u => u.Id.Equals(id)).FirstAsync();
                Context.UserProjects.Remove(Context.UserProjects
                    .Where(u => u.Id.Equals(id))
                    .FirstAsync().Result);
                await Context.SaveChangesAsync();
            }
            return await Task.Run(() => userProject);
        }

        public async Task<ICollection<UserProject>> ReadAll()
        {
            return await Context.UserProjects.ToListAsync();
        }

        public async Task<UserProject> ReadById(int id)
        {
            return await Context.UserProjects.Where(c => c.Id.Equals(id)).FirstAsync();
        }

        public async Task Update(UserProject userProject)
        {
            Context.Entry(userProject).State = EntityState.Modified;
            await Context.SaveChangesAsync();
        }

        public async Task<bool> HasAnyItem(int id)
        {
            return await Context.UserProjects.AnyAsync(c => c.Id.Equals(id));
        }
    }
}