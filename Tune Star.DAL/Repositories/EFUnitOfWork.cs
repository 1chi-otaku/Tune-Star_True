using Soccer.DAL.Interfaces;
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
    public class EFUnitOfWork : IUnitOfWork
    {
        private TuneStarContext db;
        private SongsRepository songsRepository;
        private GenresRepository genresRepository;
        private UsersRepository usersRepository;

        public EFUnitOfWork(TuneStarContext context)
        {
            db = context;
        }

        public IRepository<Songs> Songs
        {
            get
            {
                if (songsRepository == null)
                    songsRepository = new SongsRepository(db);
                return songsRepository;
            }
        }

        public IRepository<Genres> Genres
        {
            get
            {
                if (genresRepository == null)
                    genresRepository = new GenresRepository(db);
                return genresRepository;
            }
        }

        public IRepository<Users> Users
        {
            get
            {
                if (usersRepository == null)
                    usersRepository = new UsersRepository(db);
                return usersRepository;
            }
        }

        public async Task Save()
        {
            await db.SaveChangesAsync();
        }

    }
}
