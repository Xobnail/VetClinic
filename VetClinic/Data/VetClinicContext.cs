using Microsoft.EntityFrameworkCore;
using VetClinic.Models;

namespace VetClinic.Data
{
    public class VetClinicContext : DbContext
    {
        public VetClinicContext(DbContextOptions<VetClinicContext> options) : base(options) 
        {
            Database.EnsureCreated();
        }
        public DbSet<Animal> Animals { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Vaccination> Vaccinations { get; set; }
    }
}
