using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.DTO;
using DataAccessLayer.Entity;

namespace BusinessLogicLayer.Service
{
    public interface IJwtService
    {
        public string CreateToken(string email);
    }
}
