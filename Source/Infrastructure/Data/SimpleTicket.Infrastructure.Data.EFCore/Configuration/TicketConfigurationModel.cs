using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleTicket.Domain.Core.Entities;
using SimpleTicket.Domain.Core.Enums;

namespace SimpleTicket.Infrastructure.Data.EFCore.Configuration;

public class TicketConfigurationModel : IEntityTypeConfiguration<Ticket>
{
    public void Configure(EntityTypeBuilder<Ticket> builder)
    {
        builder.ToTable("TICKETS");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnType("uniqueidentifier")
            .ValueGeneratedNever()
            .IsRequired();
        
        builder.Property(x => x.Summary).HasColumnType("varchar(250)").IsRequired();
        builder.Property(x => x.Note).HasColumnType("nvarchar(1000)");
        builder.Property(x => x.CreatedAt).HasColumnType("datetime").IsRequired();
        builder.Property(x => x.Deadline).HasColumnType("datetime");
        builder.Property(x => x.FinishedAt).HasColumnType("datetime");
        builder.Property(x => x.CategoryId).HasColumnType("uniqueidentifier");
        builder.Property(x => x.ResponsibleUsername).HasColumnType("varchar(100)");
        builder.Property(x => x.CreatorUsername).HasColumnType("varchar(100)").IsRequired();
        
        builder.Property(x => x.Priority)
            .HasColumnType("varchar(20)")
            .HasConversion(x => x.HasValue ? x.Value.ToString() : null, 
                x => string.IsNullOrWhiteSpace(x) ? null :(Priority)Enum.Parse(typeof(Priority), x));
        
        builder.Property(x => x.Status)
            .HasColumnType("varchar(20)")
            .HasConversion(x => x.ToString(), 
                x => (TicketStatus)Enum.Parse(typeof(TicketStatus), x));

        builder
            .HasMany(x => x.Activities)
            .WithOne()
            .HasForeignKey(fk => fk.TicketId);

        builder.Ignore(x => x.Events);
    }
}