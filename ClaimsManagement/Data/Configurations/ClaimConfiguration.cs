using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data
{
    internal class ClaimConfiguration : IEntityTypeConfiguration<Claim>
    {
        public void Configure(EntityTypeBuilder<Claim> builder)
        {
            builder
                .HasOne(c => c.User)
                .WithMany(u => u.Claims)
                .HasForeignKey(c => c.UserId);

            builder
                .Property(c => c.Title)
                .IsRequired();

            builder
               .Property(c => c.Description)
               .IsRequired();

            builder
              .Property(c => c.FlightNumber)
              .IsRequired();

            builder
              .Property(c => c.Category)
              .IsRequired();

            builder
              .Property(c => c.CreatedAt)
              .ValueGeneratedOnAdd();
              //.IsRequired();

            builder
              .Property(c => c.Airline)
              .IsRequired();

            builder
              .Property(c => c.DepartureAirport)
              .IsRequired();

            builder
              .Property(c => c.ArrivalAirport)
              .IsRequired();

            builder
              .Property(c => c.FlightDate)
              .IsRequired();

            builder
              .Property(c => c.BoardingPassImage)
              .IsRequired();

            builder
                .Property(c => c.CountryCode)
                .IsRequired();

            builder
              .Property(c => c.PhoneNumber)
              .IsRequired();
        }
    }
}