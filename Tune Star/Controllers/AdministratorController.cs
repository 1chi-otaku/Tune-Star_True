using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Tune_Star.BLL.DTO;
using Tune_Star.BLL.Interfaces;

namespace Tune_Star.Controllers
{
    public class AdministratorController : Controller
    {

        private readonly IUserService userService;

        public AdministratorController(IUserService userServ)
        {
            userService = userServ;
        }

        public async Task<IActionResult> ApplicationList()
        {
            return View(await userService.GetUsers());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Verify(int userId)
        {
            try
            {
                if (userId == 0)
                {
                    return NotFound();
                }

                UserDTO user = await userService.GetUser((int)userId);
                user.Status = 1;
                await userService.UpdateUser(user);
            }
            catch (ValidationException ex)
            {
                return NotFound(ex.Message);
            }
            return View("ApplicationList", await userService.GetUsers());
        }

        public async Task<IActionResult> Disprove(int userId)
        {
            try
            {
                if (userId == 0)
                {
                    return NotFound();
                }

                await userService.DeleteUser((int)userId);  
                

               
            }
            catch (ValidationException ex)
            {
                return NotFound(ex.Message);
            }
            return View("ApplicationList", await userService.GetUsers());
        }
    }
}
