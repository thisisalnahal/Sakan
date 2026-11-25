using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sakan.DAL.Entities;

namespace Sakan.DAL.DataBase.Configurations
{
    internal class ConversationConfiguration : IEntityTypeConfiguration<Conversation>
    {
        public void Configure(EntityTypeBuilder<Conversation> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.CreatedAt)
                   .IsRequired();

            builder.HasIndex(c => c.BookingId)
                   .IsUnique();

            builder.HasOne(c => c.Booking)
                   .WithOne()
                   .HasForeignKey<Conversation>(c => c.BookingId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
