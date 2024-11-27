using DoeInc.Ticketing.Core;
using DoeInc.Ticketing.ServiceModel;
using ServiceStack;

namespace DoeInc.Ticketing.ServiceInterface
{
    [Authenticate]
    public class TicketService : Service,
                                 IGet<GetTickets>,
                                 IGet<GetTicket>,
                                 IPost<StoreTicket>,
                                 IPut<StoreTicket>,
                                 IDeleteVoid<DeleteTicket>
    {
        private readonly TicketRepository _ticketRepository;

        public TicketService(TicketRepository ticketRepository)
        {
            this._ticketRepository = ticketRepository;
        }

        private TicketRepository Repository
        {
            get
            {
                return this._ticketRepository;
            }
        }

        public void Delete(DeleteTicket request)
        {
            this.Request.RemoveFromCache(this.Cache,
                                         UrnId.Create<GetTicket>(request.Id),
                                         UrnId.Create<GetTickets>(string.Empty));
            this.Repository.Delete(request);
        }

        public object Get(GetTicket request)
        {
            var ticket = this.Request.ToOptimizedResultUsingCache(this.Cache,
                                                                  UrnId.Create<GetTicket>(request.Id),
                                                                  () => this.Repository.Read(request));
            if (ticket == null)
            {
                return HttpError.NotFound("The requested ticket instance cannot be found");
            }
            return ticket;
        }

        public object Get(GetTickets request)
        {
            var tickets = this.Request.ToOptimizedResultUsingCache(this.Cache,
                                                                   UrnId.Create<GetTickets>(string.Empty),
                                                                   () => this.Repository.Read());
            return tickets;
        }

        public object Post(StoreTicket request)
        {
            var ticket = this.Repository.Store(request);
            this.Request.RemoveFromCache(this.Cache,
                                         UrnId.Create<GetTicket>(ticket.Id),
                                         UrnId.Create<GetTickets>(string.Empty));
            return ticket;
        }

        public object Put(StoreTicket request)
        {
            var ticket = this.Repository.Store(request);
            this.Request.RemoveFromCache(this.Cache,
                                         UrnId.Create<GetTicket>(ticket.Id),
                                         UrnId.Create<GetTickets>(string.Empty));
            return ticket;
        }
    }
}
