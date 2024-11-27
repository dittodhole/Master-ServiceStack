using System.Collections.Generic;

namespace DoeInc.Ticketing.ServiceModel.Types
{
    public class Ticket : EntityBase
    {
        public string Title { get; set; }
        public Status Status { get; set; }
        public string Creator { get; set; }
        public string Processor { get; set; }
    }
}
