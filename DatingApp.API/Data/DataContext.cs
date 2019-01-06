using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base (options)
        {
            
        }

        
        // this will be the table name, now we need to tell our application about this, and we use our Startup class to do so.
        public DbSet<Value> Values { get; set; } 
        public DbSet<User> Users { get; set; }

    }
}