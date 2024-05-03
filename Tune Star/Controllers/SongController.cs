using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using Tune_Star.BLL.DTO;
using Tune_Star.BLL.Interfaces;


namespace Tune_Star.Controllers
{
    public class SongController : Controller
    {
        private readonly ISongService songService;
        private readonly IGenreService genreService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public SongController(ISongService songServ, IWebHostEnvironment webHostEnvironment, IGenreService genreService)
        {
            songService = songServ;
            _webHostEnvironment = webHostEnvironment;
            this.genreService = genreService;
        }


        public async Task<IActionResult> Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }
                SongDTO song = await songService.GetSong((int)id);
                return View(song);
            }
            catch (ValidationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        public IActionResult Download(string filePath)
        {
            string webRootPath = _webHostEnvironment.WebRootPath;
            string fileFullPath = Path.Combine(webRootPath, filePath.TrimStart('/'));

            if (System.IO.File.Exists(fileFullPath))
            {
                var fileBytes = System.IO.File.ReadAllBytes(fileFullPath);
                return File(fileBytes, "application/octet-stream", Path.GetFileName(fileFullPath));
            }
            else
            {
                return NotFound();
            }
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.ListTeams = new SelectList(await genreService.GetGenres(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SongDTO song, IFormFile uploadedFile, IFormFile uploadedSong)
        {

            if (ModelState.IsValid)
            {
                string path = "/pictures/" + uploadedFile.FileName;

                using (var fileStream = new FileStream(_webHostEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }

                song.Img = path;

                string songPath = "/music/" + uploadedSong.FileName;

                using (var fileStream = new FileStream(_webHostEnvironment.WebRootPath + songPath, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }

                song.Path = songPath;

                await songService.CreateSong(song);
                return RedirectToAction("Index", "Home");
            }
            ViewBag.ListTeams = new SelectList(await genreService.GetGenres(), "Id", "Name", song.GenreId);
            return View(song);
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }
                await songService.DeleteSong(id);
                return View("~/Views/Home/Index.cshtml", await songService.GetSongs());
            }
            catch (ValidationException ex)
            {
                return NotFound(ex.Message);
            }
        }


    }
}
