using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Tune_Star.DAL.EF;
using Tune_Star.DAL.Entities;
using Tune_Star.DAL.Interfaces;

namespace Tune_Star.DAL.Repositories
{
    public class SongsRepository : IRepository<Songs>
    {
        private TuneStarContext db;

        public SongsRepository(TuneStarContext context)
        {
            this.db = context;
        }

        public async Task<IEnumerable<Songs>> GetAll()
        {
            return await db.Songs.Include(o => o.Genre).ToListAsync();
        }

        public async Task<Songs> Get(int id)
        {
            var songs = await db.Songs.Include(o => o.Genre).Where(a => a.Id == id).ToListAsync();
            Songs? song = songs?.FirstOrDefault();
            return song!;
        }

        public async Task<Songs> Get(string name)
        {
            var songs = await db.Songs.Include(o => o.Genre).Where(a => a.Title == name).ToListAsync();
            Songs? song = songs?.FirstOrDefault();
            return song!;
        }

        public async Task Create(Songs song)
        {
            await db.Songs.AddAsync(song);
        }

        public void Update(Songs song)
        {
            db.Entry(song).State = EntityState.Modified;
        }

        public async Task Delete(int id)
        {
            Songs? song = await db.Songs.FindAsync(id);
            if (song != null)
                db.Songs.Remove(song);
        }

    }
}
