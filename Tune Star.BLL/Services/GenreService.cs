using AutoMapper;
using Soccer.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tune_Star.BLL.DTO;
using Tune_Star.BLL.Interfaces;
using Tune_Star.DAL.Entities;

namespace Tune_Star.BLL.Services
{
    public class GenreService : IGenreService
    {
        IUnitOfWork Database { get; set; }

        public GenreService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public async Task CreateGenre(GenreDTO genreDto)
        {
            var genre = new Genres
            {
                Id = genreDto.Id,
                Name = genreDto.Name

            };
            await Database.Genres.Create(genre);
            await Database.Save();
        }

        public async Task UpdateGenre(GenreDTO genreDto)
        {
            var genre = new Genres
            {
                Id = genreDto.Id,
                Name = genreDto.Name
            };
            Database.Genres.Update(genre);
            await Database.Save();
        }

        public async Task DeleteGenre(int id)
        {
            await Database.Genres.Delete(id);
            await Database.Save();
        }

        public async Task<GenreDTO> GetGenre(int id)
        {
            var genre = await Database.Genres.Get(id);
            if (genre == null)
                throw new Exception("Wrong genre!");
            return new GenreDTO
            {
                Id = genre.Id,
                Name = genre.Name
               
            };
        }



        public async Task<IEnumerable<GenreDTO>> GetGenres()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Genres, GenreDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Genres>, IEnumerable<GenreDTO>>(await Database.Genres.GetAll());
        }

    }
}
