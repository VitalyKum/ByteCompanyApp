using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ByteCompany.Data;
using ByteCompany.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using ByteCompany.ViewModel;
using Microsoft.AspNetCore.Hosting;

namespace ByteCompany.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ByteCompanyContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public ProductsController(ByteCompanyContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET: Products
        public async Task<IActionResult> Index(string productSection, string searchString)
        {
            IQueryable<string> sectionQuery = from m in _context.Products
                                             orderby m.Section
                                             select m.Section;

            var products = from m in _context.Products
                           select m;

           if (!String.IsNullOrEmpty(searchString) || !String.IsNullOrEmpty(productSection))
            {
                if (!String.IsNullOrEmpty(searchString))
                {
                    products = products.Where(s => s.Name.Contains(searchString));
                }

                if (!String.IsNullOrEmpty(productSection))
                {
                    products = products.Where(x => x.Section == productSection);
                }

                var productSectionVM = new ProductViewModel
                {
                    Sections = new SelectList(await sectionQuery.Distinct().ToListAsync()),
                    Products = await products.ToListAsync()
                };

                return View(productSectionVM);
            }
            else
            {
                products = products.OrderByDescending(o => o.DateOfAdd);
                var productSectionVM = new ProductViewModel
                {
                    Sections = new SelectList(await sectionQuery.Distinct().ToListAsync()), 
                    Products = await products.ToListAsync()
                };
                
                return View(productSectionVM);
            }
           
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.ID == Id);

            product.Reviews = await _context.Reviews.Where(r => r.ProductId == (int)Id).ToListAsync();
            foreach(Review review in product.Reviews)
            {
                review.User = await _userManager.FindByIdAsync(review.UserId);
            }

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Price,Description,Image,DateOfAdd,Section")] Product product, IFormFile imageFiles)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    if (imageFiles != null)
                    {
                        //long size = imageFiles.Sum(i => i.Length);

                        //foreach (var formFile in imageFiles)
                        //{
                        //    if (formFile.Length > 0)
                        //    {
                        //        var filePath = Path.GetTempFileName();

                        //        using (var stream = System.IO.File.Create(filePath))
                        //        {
                        //            var files = formFile.CopyToAsync(stream);
                        //        }   

                        //    }
                        //}

                        byte[] imageData = null;
                        using (var binaryReader = new BinaryReader(imageFiles.OpenReadStream()))
                            imageData = binaryReader.ReadBytes((int)imageFiles.Length);
                        product.Image = imageData;
                    }

                    product.DateOfAdd = DateTime.Now;
                    _context.Add(product);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes. " +
            "Try again, and if the problem persists " +
            "see your system administrator.");
            }
            return View(product);
        }

        // GET: Products/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)

        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id, [Bind("ID,Name,Price,Description,Section,Image,DateOfAdd")] Product product, IFormFile file)
        {
            if (id != product.ID)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    if (file != null)
                    {
                        byte[] imageData = null;
                        using (var binaryReader = new BinaryReader(file.OpenReadStream()))
                            imageData = binaryReader.ReadBytes((int)file.Length);
                        product.Image = imageData;
                    }

                    _context.Update(product);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
                        
            return View(product);
        }

        // GET: Products/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.ID == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        //[Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.RemoveRange(product);
            
            var reviews = await _context.Reviews.Where(r => r.ID == product.ID).ToListAsync();
            _context.Reviews.RemoveRange(reviews);
            
            await _context.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ID == id);
        }
              
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToBascket([Bind("ID,Name,Price,Description,Image,Section,DateOfAdd")] Product product, int? Id)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);                
                
                if (user != null && product != null)
                {
                    product.Reviews = await _context.Reviews.Where(r => r.ProductId == (int)Id).ToListAsync();
                    foreach (Review review in product.Reviews)
                        review.User = await _userManager.FindByIdAsync(review.UserId);

                    var bascket = new Bascket
                    {
                        UserId = user.Id,
                        ProductId = product.ID,
                        BascketPrice = product.Price
                    };
                    _context.Add(bascket);
                    await _context.SaveChangesAsync();
                }
            }
            catch (DbUpdateException)
            {
                Console.WriteLine("", "Unable to save changes. " +
            "Try again, and if the problem persists " +
            "see your system administrator.");
            }
            return View("Details", product);
        }
    }
}
