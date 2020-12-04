using ByteCompany.Models;
using ByteCompany.ViewModel;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ByteCompany.Data
{
    public class ByteCompanyContext : IdentityDbContext<User>
    {
        public ByteCompanyContext(DbContextOptions<ByteCompanyContext> options) : base(options)
        { }

        public DbSet<Master> Masters { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<New> News { get; set; }
        public DbSet<Bascket> Baskets { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
    }
}
