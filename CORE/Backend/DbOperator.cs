using CORE.Entities;
using CORE.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CORE.Backend
{
    public class DbOperator : IdentityDbContext<User, Role, int>
    {
        public DbOperator(DbContextOptions<DbOperator> options) : base(options)
        {
        }
        public DbSet<Maintenance> Maintenances { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<User>().ToTable("User");
            //modelBuilder.Entity<StatusBase>().ToTable("Status");
            //modelBuilder.Entity<Maintenance>().ToTable("Maintenance");

            modelBuilder.Entity<StatusBase>().Property(b => b.CreateDate).HasDefaultValueSql("GETDATE()");
            modelBuilder.Entity<StatusBase>().Property(b => b.IsDeleted).HasDefaultValue(false);
        }
    }

}
