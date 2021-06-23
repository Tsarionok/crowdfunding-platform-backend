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
    public class CommentRepository : BaseRepository, ICommentRepository
    {
        public CommentRepository(CrowdfundingDbContext context) : base(context)
        {
        }

        public async Task Create(Comment comment)
        {
            await Context.Comments.AddAsync(comment);
            await Context.SaveChangesAsync();
        }

        public async Task<Comment> DeleteById(int id)
        {
            Comment comment = null;
            if (!HasAnyItem(id).Result)
            {
                comment = await Context.Comments.Where(c => c.Id.Equals(id)).FirstAsync();
                Context.Comments.Remove(Context.Comments
                .Where(c => c.Id.Equals(id))
                .FirstAsync().Result);
                await Context.SaveChangesAsync();
            }
            return await Task.Run(() => comment);
        }

        public async Task<IEnumerable<Comment>> ReadAll()
        {
            return await Context.Comments.ToListAsync();
        }

        public async Task<Comment> ReadById(int id)
        {
            return await Context.Comments.Where(c => c.Id.Equals(id)).FirstAsync();
        }

        public async Task Update(Comment comment)
        {
            Context.Entry(comment).State = EntityState.Modified;
            await Context.SaveChangesAsync();
        }

        public async Task<bool> HasAnyItem(int id)
        {
            return await Context.Comments.AnyAsync(c => c.Id.Equals(id));
        }
    }
}
