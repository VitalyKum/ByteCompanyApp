﻿using ByteCompany.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ByteCompany.Data
{
    public class UserIdentityContext : IdentityDbContext<User>
    {
        public UserIdentityContext(DbContextOptions<UserIdentityContext> options)
            : base(options)
        {
        }
    }
}
