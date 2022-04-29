using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    //Context: DbTabloları ile proje classlarını bağlama(ilişkilendirme)
    //DbContext EF ile birlikte gelir
    public class NortwindContext:DbContext
    {
        // Hangi veri tabanı belirteci oncon vs.
        // @ eklemek \ fonk. etkisiz kılar
        //;Trusted_Connection=True şifresiz onayla demek
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Northwind;Trusted_Connection=True");
        }
        //hangi nesnenin db'deki hangi nesneye karşılık geliyor
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
    }
}
