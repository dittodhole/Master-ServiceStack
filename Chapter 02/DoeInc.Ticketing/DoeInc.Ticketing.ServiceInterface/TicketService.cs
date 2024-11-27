using System;
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
            throw new NotImplementedException();
        }

        public object Get(GetTicket request)
        {
            throw new NotImplementedException();
        }

        public object Get(GetTickets request)
        {
            throw new NotImplementedException();
        }

        public object Post(StoreTicket request)
        {
            throw new NotImplementedException();
        }

        public object Put(StoreTicket request)
        {
            throw new NotImplementedException();
        }
    }
}
