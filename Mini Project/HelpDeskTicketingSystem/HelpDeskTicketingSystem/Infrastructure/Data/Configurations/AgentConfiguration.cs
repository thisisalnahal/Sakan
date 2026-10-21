using HelpDeskTicketingSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HelpDeskTicketingSystem.Infrastructure.Data
{
    internal class AgentConfiguration : IEntityTypeConfiguration<Agent>
    {
        public void Configure(EntityTypeBuilder<Agent> builder)
        {
            builder.Property(a => a.Department)
                .HasConversion<string>()
                .HasMaxLength(30);
        }
    }
}