using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tune_Star.BLL.DTO
{
    public class SongDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title should not be empty!")]
        public string? Title { get; set; }

        [Required(ErrorMessage = "Artist should not be empty!")]
        public string? Artist { get; set; }
        public string? Img { get; set; }

        public string? Path { get; set; }

        public int GenreId { get; set; }
        public string? Genre { get; set; }
    }
}
