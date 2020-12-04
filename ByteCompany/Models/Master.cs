using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ByteCompany.Models
{
    public class Master
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Обязательное поле")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Формат имени неправильный")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Обязательное поле")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Формат неправильный")]
        public string SurName { get; set; }
        [Required(ErrorMessage = "Обязательное поле")]
        [StringLength(70, MinimumLength = 20, ErrorMessage = "Формат неправильный")]
        public int Experiance { get; set; }
        [Required(ErrorMessage = "Обязательное поле")]
        [StringLength(70, MinimumLength = 20, ErrorMessage = "Формат не совпадает")]
        public string About { get; set; }
        public byte[] Avatar { get; set; }
        [NotMapped]
        public string FullName
        {
            get
            {
                return Name + " " + SurName;   
            }
        }
        public User User { get; set; }
    }
}
