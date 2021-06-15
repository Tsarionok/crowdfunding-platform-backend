using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entity
{
    public class Photo : BaseEntity
    {
        public Project Project { get; set; }

        public byte[] Image { get; set; }
    }
}
