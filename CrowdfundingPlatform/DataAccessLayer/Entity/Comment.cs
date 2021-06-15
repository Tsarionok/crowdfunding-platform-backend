using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entity
{
    public class Comment : BaseEntity
    {
        public User User { get; set; }

        public Project Project { get; set; }

        public string Message { get; set; }
    }
}
