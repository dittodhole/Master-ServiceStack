using System.Collections.Generic;
using DoeInc.Ticketing.ServiceModel.Types;
using ServiceStack;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace DoeInc.Ticketing.Core
{
    public class TicketRepository : IRepository
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public TicketRepository(IDbConnectionFactory dbConnectionFactory)
        {
            this._dbConnectionFactory = dbConnectionFactory;
        }

        private IDbConnectionFactory ConnectionFactory
        {
            get
            {
                return this._dbConnectionFactory;
            }
        }

        public void Initialize()
        {
            using (var db = this.ConnectionFactory.Open())
            {
                db.CreateTableIfNotExists<Ticket>();
            }
        }

        public bool Delete(int ticketId,
                           string userAuthId)
        {
            using (var db = this.ConnectionFactory.Open())
            {
                return db.Delete<Ticket>(new
                                         {
                                             Id = ticketId,
                                             ProcessorUserAuthId = userAuthId
                                         }) > 0;
            }
        }

        public Ticket Read(int ticketId,
                           string userAuthId)
        {
            using (var db = this.ConnectionFactory.Open())
            {
                return db.Single<Ticket>(new
                                         {
                                             Id = ticketId,
                                             ProcessorUserAuthId = userAuthId
                                         });
            }
        }

        public List<Ticket> Read(string userAuthId)
        {
            using (var db = this.ConnectionFactory.Open())
            {
                return db.Select<Ticket>(ticket => ticket.ProcessorUserAuthId == userAuthId);
            }
        }

        public Ticket Store(Ticket ticket)
        {
            bool success;

            using (var db = this.ConnectionFactory.Open())
            {
                if (ticket.Id <= 0)
                {
                    success = db.Save(ticket);
                }
                else
                {
                    // this is need for not loosing properties of initial storing (creator)
                    ticket = db.SingleById<Ticket>(ticket.Id)
                               .PopulateWithNonDefaultValues(ticket);

                    success = db.Update(ticket) == 1;
                    if (success)
                    {
                        ticket = db.SingleById<Ticket>(ticket.Id);
                    }
                }
            }

            if (success)
            {
                return ticket;
            }

            return null;
        }
    }
}
