using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Entity;

namespace BusinessLogicLayer.Service
{
    public interface IJwtService
    {
        string CreateToken(string userName);
    }
}
