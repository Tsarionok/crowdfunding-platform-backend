using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Context;
using DataAccessLayer.Entity;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repository
{
    public abstract class BaseRepository
    {
        protected CrowdfundingDbContext Context;

        protected BaseRepository(CrowdfundingDbContext context)
        {
            Context = context;
        }
    }
}
