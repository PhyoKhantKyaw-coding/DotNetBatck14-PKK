using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetBatch14PKK.LoanTrackerDatabase
{
    public class AppDbContext : DbContext
    {
        private readonly SqlConnectionStringBuilder _connBuilder = new SqlConnectionStringBuilder()
        {
            DataSource = ".\\SA",
            InitialCatalog = "Loan",
            UserID = "sa",
            Password = "sa@123",
            TrustServerCertificate = true,
        };

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connBuilder.ConnectionString);
            }
        }

        public DbSet<CustomerModel> Customer { get; set; }
        public DbSet<MortgageLoanModel> LoanDetails { get; set; }
        public DbSet<PaymentModel> Payment { get; set; }
        public DbSet<LateFeeRuleModel> LateFee { get; set; }
    }
}
