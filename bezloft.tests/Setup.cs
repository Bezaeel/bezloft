using bezloft.core.Entities;
using bezloft.infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

namespace bezloft.tests;

public class SetUp : IDisposable
{
    public readonly ApplicationDbContext dbContext;
    public SetUp()
    {
        var services = new ServiceCollection();
        services.AddEntityFrameworkInMemoryDatabase()
            .AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseInMemoryDatabase("bezloft");
                options.ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning));
            });
        var serviceProvider = services.BuildServiceProvider();
        dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
        dbContext.Database.EnsureCreated();
    }

    public List<User> seedUsers(ApplicationDbContext dbContext)
    {
        List<User> users = new List<User>()
        {
            new User(){ Id = Guid.NewGuid(), Email = "talabi@mail.com", Name="Talabi"  }
            , new User(){ Id = Guid.NewGuid(), Email = "philipp@mail.com", Name="Talabi"  }
            , new User(){ Id = Guid.NewGuid(), Email = "kelvin@mail.com", Name="Talabi"  }
        };

        dbContext.Users.AddRange(users);
        dbContext.SaveChanges();
        return users;
    }

    public List<Event> seedEvents(ApplicationDbContext dbContext)
    {
        var users = seedUsers(dbContext);
        List<Event> events = new List<Event>()
        {
            new Event(){ Id = Guid.NewGuid(), Name="Talabi", Description="Talabi", ContactPersonId = users[0].Id }
        };

        dbContext.Events.AddRange(events);
        dbContext.SaveChanges();
        return events;
    }

    public List<RSVP> seedRSVPs(ApplicationDbContext dbContext)
    {
        var userIds = seedUsers(dbContext);
        var eventIds = seedEvents(dbContext);

        List<RSVP> rsvps = new List<RSVP>()
        {
            new RSVP(){ Id = Guid.NewGuid(), EventId = eventIds[0].Id, UserId =  userIds[0].Id, Intent = core.Enums.IntentStatus.YES, Approval= core.Enums.ApprovalStatus.APPROVED, Comment = string.Empty }
            , new RSVP(){ Id = Guid.NewGuid(), EventId = eventIds[0].Id, UserId =  userIds[1].Id, Intent = core.Enums.IntentStatus.YES, Approval= core.Enums.ApprovalStatus.REJECTED, Comment = string.Empty }
            , new RSVP(){ Id = Guid.NewGuid(), EventId = eventIds[0].Id, UserId =  userIds[2].Id, Intent = core.Enums.IntentStatus.NO, Approval= core.Enums.ApprovalStatus.APPROVED, Comment = string.Empty }
        };

        dbContext.RSVPs.AddRange(rsvps);
        dbContext.SaveChanges();
        return rsvps;
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }
}
