using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicPortal.Models;
using System.Diagnostics;
using Tune_Star.BLL.DTO;
using Tune_Star.BLL.Interfaces;
using Tune_Star.DAL.Entities;
using Tune_Star.Models;

namespace Tune_Star.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISongService songService;
        private readonly IGenreService genreService;

        public HomeController(ISongService songServ, IGenreService genreServ)
        {
            songService = songServ;
            genreService = genreServ;
        }

        public async Task<IActionResult> Index(string position, int genre = 0, int page = 1,
            SortState sortOrder = SortState.NameAsc)
        {
            int pageSize = 6;


            var songList = await songService.GetSongs();
            IQueryable<SongDTO> songs = songList.AsQueryable();


            if (genre != 0)
            {
                songs = songs.Where(p => p.GenreId == genre);
            }
            if (!string.IsNullOrEmpty(position))
            {
                songs = songs.Where(p => p.Artist == position);
            }

            songs = sortOrder switch
            {
                SortState.NameDesc => songs.OrderByDescending(s => s.Title),
                SortState.GenreAsc => songs.OrderBy(s => s.Genre),
                SortState.GenreDesc => songs.OrderByDescending(s => s.Genre),
                SortState.ArtistDesc => songs.OrderByDescending(s => s.Artist),
                SortState.ArtistAsc => songs.OrderBy(s => s.Artist),
                _ => songs.OrderBy(s => s.Title),
            };

            var count = songs.Count();

            var items = songs.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            var allgenres = await genreService.GetGenres();
            var allgenresList = allgenres.ToList();

            IndexViewModel viewModel = new IndexViewModel(
                items,
                new PageViewModel(count, page, pageSize),
                new FilterViewModel(allgenresList, genre, position),
                new SortViewModel(sortOrder)
            );



            string a = "a";



            return View(await songService.GetSongs());
        }

        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }


    }
}
