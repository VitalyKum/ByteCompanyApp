using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ByteCompany.Data;
using ByteCompany.Models;
using System.IO;
using System.Web;
using Microsoft.AspNetCore.Http;

namespace ByteCompany.Controllers
{
    public class MastersController : Controller
    {
        private readonly ByteCompanyContext _context;

        public MastersController(ByteCompanyContext context)
        {
            _context = context;
        }

        // GET: Masters
        public async Task<IActionResult> Index()
        {
            return View(await _context.Masters.ToListAsync());
        }

        // GET: Masters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var master = await _context.Masters
                .FirstOrDefaultAsync(m => m.ID == id);
            if (master == null)
            {
                return NotFound();
            }

            return View(master);
        }

        // GET: Masters/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Masters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,SurName,Experiance,About")] Master master, IFormFile file)
        {
            if (file != null)
            {
                byte[] imageData = null;

                using (var binaryReader = new BinaryReader(file.OpenReadStream()))
                {
                    imageData = binaryReader.ReadBytes((int)file.Length);
                }
                master.Avatar = imageData;
            }

            if (ModelState.IsValid)
            {
                _context.Add(master);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(master);
        }

        // GET: Masters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var master = await _context.Masters.FindAsync(id);
            if (master == null)
            {
                return NotFound();
            }
            return View(master);
        }

        // POST: Masters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,SurName,Experiance,About")] Master master)
        {
            if (id != master.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(master);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MasterExists(master.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(master);
        }

        // GET: Masters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var master = await _context.Masters
                .FirstOrDefaultAsync(m => m.ID == id);
            if (master == null)
            {
                return NotFound();
            }

            return View(master);
        }

        // POST: Masters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var master = await _context.Masters.FindAsync(id);
            _context.Masters.Remove(master);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MasterExists(int id)
        {
            return _context.Masters.Any(e => e.ID == id);
        }
    }
}
