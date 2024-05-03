﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using Tune_Star.BLL.DTO;
using Tune_Star.BLL.Interfaces;
using Tune_Star.BLL.Services;
using Tune_Star.DAL.Entities;
using Tune_Star.Models;

namespace Tune_Star.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService userService;

        public UsersController(IUserService userServ)
        {
            userService = userServ;
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult RegistrationNotification()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterAsync(RegisterModel reg)
        {

            if (ModelState.IsValid)
            {
                int status = 0;
                if (reg.Login == "admin") status = 3;
                var userDTO = new UserDTO
                {
                    Login = reg.Login,
                    Password = reg.Password,
                    Status = status,
                };

                await userService.CreateUser(userDTO);
                return RedirectToAction("RegistrationNotification", "Users");
            }

            return View(reg);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel logon)
        {

            try
            {
                var users = await userService.GetUsers();

                if (users == null || users.Count() == 0 || logon.Login == null)
                {
                    ModelState.AddModelError("", "Wrong login or password!");
                    return View(logon);
                }

                var user = await userService.GetUser(logon.Login);

                if (user == null)
                {
                    ModelState.AddModelError("", "Wrong login or password!");
                    return View(logon);
                }

                if (user.Status < 1)
                {
                    ModelState.AddModelError("", "Your account has not been approved yet. Contact your administrator.");
                    return View(logon);
                }

                if (ModelState.IsValid)
                {

                    if (user != null)
                    {
                        string? salt = user.Salt;

                        byte[] password = Encoding.Unicode.GetBytes(salt + logon.Password);

                        byte[] byteHash = SHA256.HashData(password);

                        StringBuilder hash = new StringBuilder(byteHash.Length);
                        for (int i = 0; i < byteHash.Length; i++)
                            hash.Append(string.Format("{0:X2}", byteHash[i]));

                        if (user.Password != hash.ToString())
                        {
                            ModelState.AddModelError("", "Wrong login or password!");
                            return View(logon);
                        }

                        HttpContext.Session.SetString("Login", user.Login);

                        return RedirectToAction("Index", "Home");
                    }
                    else ModelState.AddModelError("", "Wrong login or password!");

                }
                return View(logon);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Your user either doesn't exist or wrong login or password");
                return View(logon);
            }


        }
    }
}
