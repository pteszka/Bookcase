using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Owin;
using Microsoft.AspNetCore.Authorization;
using Bookcase.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Bookcase.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;

namespace Bookcase.Controllers
{
    public class AccountsController : Controller
    {
        private readonly BookcaseContext _context;

        public AccountsController(BookcaseContext context)
        {
            _context = context;
        }

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }


        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }
        

        //check if have to be async
        [AllowAnonymous]
        private async Task<bool> CheckIfUserIsInDBForLogin(LoginViewModel model)
        {
                await Task.Delay(150);
                var tempUser = _context.AppUser.
                    FirstOrDefault(u => u.Name == model.Name &&
                                        u.Password == model.Password);
                if (tempUser is null)
                    return false;
                return true;
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if(ModelState.IsValid)
            {
                bool isInDB = await CheckIfUserIsInDBForLogin(model);

                if (isInDB)
                {
                    var roleOfUser = _context.AppUser.FirstOrDefault(u => u.Name == model.Name).Role;
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, model.Name),
                        new Claim(ClaimTypes.Role as string, roleOfUser.ToString())
                    };

                    var claimsIdentity = new ClaimsIdentity(claims,
                        CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity));

                    return RedirectToAction("Index");
                }
                //napraw
                return NotFound();
            }
            return View(model);
        }

        [AllowAnonymous]
        private async Task<bool> CheckIfUserIsInDBForRegister(RegisterViewModel model)
        {
            var tempUser =  await _context.AppUser.
                FirstOrDefaultAsync(u => u.Name == model.Name &&
                                    u.Email == model.Email);
            if (tempUser is null)
                return false;
            return true;
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if(ModelState.IsValid)
            {
                bool isInDB = await CheckIfUserIsInDBForRegister(model);
                // check if user is in db
                if(!isInDB)
                {
                    var newUser = new AppUser {
                        Name = model.Name,
                        Email = model.Email,
                        Password = model.Password,
                        Role = Roles.Member
                    };
                    // we adding new user to db
                    await _context.AddAsync<AppUser>(newUser);
                    await _context.SaveChangesAsync();

                    return RedirectToAction("Login");
                }
                //napraw
                return NotFound();
            }
            // if modelstate is corrupted just refresh page
            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> LogoutAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

    }
}
