using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Config
{
    public class ClinicalTrialConfiguration : IEntityTypeConfiguration<ClinicalTrial>
    {
        public void Configure(EntityTypeBuilder<ClinicalTrial> builder)
        {
            builder.Property(p => p.Title).IsRequired().HasMaxLength(100);
            builder.Property(p => p.StartDate).IsRequired();
            builder.Property(p => p.EndDate).IsRequired(false);
            builder.Property(p => p.Participants).IsRequired();
            builder.HasOne(pt => pt.Status).WithMany()
                .HasForeignKey(p => p.ClinicalTrialStatusId);
        }
    }
}