using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Tune_Star.DAL.Entities;

namespace Tune_Star.DAL.EF
{
    public class TuneStarContext : DbContext
    {
        public DbSet<Songs> Songs { get; set; }
        public DbSet<Genres> Genres { get; set; }
        public DbSet<Users> Users { get; set; }
        public TuneStarContext(DbContextOptions<TuneStarContext> options)
                   : base(options)
        {
            if (Database.EnsureCreated())
            {
                Genres pop = new Genres { Name = "Pop" };
                Genres jazz = new Genres { Name = "Jazz" };
                Genres rap = new Genres { Name = "Rap" };
                Genres funk = new Genres { Name = "Funk" };
                Genres jpop = new Genres { Name = "J-Pop" };
                Genres videoGameMusic = new Genres { Name = "Video Game Music" };

                Genres.AddRange(funk, jpop, jazz, pop, rap, videoGameMusic);

                Songs.AddRange(
                    new Songs { GenreId = 6, Genre = videoGameMusic, Title = "Frozen Base", Artist = "Hiro", Img = "/pictures/FrozenBaseAct2.png", Path = "/music/FrozenBaseAct2.mp3" },
                    new Songs { GenreId = 5, Genre = jpop, Title = "Song of Inheritance", Artist = "Yuiko Ohara", Img = "/pictures/Song of Inheritance.png", Path = "/music/Song of Inheritance.mp3"},
                    new Songs { GenreId = 3, Genre = rap, Title = "Color Your Night", Artist = "Lotus Juice", Img = "/pictures/Color Your Night.png", Path = "/music/Color Your Night.mp3" },
                    new Songs { GenreId = 2, Genre = jazz, Title = "Fly Me To The Moon", Artist = "Frank Sinatra", Img = "/pictures/Fly Me To The Moon.png", Path = "/music/Fly Me To The Moon.mp3" },
                    new Songs { GenreId = 6, Genre = videoGameMusic, Title = "Feathers", Artist = "Ryukishi07", Img = "/pictures/Feathers.png", Path = "/music/Feathers.mp3" },
                    new Songs { GenreId = 4, Genre = funk, Title = "The Whims Of Fate", Artist = "Lyn Inaizumi", Img = "/pictures/The Whims Of Fate.png", Path = "/music/The Whims Of Fate.mp3" },
                    new Songs { GenreId = 5, Genre = jpop, Title = "Watashi Dake Yuurei", Artist = "Lowphonic", Img = "/pictures/WatashiDakeYuurei.png", Path = "/music/Watashi Dake Yuurei.mp3" },
                    new Songs { GenreId = 2, Genre = jazz, Title = "Billie Jean", Artist = "Michael Jackson", Img = "/pictures/Billie Jean.png", Path = "/music/Billie Jean.mp3" }
                );

                SaveChanges();




            }
        }
    }
}
