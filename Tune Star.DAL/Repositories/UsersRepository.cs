using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tune_Star.DAL.EF;
using Tune_Star.DAL.Entities;
using Tune_Star.DAL.Interfaces;

namespace Tune_Star.DAL.Repositories
{
    public class UsersRepository : IRepository<Users>
    {
        private TuneStarContext db;

        public UsersRepository(TuneStarContext context)
        {
            this.db = context;
        }

        public async Task<IEnumerable<Users>> GetAll()
        {
            return await db.Users.ToListAsync();
        }

        public async Task<Users> Get(int id)
        {
            Users? user = await db.Users.FindAsync(id);
            return user!;
        }

        public async Task<Users> Get(string name)
        {
            var users = await db.Users.Where(a => a.Login == name).ToListAsync();
            Users? user = users?.FirstOrDefault();
            return user!;
        }

        public async Task Create(Users user)
        {
            await db.Users.AddAsync(user);
        }

        public void Update(Users user)
        {
            db.Entry(user).State = EntityState.Modified;
        }

        public async Task Delete(int id)
        {
            Users? user = await db.Users.FindAsync(id);
            if (user != null)
                db.Users.Remove(user);
        }
    }
}
