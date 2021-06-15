using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Repository.Implementation;

namespace DataAccessLayer.Context
{
    public interface IUnitOfWork
    {
        public CountryRepository Countries { get; } 
    }
}
