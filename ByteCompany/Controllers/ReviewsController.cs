using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ByteCompany.Data;
using ByteCompany.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ByteCompany.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly ByteCompanyContext _context;
        
        public ReviewsController(UserManager<User> userManager, ByteCompanyContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<IActionResult> Create(int? productId)
        {

            if (productId == null || !await _context.Products.AnyAsync(p => p.ID == productId))
            {
                return NotFound();
            }
            
            Review review = new Review
            {
                ProductId = (int)productId
            };
            
            return View(review);
        }

        [HttpPost]
        public async Task<IActionResult> Create(int? productId, [Bind("Rating, TextReview")] Review review)
        {
            if (productId == null)
                return NotFound();

            if (!_context.Products.Any(p => p.ID == productId))
                return NotFound();

            review.UserId = _userManager.GetUserId(User);
            review.ProductId = (int)productId;

            if (_context.Reviews.Any(r => r.UserId == review.UserId &&
                                             r.ProductId == review.ProductId))
            {
                ModelState.AddModelError("", "Вы уже оставляли отзыв к данному товару");
                return View(review);
            }
            if (ModelState.IsValid)
            {            
                try
                {
                    await _context.AddAsync(review);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException ex)
                {
                    Console.WriteLine("", ex.Message);
                }
            }
           
            return RedirectToAction("Details", "Products", new { Id = productId });
        }
    }
}