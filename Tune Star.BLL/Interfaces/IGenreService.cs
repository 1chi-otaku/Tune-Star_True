using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tune_Star.BLL.DTO;

namespace Tune_Star.BLL.Interfaces
{
    public interface IGenreService
    {
        Task CreateGenre(GenreDTO genreDto);
        Task UpdateGenre(GenreDTO genreDto);
        Task DeleteGenre(int id);
        Task<GenreDTO> GetGenre(int id);
        Task<IEnumerable<GenreDTO>> GetGenres();
    }
}
