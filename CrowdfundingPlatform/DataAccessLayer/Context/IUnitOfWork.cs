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

        public CityRepository Cities { get; }

        public CategoryRepository Categories { get; }

        public UserRepository Users { get; }

        public ProjectRepository Projects { get; }

        public PhotoRepository Photos { get; }

        public UserProjectRepository UserProjects { get; }

        public CommentRepository Comments { get; }

        public DonationHistoryRepository DonationHistories { get; }
    }
}
