using DoeInc.Ticketing.Core;
using DoeInc.Ticketing.ServiceModel;
using ServiceStack;

namespace DoeInc.Ticketing.ServiceInterface
{
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
            this.Repository.Delete(request);
        }

        public object Get(GetTicket request)
        {
            return this.Repository.Read(request);
        }

        public object Get(GetTickets request)
        {
            return this.Repository.Read();
        }

        public object Post(StoreTicket request)
        {
            return this.Repository.Store(request);
        }

        public object Put(StoreTicket request)
        {
            return this.Repository.Store(request);
        }
    }
}
