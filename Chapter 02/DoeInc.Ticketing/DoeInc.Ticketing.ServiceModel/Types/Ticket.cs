using System.Collections.Generic;

namespace DoeInc.Ticketing.ServiceModel.Types
{
    public class Ticket : EntityBase
    {
        public string Title { get; set; }
        public List<Comment> Comments { get; set; }
        public Status Status { get; set; }
        public User Creator { get; set; }
        public User Processor { get; set; }
    }
}
