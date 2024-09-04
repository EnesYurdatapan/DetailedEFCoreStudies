using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Lesson1
{
    public class ExampleDbContext:DbContext
    {
        public DbSet<Product> Products { get; set; }


        //OnConfiguring metodu, Ef Core tool'unu yapılandırmak için kullandığımız metottur.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=ExampleDb;Trusted_Connection=True");
           // Provider
           // ConnectionString
           //Lazy Loading
           //vb.
        }
    }
}


