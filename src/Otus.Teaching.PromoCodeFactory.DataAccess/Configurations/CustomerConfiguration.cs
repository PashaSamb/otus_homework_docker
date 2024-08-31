using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement;

namespace Otus.Teaching.PromoCodeFactory.DataAccess.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property<string>("FirstName").IsRequired().HasMaxLength(50);
            builder.Property<string>("LastName").IsRequired().HasMaxLength(50);
            builder.Property<string>("Email").IsRequired().HasMaxLength(50);


            builder.HasMany<PromoCode>(e => e.PromoCodes)
                   .WithOne()
                   .IsRequired();

        }
    }
}
