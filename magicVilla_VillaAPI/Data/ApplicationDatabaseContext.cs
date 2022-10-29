using magicVilla_VillaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace magicVilla_VillaAPI.Data
{
    public class ApplicationDatabaseContext : DbContext
    {
        public ApplicationDatabaseContext(DbContextOptions<ApplicationDatabaseContext> options) :base(options)
        {

        }
        public DbSet<Villa> Villas { get; set; }
    }
}
