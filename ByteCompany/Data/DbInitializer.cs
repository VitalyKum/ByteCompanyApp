using ByteCompany.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ByteCompany.Data
{
    public static class DbInitializer
    {
        public static void Intialize(IServiceProvider serviceProvider)
        {
            using (var context = new ByteCompanyContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<ByteCompanyContext>>()))
            {
                if (context.Masters.Any())
                {
                    return;
                }
                if (context.Products.Any())
                {
                    return;
                }

                context.Products.AddRange(
                    new Product
                    {
                        Name = "Computer",
                        Price = 3,
                        Section = "Аксессуары",
                        DateOfAdd = DateTime.Parse("22-02-2020-23:00"),
                        Description = "euhfouhfew"
                    }
                 );
                context.SaveChanges();
            }

            //if (context.Masters.Any())
            //{
            //    return;
            //}

            //var masters = new Master[]
            //{

            //};

            //var product = new Product[]
            //{

            //};
        }
    }
}
