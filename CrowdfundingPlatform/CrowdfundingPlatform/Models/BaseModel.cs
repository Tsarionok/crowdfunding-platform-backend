using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrowdfundingPlatform.Models
{
    public abstract class BaseModel<T>
    {
        public T Id { get; set; }
    }
}
