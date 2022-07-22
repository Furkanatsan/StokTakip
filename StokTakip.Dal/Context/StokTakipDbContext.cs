using Microsoft.EntityFrameworkCore;
using StokTakip.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StokTakip.Dal.Context
{
    public class StokTakipDbContext:DbContext
    {
       
        public StokTakipDbContext(DbContextOptions<StokTakipDbContext> options):base(options)
        {
                
        }
        public StokTakipDbContext()
        {

        }
        public DbSet<Userr> Userrs { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Book> Books { get; set; }
    }
}
