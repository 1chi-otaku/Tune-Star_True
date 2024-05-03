using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Soccer.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Tune_Star.BLL.DTO;
using Tune_Star.BLL.Interfaces;
using Tune_Star.DAL.Entities;

namespace Tune_Star.BLL.Services
{
    public class UserService : IUserService
    {
        IUnitOfWork Database { get; set; }

        public UserService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public async Task CreateUser(UserDTO userDto)
        {
            byte[] saltbuf = new byte[16];

            RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create();
            randomNumberGenerator.GetBytes(saltbuf);

            StringBuilder sb = new StringBuilder(16);
            for (int i = 0; i < 16; i++)
                sb.Append(string.Format("{0:X2}", saltbuf[i]));
            string salt = sb.ToString();

            //переводим пароль в байт-массив  
            byte[] password = Encoding.Unicode.GetBytes(salt + userDto.Password);

            //вычисляем хеш-представление в байтах  
            byte[] byteHash = SHA256.HashData(password);

            StringBuilder hash = new StringBuilder(byteHash.Length);
            for (int i = 0; i < byteHash.Length; i++)
                hash.Append(string.Format("{0:X2}", byteHash[i]));


            var user = new Users
            {
                Id = userDto.Id,
                Login = userDto.Login,
                Password = hash.ToString(),
                Status = userDto.Status,
                Salt = salt,
            };
            await Database.Users.Create(user);
            await Database.Save();
        }

        public async Task UpdateUser(UserDTO userDto)
        {
            var user = new Users
            {
                Id = userDto.Id,
                Login = userDto.Login,
                Password = userDto.Password,
                Status = userDto.Status,
                Salt = userDto.Salt,
            };
            var existingUser = await Database.Users.Get(userDto.Id);
            if (existingUser == null)
            {
                throw new Exception("Пользователь не найден");
            }
            existingUser.Login = user.Login;
            existingUser.Password = user.Password;
            existingUser.Status = user.Status;
            existingUser.Salt = user.Salt;
            Database.Users.Update(existingUser);
            await Database.Save();
        }

        public async Task DeleteUser(int id)
        {
            await Database.Users.Delete(id);
            await Database.Save();
        }

        public async Task<UserDTO> GetUser(int id)
        {
            var user = await Database.Users.Get(id);
            if (user == null)
                throw new Exception("Wrong user!");
            return new UserDTO
            {
                Id = user.Id,
                Login = user.Login,
                Password = user.Password,
                Status = user.Status,
                Salt = user.Salt,
            };
        }

        public async Task<UserDTO> GetUser(string login)
        {
            var user = await Database.Users.Get(login);
            if (user == null)
                throw new Exception("Wrong user!");
            return new UserDTO
            {
                Id = user.Id,
                Login = user.Login,
                Password = user.Password,
                Status = user.Status,
                Salt = user.Salt,
            };

        }

        // Automapper позволяет проецировать одну модель на другую, что позволяет сократить объемы кода и упростить программу.

        public async Task<IEnumerable<UserDTO>> GetUsers()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Users, UserDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Users>, IEnumerable<UserDTO>>(await Database.Users.GetAll());
        }

      
    }
}
