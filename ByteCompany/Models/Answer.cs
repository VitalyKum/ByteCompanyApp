using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ByteCompany.Models
{
    public class Answer
    {
        [Key]
        public int ID { get; set; }
        public string Userid { get; set; }
        public int ProductId { get; set; }

        [Required]
        [StringLength(70, MinimumLength = 20, ErrorMessage = "Формат ")]
        public string TextAnswer { get; set; }

        public User User { get; set; }
    }
}
