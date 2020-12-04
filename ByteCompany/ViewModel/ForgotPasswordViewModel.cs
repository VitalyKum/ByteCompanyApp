using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ByteCompany.ViewModel
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "Это поле обязательно")]
        [EmailAddress(ErrorMessage = "Неправильный формат адреса эл. почты")]
        public string Email { get; set; }
    }
}
