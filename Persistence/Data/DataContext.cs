using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence.Config;

namespace Persistence.Data
{
    public class DataContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<ClinicalTrial> ClinicalTrials { get; set; }
        public DbSet<ClinicalTrialStatus> ClinicalTrialStatus { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ClinicalTrialConfiguration).Assembly);
        }
    }
}