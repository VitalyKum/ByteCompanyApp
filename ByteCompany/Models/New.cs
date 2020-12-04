using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ByteCompany.Models
{
    public class New
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Обязательное поле")]
        [StringLength(70, MinimumLength = 20, ErrorMessage = "Формат неправильный")]
        public string HeadLine { get; set; }
        [Required(ErrorMessage = "Обязательное поле")]
        [StringLength(500, MinimumLength = 20, ErrorMessage = "Формат неправильный")]
        public string Text { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime DateAddedNews { get; set; }
        public string CompanyNew { get; set; }
    }
}
