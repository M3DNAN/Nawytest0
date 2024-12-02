using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RepositoryModel.Models;

namespace DataBaseModel
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Developer> Developers { get; set; }

        public DbSet<CompanyInstallment> CompanyInstallments { get; set; }

        public DbSet<CompanyTransaction> CompanyTransactions { get; set; }

        public DbSet<SharesTransaction> SharesTransactions { get; set; }
        public DbSet<Installment> Installments { get; set; }
        public DbSet<Unit> Units { get; set; }

        public DbSet<UnitDescription> UnitDescriptions { get; set; }

        public DbSet<UnitImages> UnitImages { get; set; }

        public DbSet<Token> Tokens { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Set default value for AvalibaleSharesNumber as 3
            modelBuilder.Entity<SharesTransaction>()
                .Property(st => st.AvalibaleSharesNumber)
                .HasDefaultValue(3);

            modelBuilder.Entity<Unit>()
       .Property(e => e.DownPayment)
       .HasColumnType("decimal(10, 2)");

            modelBuilder.Entity<Unit>()
       .Property(e => e.StartUnitPrice)
       .HasColumnType("decimal(10, 2)");

            modelBuilder.Entity<Unit>()
       .Property(e => e.CurrentUnitPrice)
       .HasColumnType("decimal(10, 2)");

            modelBuilder.Entity<Unit>()
       .Property(e => e.CurrentUnitROI)
       .HasColumnType("decimal(10, 2)");

            modelBuilder.Entity<Unit>()
 .Property(e => e.MonthlyPayment)
 .HasColumnType("decimal(10, 2)");

            
            modelBuilder.Entity<SharesTransaction>()
 .Property(e => e.TotalPaidTillNow)
 .HasColumnType("decimal(10, 2)");

            
            modelBuilder.Entity<Installment>()
.Property(e => e.InstallmentPrice)
.HasColumnType("decimal(10, 2)");

            modelBuilder.Entity<CompanyTransaction>()
  .Property(e => e.DownPayment)
  .HasColumnType("decimal(10,2)");

            modelBuilder.Entity<CompanyTransaction>()
.Property(e => e.DownPayment)
.HasColumnType("decimal(10, 2)");

            modelBuilder.Entity<CompanyInstallment>()
.Property(e => e.InstallmentPrice)
.HasColumnType("decimal(10, 2)");
           

        }
    }
}
