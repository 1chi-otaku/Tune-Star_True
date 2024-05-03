using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tune_Star.BLL.DTO;

namespace Tune_Star.BLL.Interfaces
{
    public interface ISongService
    {
        Task CreateSong(SongDTO songDto);
        Task UpdateSong(SongDTO songDto);
        Task DeleteSong(int id);
        Task<SongDTO> GetSong(int id);
        Task<IEnumerable<SongDTO>> GetSongs();
    }
}
