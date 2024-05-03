using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tune_Star.BLL.DTO;

namespace Tune_Star.BLL.Interfaces
{
    public interface IUserService
    {
        Task CreateUser(UserDTO userDto);
        Task UpdateUser(UserDTO userDto);
        Task DeleteUser(int id);
        Task<UserDTO> GetUser(int id);
        Task<UserDTO> GetUser(string login);
        Task<IEnumerable<UserDTO>> GetUsers();
    }
}
