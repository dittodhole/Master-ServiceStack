using DoeInc.Ticketing.Core;
using DoeInc.Ticketing.ServiceModel;
using DoeInc.Ticketing.ServiceModel.Types;
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
            var userAuthId = this.GetSession()
                                 .UserAuthId;
            var ticketId = request.Id;

            if (this.Repository.Delete(ticketId,
                                       userAuthId))
            {
                this.Request.RemoveFromCache(this.Cache,
                                             UrnId.Create<GetTicket>(ticketId),
                                             UrnId.Create<GetTickets>(userAuthId));
            }
        }

        public object Get(GetTicket request)
        {
            var userAuthId = this.GetSession()
                                 .UserAuthId;
            var ticketId = request.Id;

            var ticket = this.Request.ToOptimizedResultUsingCache(this.Cache,
                                                                  UrnId.Create<GetTicket>(ticketId),
                                                                  () => this.Repository.Read(ticketId,
                                                                                             userAuthId));
            if (ticket == null)
            {
                throw HttpError.NotFound("The requested ticket instance cannot be found");
            }

            return ticket;
        }

        public object Get(GetTickets request)
        {
            var userAuthId = this.GetSession()
                                 .UserAuthId;

            var tickets = this.Request.ToOptimizedResultUsingCache(this.Cache,
                                                                   UrnId.Create<GetTickets>(userAuthId),
                                                                   () => this.Repository.Read(userAuthId));
            return tickets;
        }

        public object Post(StoreTicket request)
        {
            var userAuthId = this.GetSession()
                                 .UserAuthId;

            var ticket = request.ConvertTo<Ticket>();
            ticket.ProcessorUserAuthId = userAuthId;
            ticket.CreatorUserAuthId = userAuthId;

            ticket = this.Repository.Store(ticket);
            if (ticket == null)
            {
                throw HttpError.NotFound("The ticket instance cannot be found for update");
            }

            this.Request.RemoveFromCache(this.Cache,
                                         UrnId.Create<GetTicket>(ticket.Id),
                                         UrnId.Create<GetTickets>(userAuthId));

            return ticket;
        }

        public object Put(StoreTicket request)
        {
            var userAuthId = this.GetSession()
                                 .UserAuthId;

            var ticket = request.ConvertTo<Ticket>();
            ticket.ProcessorUserAuthId = userAuthId;

            ticket = this.Repository.Store(ticket);

            this.Request.RemoveFromCache(this.Cache,
                                         UrnId.Create<GetTicket>(ticket.Id),
                                         UrnId.Create<GetTickets>(userAuthId));

            return ticket;
        }
    }
}
