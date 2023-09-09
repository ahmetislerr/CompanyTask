using CompanyTask.Domain.Entities;
using CompanyTask.Infrastructure.EntitiesConfig;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CompanyTask.Infrastructure.DbContext
{
    public class ApplicationDbContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Statu> Status { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Title> Titles { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<CompanySector> CompanySectors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AddressConfig())
                        .ApplyConfiguration(new AppUserConfig())
                        .ApplyConfiguration(new CityConfig())
                        .ApplyConfiguration(new CountryConfig())
                        .ApplyConfiguration(new DepartmentConfig())
                        .ApplyConfiguration(new DistrictConfig())
                        .ApplyConfiguration(new StatuConfig())
                        .ApplyConfiguration(new TitleConfig())
                        .ApplyConfiguration(new CompanyConfig())
                        .ApplyConfiguration(new CompanySectorConfig());

            foreach (var foreignKey in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}
