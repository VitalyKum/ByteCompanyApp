using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ByteCompany.Data;
using ByteCompany.Models;
using ByteCompany.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace ByteCompany.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ByteCompanyContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public ProfileController(ByteCompanyContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult CreateProductInBuscket()
        {
            return View("Profile");
        }

        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            User user = await _userManager.GetUserAsync(User);
            //IdentityRole role = user.IdentityRole;
            
            var bascket = from b in _context.Baskets select b;
            bascket = bascket.Where(x => x.UserId == user.Id);
            
            foreach (Bascket b in bascket)
                b.Product = await _context.Products.FirstOrDefaultAsync(x => x.ID == b.ProductId);

            ProfileViewModel model = new ProfileViewModel
            {
                CurrentUser = user,
                Bascket = await bascket.ToListAsync()
            };
            return View("Profile", model);
        }

        //public IActionResult PersonalData()
        //{
        //    return View("PersonalData");
        //}
        /*
         * Папа буб буб
         * Не знаю, если хочешь то можешь посмотреть мой проект и что нибудь здесь сделать
         */

        [HttpPost]
        public async Task<IActionResult> AddToBascket(ProfileViewModel model, Product product)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            User applicationUser = await _userManager.GetUserAsync(User);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult  Chat()
        {
            return View();
        }
    }
}