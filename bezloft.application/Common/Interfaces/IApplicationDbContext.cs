using bezloft.core.Entities;
using Microsoft.EntityFrameworkCore;

namespace bezloft.application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<User> Users { get; }
        DbSet<Event> Events { get; }
        DbSet<RSVP> RSVPs { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
