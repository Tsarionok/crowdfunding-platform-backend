using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Repository.Implementation;

namespace DataAccessLayer.Context
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CrowdfundingDbContext Context;

        private CountryRepository CountryRepository;

        private CityRepository CityRepository;

        private CategoryRepository CategoryRepository;

        public UnitOfWork(CrowdfundingDbContext context)
        {
            Context = context;
        }

        public CountryRepository Countries
        {
            get
            {
                if (CountryRepository == null)
                {
                    CountryRepository = new CountryRepository(Context);
                }
                return CountryRepository;
            }
        }

        public CityRepository Cities
        {
            get
            {
                if (CityRepository == null)
                {
                    CityRepository = new CityRepository(Context);
                }
                return CityRepository;
            }
        }

        public CategoryRepository Categories
        {
            get
            {
                if (CategoryRepository == null)
                {
                    CategoryRepository = new CategoryRepository(Context);
                }
                return CategoryRepository;
            }
        }
    }
}
