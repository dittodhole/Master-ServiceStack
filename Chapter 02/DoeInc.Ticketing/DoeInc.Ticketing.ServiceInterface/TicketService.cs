using DoeInc.Ticketing.ServiceModel;
using ServiceStack;

namespace DoeInc.Ticketing.ServiceInterface
{
    public class TicketService : Service,
                                 IGet<GetTickets>,
                                 IGet<GetTicket>,
                                 IPost<StoreTicket>,
                                 IPut<StoreTicket>,
                                 IDelete<DeleteTicket>
    {
        public object Get(GetTickets request)
        {
            throw new System.NotImplementedException();
        }

        public object Get(GetTicket request)
        {
            throw new System.NotImplementedException();
        }

        public object Post(StoreTicket request)
        {
            throw new System.NotImplementedException();
        }

        public object Put(StoreTicket request)
        {
            throw new System.NotImplementedException();
        }

        public object Delete(DeleteTicket request)
        {
            throw new System.NotImplementedException();
        }
    }
}
