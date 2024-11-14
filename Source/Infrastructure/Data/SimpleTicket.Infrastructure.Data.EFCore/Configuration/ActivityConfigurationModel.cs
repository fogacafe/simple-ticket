using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Identity.Client;
using SimpleTicket.Domain.Core.Entities;

namespace SimpleTicket.Infrastructure.Data.EFCore.Configuration;

public class ActivityConfigurationModel : IEntityTypeConfiguration<Activity>
{
    public void Configure(EntityTypeBuilder<Activity> builder)
    {
        builder.ToTable("ACTIVITIES");
        
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasColumnType("uniqueidentifier")
            .ValueGeneratedNever()
            .IsRequired();

        builder.Property(x => x.CreatedAt).HasColumnType("datetime").IsRequired();
        builder.Property(x => x.Note).HasColumnType("varchar(250)").IsRequired();
        
        builder.Property(x => x.TicketId)
            .HasColumnType("uniqueidentifier");

        

    }
}