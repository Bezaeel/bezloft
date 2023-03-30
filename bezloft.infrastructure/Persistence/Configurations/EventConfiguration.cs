using bezloft.core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bezsoft.Infrastructure.Persistence.Configurations;

public class EventConfiguration : IEntityTypeConfiguration<Event>
{
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        builder.HasOne(x => x.ContactPerson)
            .WithMany(x => x.Events)
            .HasForeignKey(x => x.ContactPersonId);
    }
}