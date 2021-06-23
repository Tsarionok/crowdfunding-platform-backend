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

        private UserRepository UserRepository;

        private ProjectRepository ProjectRepository;

        private PhotoRepository PhotoRepository;

        private UserProjectRepository UserProjectRepository;

        private CommentRepository CommentRepository;

        private DonationHistoryRepository DonationHistoryRepository;

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

        public UserRepository Users
        {
            get
            {
                if (UserRepository == null)
                {
                    UserRepository = new UserRepository(Context);
                }
                return UserRepository;
            }
        }

        public ProjectRepository Projects
        {
            get
            {
                if (ProjectRepository == null)
                {
                    ProjectRepository = new ProjectRepository(Context);
                }
                return ProjectRepository;
            }
        }

        public PhotoRepository Photos
        {
            get
            {
                if (PhotoRepository == null)
                {
                    PhotoRepository = new PhotoRepository(Context);
                }
                return PhotoRepository;
            }
        }

        public UserProjectRepository UserProjects
        {
            get
            {
                if (UserProjectRepository == null)
                {
                    UserProjectRepository = new UserProjectRepository(Context);
                }
                return UserProjectRepository;
            }
        }

        public CommentRepository Comments
        {
            get
            {
                if (CommentRepository == null)
                {
                    CommentRepository = new CommentRepository(Context);
                }
                return CommentRepository;
            }
        }

        public DonationHistoryRepository DonationHistories
        {
            get
            {
                if (DonationHistoryRepository == null)
                {
                    DonationHistoryRepository = new DonationHistoryRepository(Context);
                }
                return DonationHistoryRepository;
            }
        }
    }
}
