using System.Collections.Generic;
using DoeInc.Ticketing.ServiceModel.Types;
using ServiceStack;

namespace DoeInc.Ticketing.ServiceModel
{
    public class GetTickets : IReturn<List<Ticket>>
    {
    }
}
