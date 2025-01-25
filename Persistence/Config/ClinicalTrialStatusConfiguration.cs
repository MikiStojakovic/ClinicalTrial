using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Config
{
    public class ClinicalTrialStatusConfiguration: IEntityTypeConfiguration<ClinicalTrialStatus>
    {
        public void Configure(EntityTypeBuilder<ClinicalTrialStatus> builder)
        {
            builder.Property(p => p.Type).IsRequired().HasMaxLength(50);
        }
    }
}