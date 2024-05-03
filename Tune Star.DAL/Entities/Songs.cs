using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tune_Star.DAL.Entities
{
    public class Songs
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Artist { get; set; }
        public string? Img { get; set; }
        public string? Path { get; set; }

        public int GenreId { get; set; }
        public Genres? Genre { get; set; }



    }
}
