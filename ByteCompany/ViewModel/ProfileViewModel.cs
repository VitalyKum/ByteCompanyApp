using ByteCompany.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ByteCompany.ViewModel
{
    public class ProfileViewModel
    { 
        public List<User> User { get; set; }
        public User CurrentUser { get; set; }
        public List<Bascket> Bascket { get; set; }
    }
}
