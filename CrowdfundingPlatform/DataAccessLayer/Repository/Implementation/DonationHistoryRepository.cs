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
    public class DonationHistoryRepository : BaseRepository, IDonationHistoryRepository
    {
        public DonationHistoryRepository(CrowdfundingDbContext context) : base(context)
        {
        }

        public async Task Create(DonationHistory donationHistory)
        {
            await Context.DonationHistories.AddAsync(donationHistory);
            await Context.SaveChangesAsync();
        }

        public async Task<DonationHistory> DeleteById(int id)
        {
            DonationHistory donationHistory = null;
            if (HasAnyItem(id).Result)
            {
                donationHistory = await Context.DonationHistories.Where(d => d.Id.Equals(id)).FirstAsync();
                Context.DonationHistories.Remove(Context.DonationHistories
                    .Where(d => d.Id.Equals(id))
                    .FirstAsync().Result);
                await Context.SaveChangesAsync();
            }
            return await Task.Run(() => donationHistory);
        }

        public async Task<ICollection<DonationHistory>> ReadAll()
        {
            return await Context.DonationHistories.ToListAsync();
        }

        public async Task<DonationHistory> ReadById(int id)
        {
            return await Context.DonationHistories.Where(d => d.Id.Equals(id)).FirstAsync();
        }

        public async Task Update(DonationHistory donationHistory)
        {
            Context.Entry(donationHistory).State = EntityState.Modified;
            await Context.SaveChangesAsync();
        }

        public async Task<bool> HasAnyItem(int id)
        {
            return await Context.DonationHistories.AnyAsync(c => c.Id.Equals(id));
        }
    }
}
