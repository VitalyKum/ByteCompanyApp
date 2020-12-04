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
    public class QuestionsController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly ByteCompanyContext _context;

        public QuestionsController(UserManager<User> userManager, ByteCompanyContext context)
        {
            _userManager = userManager;
            _context = context;
        }


        public async Task<IActionResult> Index()
        {
            var questions = from q in _context.Questions
                            select q;
            
            foreach (Question question in questions)
            {
                question.User = await _userManager.FindByIdAsync(question.UserId);
            }

            return View(questions);
        }

        public async Task<IActionResult> Create(int? productId)
        {
            if (productId == null || !await _context.Products.AnyAsync(p => p.ID == productId))
            {
                return NotFound();
            }

            Question questions = new Question
            {
                ProductId = (int)productId
            };

            return View(questions);
        }

        [HttpPost, ActionName("Create")]
        public async Task<IActionResult> CreatePost(int? productId, [Bind("TextQuestion")] Question question)
        {
            if (productId == null)
                return NotFound();

            if (!_context.Products.Any(p => p.ID == productId))
                return NotFound();

            question.UserId = _userManager.GetUserId(User);
            question.ProductId = (int)productId;
            
            if (ModelState.IsValid)
            {
                try
                {
                    await _context.AddAsync(question);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException ex)
                {
                    Console.WriteLine("", ex.Message);
                }
            }

            return RedirectToAction("Index", "Questions", new { Id = productId });
        }
    }
}