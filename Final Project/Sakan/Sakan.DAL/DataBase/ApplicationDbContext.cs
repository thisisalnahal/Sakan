using Microsoft.EntityFrameworkCore;
using Sakan.DAL.Entities;

namespace Sakan.DAL.DataBase
{
    internal class ApplicationDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=Sakan;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=true");
        }

        //finance 
        public DbSet<PaymentAuthorization> PaymentAuthorizations { get; set; }
        public DbSet<SecurityDeposit> SecurityDeposits { get; set; }
        public DbSet<FinancialTransaction> FinancialTransactions { get; set; }
        public DbSet<HostPayout> HostPayouts { get; set; }
        public DbSet<HostPenalty> HostPenalties { get; set; }
        public DbSet<HostPenaltyDeduction> HostPenaltyDeductions { get; set; }

    }
}
