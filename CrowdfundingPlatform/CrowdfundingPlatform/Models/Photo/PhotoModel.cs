using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrowdfundingPlatform.Models.Photo
{
    public class PhotoModel : BaseModel
    {
        public byte[] Image { get; set; }

        public int ProjectId { get; set; }
    }
}
