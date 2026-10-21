using HelpDeskTicketingSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HelpDeskTicketingSystem.Infrastructure.Data
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(u => u.Id);

            builder.Ignore(u => u.Role);

            builder.Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(u => u.PasswordHash)
                .IsRequired()
                .HasMaxLength(500);

            builder.HasIndex(u => u.Email)
                .IsUnique();

            builder.HasDiscriminator<string>("UserType")
                .HasValue<Client>("Client")
                .HasValue<Dispatcher>("Dispatcher")
                .HasValue<Agent>("Agent")
                .HasValue<Admin>("Admin");
        }
    }
}