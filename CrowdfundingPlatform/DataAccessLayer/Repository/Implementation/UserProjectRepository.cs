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
            if (await Context.UserProjects.AnyAsync(c => c.Id.Equals(id)))
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
            if (await Context.UserProjects.AnyAsync(c => c.Id.Equals(id)))
            {
                return await Context.UserProjects.Where(c => c.Id.Equals(id)).FirstAsync();
            }
            return null;
        }

        public async Task Update(UserProject userProject)
        {
            Context.Entry(userProject).State = EntityState.Modified;
            await Context.SaveChangesAsync();
        }

        public async Task AddToFavourites(UserProject userProject)
        {
            if (await Context.UserProjects.AnyAsync(c =>
                    c.ProjectId.Equals(userProject.ProjectId) &&
                    c.UserId.Equals(userProject.UserId))
                )
            {
                UserProject user = await Context.UserProjects
                    .Where(c =>
                        c.ProjectId.Equals(userProject.ProjectId) &&
                        c.UserId.Equals(userProject.UserId))
                    .FirstAsync();

                user.IsFavourites = userProject.IsFavourites;

                Context.Entry(user).State = EntityState.Modified; 
            }
            else
            {
                await Context.UserProjects.AddAsync(userProject);
            }
            await Context.SaveChangesAsync();
        }

        public async Task Estimate(UserProject userProject)
        {
            if (await Context.UserProjects.AnyAsync(c =>
                    c.ProjectId.Equals(userProject.ProjectId) &&
                    c.UserId.Equals(userProject.UserId))
                )
            {
                UserProject user = await Context.UserProjects
                    .Where(c =>
                        c.ProjectId.Equals(userProject.ProjectId) &&
                        c.UserId.Equals(userProject.UserId))
                    .FirstAsync();

                user.Evaluation = userProject.Evaluation;

                Context.Entry(user).State = EntityState.Modified;
            }
            else
            {
                await Context.UserProjects.AddAsync(userProject);
            }
            await Context.SaveChangesAsync();
        }
    }
}