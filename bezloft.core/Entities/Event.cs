using bezloft.core.Enums;

namespace bezloft.core.Entities;

public class Event : BaseEntity
{
    public Guid Id { get; set; }
    public Guid ContactPersonId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public EventVisibility Visibility { get; set; }
    public int Limit { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    // location
    public User ContactPerson { get; set; }
    public List<RSVP> RSVPs { get; set; }

}
