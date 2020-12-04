using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ByteCompany.Models
{
    public class Question
    {
        [Key]
        public int ID { get; set; }
        public string UserId { get; set; }
        public int ProductId { get; set; }
        public string TextQuestion { get; set; }

        [NotMapped]
        public User User { get; set; }
    }
}
