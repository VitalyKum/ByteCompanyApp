using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace ByteCompany.Models
{
    public class Bascket
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string UserId { get; set; }
        public decimal BascketPrice { get; set; }
  
        [NotMapped]
        public Product Product { get; set; }

    }
}
