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
    public class GenresRepository : IRepository<Genres>
    {
        private TuneStarContext db;

        public GenresRepository(TuneStarContext context)
        {
            this.db = context;
        }

        public async Task<IEnumerable<Genres>> GetAll()
        {
            return await db.Genres.ToListAsync();
        }

        public async Task<Genres> Get(int id)
        {
            Genres? genre = await db.Genres.FindAsync(id);
            return genre!;
        }

        public async Task<Genres> Get(string name)
        {
            var genres = await db.Genres.Where(a => a.Name == name).ToListAsync();
            Genres? genre = genres?.FirstOrDefault();
            return genre!;
        }

        public async Task Create(Genres genre)
        {
            await db.Genres.AddAsync(genre);
        }

        public void Update(Genres genre)
        {
            db.Entry(genre).State = EntityState.Modified;
        }

        public async Task Delete(int id)
        {
            Genres? genre = await db.Genres.FindAsync(id);
            if (genre != null)
                db.Genres.Remove(genre);
        }
    }
}
