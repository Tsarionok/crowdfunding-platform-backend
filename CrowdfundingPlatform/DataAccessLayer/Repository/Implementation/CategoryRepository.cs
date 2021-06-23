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
    public class CategoryRepository : BaseRepository, ICategoryRepository
    {
        public CategoryRepository(CrowdfundingDbContext context) : base(context)
        {

        }

        public async Task Create(Category category)
        {
            await Context.Categories.AddAsync(category);
            await Context.SaveChangesAsync();
        }

        public async Task<Category> DeleteById(int id)
        {
            Category category = null;
            if (!HasAnyItem(id).Result)
            {
                category = await Context.Categories.Where(c => c.Id.Equals(id)).FirstAsync();
                Context.Categories.Remove(Context.Categories
                .Where(c => c.Id.Equals(id))
                .FirstAsync().Result);
                await Context.SaveChangesAsync();
            }
            return await Task.Run(() => category);
        }

        public async Task<IEnumerable<Category>> ReadAll()
        {
            return await Context.Categories.ToListAsync();
        }

        public async Task<Category> ReadById(int id)
        {
            return await Context.Categories.Where(c => c.Id.Equals(id)).FirstAsync();
        }

        public async Task Update(Category category)
        {
            Context.Entry(category).State = EntityState.Modified;
            await Context.SaveChangesAsync();
        }

        public async Task<bool> HasAnyItem(int id)
        {
            return await Context.Categories.AnyAsync(c => c.Id.Equals(id));
        }
    }
}
