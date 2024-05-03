using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Tune_Star.BLL.Interfaces;

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

        public async Task<IActionResult> Index()
        {
            return View(await songService.GetSongs());
        }

        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }


    }
}
