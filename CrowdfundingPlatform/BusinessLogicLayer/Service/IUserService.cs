using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.DTO;

namespace BusinessLogicLayer.Service
{
    public interface IUserService : ICrudService<UserDTO, string>
    {
        public Task UploadAvatar(UserDTO user);

        public Task<UserDTO> ReadByEmail(string email);

        public Task<ICollection<string>> GetRoles(string email);
    }
}
