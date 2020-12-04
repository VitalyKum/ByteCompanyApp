using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ByteCompany.Models
{
    public class ProductViewModel
    {
        public List<Product> Products { get; set; }
        public SelectList Sections { get; set; }
        public string ProductSection { get; set; }
        public string SearchString { get; set; }
        public IdentityRole IdentityRole { get; set; }
    }
}
