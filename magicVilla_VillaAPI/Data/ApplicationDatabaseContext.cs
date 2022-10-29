using magicVilla_VillaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace magicVilla_VillaAPI.Data
{
    public class ApplicationDatabaseContext : DbContext
    {
        public DbSet<Villa> Villas { get; set; }
    }
}
