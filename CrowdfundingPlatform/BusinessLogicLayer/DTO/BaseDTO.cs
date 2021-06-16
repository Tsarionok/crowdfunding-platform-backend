using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.DTO
{
    public abstract class BaseDTO
    {
        [Key]
        public int Id { get; set; }
    }
}
