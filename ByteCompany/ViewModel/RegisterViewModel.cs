using ByteCompany.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ByteCompany.ViewModel
{
    public class RegisterViewModel
    {
        //private const int _minAge = 16;
        //private const int _maxAge = 65;

        public string Login { get; set; }

        [Required(ErrorMessage = "Имя обязательно")]
        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Фамилия обязательна")]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email обязателен")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Год рождения обязателен")]
        [Display(Name = "Год рождения")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Пароль обязателен")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Подтвердите пароль")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтвердить пароль")]
        public string PasswordConfirm { get; set; }

        public string FullName
        {
            get
            {
                return LastName + " " + FirstName;
            }
        }
    }
}
