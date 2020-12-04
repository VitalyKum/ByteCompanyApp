using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ByteCompany.Controllers
{
    public class ChatController : Controller
    {
        public IActionResult Chat()
        {
            return View("ChatForByteCompany/Index");
        }
    }
}