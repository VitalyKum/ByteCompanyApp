using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ByteCompany.Models
{
    public class User : IdentityUser
    {
        public int Age { get; set; }
        [Required(ErrorMessage = "Неправильный формат имени")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Неправильный формат фамилии")]
        public string LastName { get; set; }

        public string Login { get; set; }

        public string FullName
        {
            get
            {
                return LastName + " " + FirstName;
            }
        }
                
    }
}
