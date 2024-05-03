using AutoMapper;
using Soccer.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Tune_Star.BLL.DTO;
using Tune_Star.BLL.Interfaces;
using Tune_Star.DAL.Entities;

namespace Tune_Star.BLL.Services
{
    public class SongService : ISongService
    {
        IUnitOfWork Database { get; set; }

        public SongService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public async Task CreateSong(SongDTO songDto)
        {
            var song = new Songs
            {
                Id = songDto.Id,
                Title = songDto.Title,
                Artist = songDto.Artist,
                Img = songDto.Img,
                GenreId = songDto.GenreId,
                Path = songDto.Path,
            };
            await Database.Songs.Create(song);
            await Database.Save();
        }

        public async Task UpdateSong(SongDTO songDto)
        {
            var song = new Songs
            {
                Id = songDto.Id,
                Title = songDto.Title,
                Artist = songDto.Artist,
                Img = songDto.Img,
                GenreId = songDto.GenreId,
                Path = songDto.Path
            };
            Database.Songs.Update(song);
            await Database.Save();
        }

        public async Task DeleteSong(int id)
        {
            await Database.Songs.Delete(id);
            await Database.Save();
        }

        public async Task<SongDTO> GetSong(int id)
        {
            var song = await Database.Songs.Get(id);
            if (song == null)
                throw new Exception("Wrong song!");
            return new SongDTO
            {
                Id = song.Id,
                Title = song.Title,
                Artist = song.Artist,
                Img = song.Img,
                GenreId = song.GenreId,
                Genre = song.Genre?.Name,
                Path = song.Path
            };
        }

        // Automapper позволяет проецировать одну модель на другую, что позволяет сократить объемы кода и упростить программу.

        public async Task<IEnumerable<SongDTO>> GetSongs()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Songs, SongDTO>()
            .ForMember("Genre", opt => opt.MapFrom(c => c.Genre.Name)));
            var mapper = new Mapper(config);
            return mapper.Map<IEnumerable<Songs>, IEnumerable<SongDTO>>(await Database.Songs.GetAll());
        }

    }
}
