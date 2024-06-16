using TravelInsurance.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;


namespace TravelInsurance.Repository.Ef
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options) { }

        public DbSet<Application> Applications { get; set; }
        public DbSet<AuditTrail> AuditTrails { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<MenusInRoles> MenusInRoles { get; set; }
        public DbSet<Merchant> Merchants { get; set; }
        public DbSet<NRCType> NRCTypes { get; set; }
        public DbSet<PremiumFee> PremiumFees { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<StateCode> StateCodes { get; set; }
        public DbSet<TownShipCode> TownShipCodes { get; set; }
        public DbSet<TransactionLog> TransactionLogs { get; set; }
        public DbSet<Traveller> Travellers { get; set; }

        public DbSet<User> Users { get; set; }

      

        public DbSet<Permission> Permission { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
          
            base.OnModelCreating(builder);
        }
    }
}
