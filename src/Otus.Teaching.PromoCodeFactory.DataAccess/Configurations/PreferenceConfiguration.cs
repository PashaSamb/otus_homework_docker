using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement;

namespace Otus.Teaching.PromoCodeFactory.DataAccess.Configurations
{
    public class PreferenceConfiguration : IEntityTypeConfiguration<Preference>
    {
        public void Configure(EntityTypeBuilder<Preference> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property<string>(x => x.Name).IsRequired().HasMaxLength(50);

           
            builder.HasMany(x => x.Customers)
            .WithMany(x => x.Preferences)
            .UsingEntity("CustomerPreference",
                l => l.HasOne(typeof(Customer))
                      .WithMany()
                      .HasForeignKey("CustomerId")
                      .OnDelete(DeleteBehavior.Cascade)
                      .HasPrincipalKey(nameof(Customer.Id)),
                r => r.HasOne(typeof(Preference))
                     .WithMany()                 
                     .HasForeignKey("PreferenceId")
                     .OnDelete(DeleteBehavior.NoAction)
                     .HasPrincipalKey(nameof(Preference.Id)),
                j => j.HasKey("CustomerId", "PreferenceId"));      

        }
    }
}
