using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.DTO;

namespace BusinessLogicLayer.Service
{
    public interface IUserService : ICrudService<UserDTO>
    {
        public Task UploadAvatar(UserDTO user);
    }
}
