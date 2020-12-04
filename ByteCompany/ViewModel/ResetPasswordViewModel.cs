using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ByteCompany.ViewModel
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(19, ErrorMessage = "Минимум 6 символов, максимум 19", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Подтвердите пароль")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }
}
