using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bezloft.core.Entities;

public class User : BaseEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public List<Event> Events { get; set; }
    public List<RSVP> RSVPs { get; set; }

}
