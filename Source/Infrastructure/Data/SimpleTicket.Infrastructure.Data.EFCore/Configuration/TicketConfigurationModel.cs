using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleTicket.Domain.Core.Entities;
using SimpleTicket.Domain.Core.Enums;

namespace SimpleTicket.Infrastructure.Data.EFCore.Configuration;

public class TicketConfigurationModel : IEntityTypeConfiguration<Ticket>
{
    public void Configure(EntityTypeBuilder<Ticket> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnType("uniqueidentifier")
            .ValueGeneratedNever();
        
        builder.Property(x => x.Summary);
        builder.Property(x => x.Note);
        builder.Property(x => x.CreatedAt);
        builder.Property(x => x.Deadline);
        builder.Property(x => x.FinishedAt);
        builder.Property(x => x.CategoryId);
        builder.Property(x => x.ResponsibleUsername);
        builder.Property(x => x.CreatorUsername);
        
        builder.Property(x => x.Priority)
            .HasConversion(x => x.HasValue ? x.Value.ToString() : null, 
                x => string.IsNullOrWhiteSpace(x) ? null :(Priority)Enum.Parse(typeof(Priority), x));
        
        builder.Property(x => x.Status)
            .HasConversion(x => x.ToString(), 
                x => (TicketStatus)Enum.Parse(typeof(TicketStatus), x));

        builder
            .HasMany(x => x.Activities)
            .WithOne()
            .HasForeignKey(fk => fk.TicketId);

        builder.Ignore(x => x.Events);
    }
}