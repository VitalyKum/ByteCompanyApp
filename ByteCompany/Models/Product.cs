using ByteCompany.ViewModel;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ByteCompany.Models
{
    public class Product
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Обязательное поле")]
        [StringLength(70, MinimumLength = 20, ErrorMessage = "Формат неправильный")]
        public string Name { get; set; }
        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "Неверный формат")]
        public decimal Price { get; set; }

        //[StringLength(200, MinimumLength = 20, ErrorMessage = "Формат неправильный")]
        //[Required(ErrorMessage = "Неверный формат")]
        public string Description { get; set; }

        [NotMapped]
        public int[] Ratings { get; set; }
        public byte[] Image { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfAdd { get; set; }
        public string Section { get; set; }

        public User User { get; set; }

        [NotMapped]
        public ICollection<byte[]> Images { get; set; }

        [NotMapped]
        public New News { get; set; }
        
        [NotMapped]
        public virtual ICollection<Review> Reviews { get; set; }

        [NotMapped]
        public virtual ICollection<Question> Questions { get; set; }

        [NotMapped]
        public virtual ProductViewModel ProductViewModel { get; set; }
    }
}
