using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RepositoryModel.ConstEnum;
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
        public DbSet<LocationRoi> LocationRois { get; set; }

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

			modelBuilder.Entity<Developer>().HasData(
		   new Developer
		   {
			   Id = "dev1",
			   Name = "Developer A"
		   },
		   new Developer
		   {
			   Id = "dev2",
			   Name = "Developer B"
		   }
	   );

			modelBuilder.Entity<Unit>().HasData(
		new Unit
		{
			Id = "unit1",
			Location = "New Cairo",
			Name = "Luxury Apartment",
			Available = Avalibility.Available,
			AvailableShares = 10,
			DownPayment = 50000.00m,
			StartUnitPrice = 1000000.00m,
			CurrentUnitPrice = 1200000.00m,
			CurrentUnitROI = 15.00m,
			MonthlyPayment = 15000.00m,
			AvilableDate = new DateTime(2024, 1, 1),
			ExitDate = new DateTime(2027, 1, 1),
			DeveloperId = "dev1"
		},
		new Unit
		{
			Id = "unit2",
			Location = "Sheikh Zayed",
			Name = "Family Villa",
			Available = Avalibility.Funded,
			AvailableShares = 5,
			DownPayment = 100000.00m,
			StartUnitPrice = 5000000.00m,
			CurrentUnitPrice = 5500000.00m,
			CurrentUnitROI = 10.00m,
			MonthlyPayment = 50000.00m,
			AvilableDate = new DateTime(2024, 6, 1),
			ExitDate = new DateTime(2030, 6, 1),
			DeveloperId = "dev2"
		}
	);

			// Seed UnitDescriptions
			modelBuilder.Entity<UnitDescription>().HasData(
				new UnitDescription
				{
					Id = 1,
					UnitId = "unit1",
					NumberOfBathrooms = 2,
					NumberOfBedrooms = 3,
					Area = 120
				},
				new UnitDescription
				{
					Id = 2,
					UnitId = "unit2",
					NumberOfBathrooms = 4,
					NumberOfBedrooms = 5,
					Area = 350
				}
			);

			// Seed UnitViews
			modelBuilder.Entity<UnitView>().HasData(
				new UnitView
				{
					Id = 1,
					UnitId = "unit1",
					Name = ViewSpecial.Parks
				},
				new UnitView
				{
					Id = 2,
					UnitId = "unit1",
					Name = ViewSpecial.ClubHouse
				},
				new UnitView
				{
					Id = 3,
					UnitId = "unit2",
					Name = ViewSpecial.Gym
				},
				new UnitView
				{
					Id = 4,
					UnitId = "unit2",
					Name = ViewSpecial.Mall
				}
			);
		}

    }
}
