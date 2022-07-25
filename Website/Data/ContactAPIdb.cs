using Microsoft.EntityFrameworkCore;
using Website.Models;

namespace Website.Data
{
    public class ContactAPIdb : DbContext
    {
        public ContactAPIdb(DbContextOptions<ContactAPIdb> options) : base(options)
        {
        }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Product> Products { get; set; }
       public DbSet<Order> Orders { get; set; }
    }
}
