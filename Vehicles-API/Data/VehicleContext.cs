using Microsoft.EntityFrameworkCore;
using Vehicles_API.Models;

namespace Vehicles_API.Data
{
    public class VehicleContext : DbContext
    {
        public DbSet<Vehicle> Vehicles =>Set<Vehicle>();
        public DbSet<Manufacturer> Manufacturers => Set<Manufacturer>();
        public VehicleContext(DbContextOptions options) : base(options)
        {
            
        }


    }
}