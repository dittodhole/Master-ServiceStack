using System.Collections.Generic;
using DoeInc.Ticketing.ServiceModel;
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

        public bool Delete(DeleteTicket request,
                           string userAuthId)
        {
            using (var db = this.ConnectionFactory.Open())
            {
                return db.Delete<Ticket>(new
                                         {
                                             request.Id,
                                             ProcessorUserAuthId = userAuthId
                                         }) > 1;
            }
        }

        public Ticket Read(GetTicket request,
                           string userAuthId)
        {
            using (var db = this.ConnectionFactory.Open())
            {
                return db.Single<Ticket>(new
                                         {
                                             request.Id,
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

        public Ticket Store(StoreTicket request,
                            string userAuthId)
        {
            var ticket = request.ConvertTo<Ticket>();
            ticket.ProcessorUserAuthId = userAuthId;

            using (var db = this.ConnectionFactory.Open())
            {
                bool success;
                if (request.Id <= 0)
                {
                    success = db.Save(ticket);
                }
                else
                {
                    success = db.Update(ticket) == 1;
                    if (success)
                    {
                        ticket = db.SingleById<Ticket>(request.Id);
                    }
                }

                if (!success)
                {
                    return null;
                }
            }
            return ticket;
        }
    }
}
