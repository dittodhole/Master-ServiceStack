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
            var ticket = this.Repository.Read(request);
            if (ticket == null)
            {
                throw HttpError.NotFound("The requested ticket instance cannot be found");
            }
            return ticket;
        }

        public object Get(GetTickets request)
        {
            return this.Repository.Read();
        }

        public object Post(StoreTicket request)
        {
            var ticket = this.Repository.Store(request);
            if (ticket == null)
            {
                throw HttpError.NotFound("The ticket instance cannot be found for update");
            }

            return ticket;
        }

        public object Put(StoreTicket request)
        {
            return this.Repository.Store(request);
        }
    }
}
