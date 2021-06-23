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
    public class PhotoRepository : BaseRepository, IPhotoRepository
    {
        public PhotoRepository(CrowdfundingDbContext context) : base(context)
        {
        }

        public async Task Create(Photo photo)
        {
            await Context.Photos.AddAsync(photo);
            await Context.SaveChangesAsync();
        }

        public async Task<Photo> DeleteById(int id)
        {
            Photo photo = null;
            if (HasAnyItem(id).Result)
            {
                photo = await Context.Photos.Where(c => c.Id.Equals(id)).FirstAsync();
                Context.Photos.Remove(Context.Photos
                    .Where(c => c.Id.Equals(id))
                    .FirstAsync().Result);
                await Context.SaveChangesAsync();
            }
            return await Task.Run(() => photo);
        }

        public async Task<IEnumerable<Photo>> ReadAll()
        {
            return await Context.Photos.ToListAsync();
        }

        public async Task<Photo> ReadById(int id)
        {
            return await Context.Photos.Where(c => c.Id.Equals(id)).FirstAsync();
        }

        public async Task Update(Photo photo)
        {
            Context.Entry(photo).State = EntityState.Modified;
            await Context.SaveChangesAsync();
        }

        public async Task<bool> HasAnyItem(int id)
        {
            return await Context.Photos.AnyAsync(c => c.Id.Equals(id));
        }
    }
}
