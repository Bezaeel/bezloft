using bezloft.core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bezsoft.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasData(
            new User { Id= Guid.Parse("2a5a05ad-82f9-48a3-869c-2818b1ac1d33"), Email = "Sincere@april.biz", Name = "Leanne Graham" }, // contact person
            new User { Id = Guid.Parse("c3b0ce62-75a4-483f-b965-f625059803f0"), Email = "talabi@april.biz", Name = "Talabi" }, // attendee
            new User { Id = Guid.Parse("297e3e8e-3164-4343-8b64-44d1c0ce127d"), Email = "jb@april.biz", Name = "Jubril" } // attendee
        );
    }
}