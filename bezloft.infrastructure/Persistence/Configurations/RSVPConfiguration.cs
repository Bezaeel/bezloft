using bezloft.core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bezsoft.Infrastructure.Persistence.Configurations;

public class RSVPConfiguration : IEntityTypeConfiguration<RSVP>
{
    public void Configure(EntityTypeBuilder<RSVP> builder)
    {
        builder.HasKey(x => new {x.UserId, x.EventId});

        builder.HasOne(x => x.User)
            .WithMany(x => x.RSVPs)
            .HasForeignKey(x => x.UserId);

        builder.HasOne(x => x.Event)
            .WithMany(x => x.RSVPs)
            .HasForeignKey(x => x.EventId);
    }
}