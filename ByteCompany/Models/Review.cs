using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ByteCompany.Models
{
    public class Review
    {
        [Key]
        public int ID { get; set; }
        public string UserId { get; set; }
        public int ProductId { get; set; }
        
        [Required(ErrorMessage = "Поле обязательное для заполнения."), MinLength(10, ErrorMessage = "Введите не менне 10 символов")]
        public string TextReview { get; set; }
        [Required(ErrorMessage = "Укажите оценку.")]
        public int Rating { get; set; }

        [NotMapped]
        public User User { get; set; }
    }
}
