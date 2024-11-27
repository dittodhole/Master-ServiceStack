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

        public void Delete(DeleteTicket request)
        {
            using (var db = this.ConnectionFactory.Open())
            {
                db.DeleteById<Ticket>(request.Id);
            }
        }

        public Ticket Read(GetTicket request)
        {
            using (var db = this.ConnectionFactory.Open())
            {
                return db.SingleById<Ticket>(request.Id);
            }
        }

        public List<Ticket> Read()
        {
            using (var db = this.ConnectionFactory.Open())
            {
                return db.Select<Ticket>();
            }
        }

        public Ticket Store(StoreTicket request)
        {
            var ticket = request.ConvertTo<Ticket>();
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
