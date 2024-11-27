using System.Collections.Generic;
using DoeInc.Ticketing.ServiceModel.Types;
using ServiceStack;

namespace DoeInc.Ticketing.ServiceModel
{
    public class GetComments : IReturn<List<Comment>>
    {
        public int TicketId { get; set; }
    }
}
