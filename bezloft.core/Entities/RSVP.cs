using bezloft.core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bezloft.core.Entities
{
    public class RSVP : BaseEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid EventId { get; set; }

        // pre-event
        public IntentStatus Intent { get; set; }
        public ApprovalStatus Approval { get; set; }
        public bool Attended { get; set; }

        // post-event experience
        public string Comment { get; set; }

        public User User { get; set; }
        public Event Event { get; set; }
    }
}
