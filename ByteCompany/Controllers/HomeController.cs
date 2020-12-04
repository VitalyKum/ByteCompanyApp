using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ByteCompany.Models;
using ByteCompany.Data;
using Microsoft.EntityFrameworkCore;

namespace ByteCompany.Controllers
{
    public class HomeController : Controller
    {
        private readonly ByteCompanyContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ByteCompanyContext context)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var product = from m in _context.Products
                          orderby m.DateOfAdd descending
                          select m;

            var productQwery = product.Take(6);
            return View(await productQwery.ToListAsync());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
