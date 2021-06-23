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
    public class ProjectRepository : BaseRepository, IProjectRepository
    {
        public ProjectRepository(CrowdfundingDbContext context) : base(context)
        {
        }
        public async Task Create(Project project)
        {
            await Context.Projects.AddAsync(project);
            await Context.SaveChangesAsync();
        }

        public async Task<Project> DeleteById(int id)
        {
            Project project = null;
            if (HasAnyItem(id).Result)
            {
                project = await Context.Projects.Where(p => p.Id.Equals(id)).FirstAsync();
                Context.Projects.Remove(Context.Projects
                    .Where(p => p.Id.Equals(id))
                    .FirstAsync().Result);
                await Context.SaveChangesAsync();
            }
            return await Task.Run(() => project);
        }

        public async Task<ICollection<Project>> ReadAll()
        {
            return await Context.Projects.ToListAsync();
        }

        public async Task<Project> ReadById(int id)
        {
            return await Context.Projects.Where(p => p.Id.Equals(id)).FirstAsync();
        }

        public async Task Update(Project project)
        {
            Context.Entry(project).State = EntityState.Modified;
            await Context.SaveChangesAsync();
        }

        public async Task<bool> HasAnyItem(int id)
        {
            return await Context.Projects.AnyAsync(c => c.Id.Equals(id));
        }
    }
}
