using System;
using DoeInc.Ticketing.ServiceModel;
using DoeInc.Ticketing.ServiceModel.Types;
using ServiceStack.Data;

namespace DoeInc.Ticketing.Core
{
    public class TicketRepository
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

        public void Delete(DeleteTicket request)
        {
            throw new NotImplementedException();
        }

        public Ticket Read(GetTicket request)
        {
            throw new NotImplementedException();
        }

        public Ticket[] Read()
        {
            throw new NotImplementedException();
        }

        public Ticket Store(StoreTicket request)
        {
            throw new NotImplementedException();
        }
    }
}
