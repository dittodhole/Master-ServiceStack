using System.Collections.Generic;
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

        public bool Delete(int ticketId,
                           int commentId,
                           string userAuthId)
        {
            using (var db = this.ConnectionFactory.Open())
            {
                return db.Delete<Comment>(new
                                          {
                                              Id = commentId,
                                              TicketId = ticketId,
                                              CreatorUserAuthId = userAuthId
                                          }) > 0;
            }
        }

        public List<Comment> Read(int ticketId)
        {
            using (var db = this.ConnectionFactory.Open())
            {
                return db.Select<Comment>(arg => arg.TicketId == ticketId);
            }
        }

        public Comment Read(int ticketId,
                            int id)
        {
            using (var db = this.ConnectionFactory.Open())
            {
                return db.Single<Comment>(new
                                          {
                                              TicketId = ticketId,
                                              Id = id
                                          });
            }
        }

        public Comment Store(Comment comment)
        {
            bool success;

            using (var db = this.ConnectionFactory.Open())
            {
                if (comment.Id <= 0)
                {
                    success = db.Save(comment);
                }
                else
                {
                    // this is need for not loosing properties of initial storing (eg creator)
                    comment = db.SingleById<Comment>(comment.Id)
                                .PopulateWithNonDefaultValues(comment);

                    success = db.Update(comment) == 1;
                    if (success)
                    {
                        comment = db.SingleById<Comment>(comment.Id);
                    }
                }
            }

            if (success)
            {
                return comment;
            }
            return null;
        }
    }
}
