using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Otus.Teaching.PromoCodeFactory.Core.Domain.Administration;
using Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement;

namespace Otus.Teaching.PromoCodeFactory.DataAccess.Configurations
{
    public class PromoCodeConfiguration : IEntityTypeConfiguration<PromoCode>
    {
        public void Configure(EntityTypeBuilder<PromoCode> builder)
        {
            builder.HasKey(x=> x.Id);

            builder.Property<string>("Code").IsRequired().HasMaxLength(50);
            builder.Property<string>("ServiceInfo").IsRequired().HasMaxLength(50);
            builder.Property<string>("PartnerName").IsRequired().HasMaxLength(50);

            builder.HasOne(x => x.Preference)
                   .WithOne()
                   .HasForeignKey<Preference>(e => e.PromoCodeId)
                   .IsRequired(); 

            builder.HasOne(x => x.PartnerManager)
                   .WithOne()
                   .HasForeignKey<Employee>(e => e.PromoCodeId)
                   .IsRequired();
        }
    }
}
