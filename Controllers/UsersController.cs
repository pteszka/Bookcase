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
using Microsoft.AspNetCore.Mvc;
using System;

namespace Bookcase.Controllers
{
    public class UsersController : Controller
    {
        private readonly BookcaseContext _context;

        public UsersController(BookcaseContext context)
        {
            _context = context;
        }

        // GET: Users
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index(int? pageNumber = 1, int pageSize = 4)
        {
            var userName = HttpContext.User.Identity.Name;
            var curUser = await _context.AppUser.FirstOrDefaultAsync(a => a.Name == userName);
            var data = _context.AppUser.Where(a => a.Id != curUser.Id).AsQueryable();
            return View(await PaginatedList<AppUser>.CreateAsync(data, pageNumber ?? 1, pageSize));
        }

        // POST: Users/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _context.AppUser.FindAsync(id);
            _context.AppUser.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _context.AppUser.Any(e => e.Id == id);
        }
        private async Task<AppUser> GetUserAsync(int id)
        {
            return await _context.AppUser.FirstOrDefaultAsync(u => u.Id == id);
        }

    }
}
