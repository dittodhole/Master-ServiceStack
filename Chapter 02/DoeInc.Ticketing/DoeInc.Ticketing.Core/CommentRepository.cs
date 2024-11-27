using System;
using System.Collections.Generic;
using DoeInc.Ticketing.ServiceModel;
using DoeInc.Ticketing.ServiceModel.Types;
using ServiceStack;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace DoeInc.Ticketing.Core
{
    public class CommentRepository : IRepository
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public CommentRepository(IDbConnectionFactory dbConnectionFactory)
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
                db.CreateTableIfNotExists<Comment>();
            }
        }

        public void Delete(DeleteComment request)
        {
            using (var db = this.ConnectionFactory.Open())
            {
                db.Delete<Comment>(new
                                   {
                                       request.Id,
                                       request.TicketId
                                   });
            }
        }

        public List<Comment> Read(GetComments request)
        {
            using (var db = this.ConnectionFactory.Open())
            {
                return db.Select<Comment>(arg => arg.TicketId == request.TicketId);
            }
        }

        public Comment Store(StoreComment request)
        {
            var comment = request.ConvertTo<Comment>();
            using (var db = this.ConnectionFactory.Open())
            {
                var success = db.Save(comment);
                if (!success)
                {
                    return null;
                }
            }
            return comment;
        }
    }
}
