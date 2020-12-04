using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ByteCompany.Data;
using ByteCompany.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ByteCompany.Controllers
{
    public class AnswersController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly ByteCompanyContext _context;

        public AnswersController(UserManager<User> userManager, ByteCompanyContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var answers = from a in _context.Answers
                          select a;

            return View(answers);
        }

        public async Task<IActionResult> Create(int? productId)
        {
            if (productId == null || !await _context.Products.AnyAsync(p => p.ID == productId))
            {
                return NotFound();
            }

            Answer answer = new Answer
            {
                ProductId = (int)productId
            };

            return View(answer);
        }
        [HttpPost, ActionName("Create")]
        public async Task<IActionResult> CreatePost(int? productId, [Bind("TextAnswer")] Answer answer)
        {
            if (productId == null)
                return NotFound();

            if (!_context.Products.Any(p => p.ID == productId))
                return NotFound();

            answer.ProductId = (int)productId;
            answer.Userid = _userManager.GetUserId(User);

            if (ModelState.IsValid)
            {
                try
                {
                    await _context.AddAsync(answer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException ex)
                {
                    Console.WriteLine("", ex.Message);
                }
            }

            return RedirectToAction("Index", "Answers", new { Id = productId});
        }
    }
}