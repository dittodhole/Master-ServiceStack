using DoeInc.Ticketing.Core;
using DoeInc.Ticketing.ServiceModel;
using DoeInc.Ticketing.ServiceModel.Types;
using ServiceStack;

namespace DoeInc.Ticketing.ServiceInterface
{
    [Authenticate]
    public class CommentService : Service,
                                  IGet<GetComments>,
                                  IPost<StoreComment>,
                                  IPut<StoreComment>,
                                  IDeleteVoid<DeleteComment>
    {
        private readonly CommentRepository _commentRepository;

        public CommentService(CommentRepository commentRepository)
        {
            this._commentRepository = commentRepository;
        }

        private CommentRepository Repository
        {
            get
            {
                return this._commentRepository;
            }
        }

        public void Delete(DeleteComment request)
        {
            var ticketId = request.TicketId;
            var commentId = request.Id;
            var userAuthId = this.GetSession()
                                 .UserAuthId;

            if (this.Repository.Delete(ticketId,
                                       commentId,
                                       userAuthId))
            {
                this.Request.RemoveFromCache(this.Cache,
                                             UrnId.Create<GetComments>(ticketId));
            }
        }

        public object Get(GetComments request)
        {
            var ticketId = request.TicketId;

            return this.Request.ToOptimizedResultUsingCache(this.Cache,
                                                            UrnId.Create<GetComments>(ticketId),
                                                            () => this.Repository.Read(ticketId));
        }

        public object Post(StoreComment request)
        {
            var ticketId = request.TicketId;
            var comment = request.ConvertTo<Comment>();

            this.Request.RemoveFromCache(this.Cache,
                                         UrnId.Create<GetComments>(ticketId));
            return this.Repository.Store(comment);
        }

        public object Put(StoreComment request)
        {
            var ticketId = request.TicketId;
            var comment = request.ConvertTo<Comment>();
            comment.CreatorUserAuthId = this.GetSession()
                                            .UserAuthId;

            this.Request.RemoveFromCache(this.Cache,
                                         UrnId.Create<GetComments>(ticketId));
            return this.Repository.Store(comment);
        }
    }
}
