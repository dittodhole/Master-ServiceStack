using DoeInc.Ticketing.Core;
using DoeInc.Ticketing.ServiceModel;
using ServiceStack;

namespace DoeInc.Ticketing.ServiceInterface
{
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
            this.Request.RemoveFromCache(this.Cache,
                                         UrnId.Create<GetComments>(request.TicketId));
            this.Repository.Delete(request);
        }

        public object Get(GetComments request)
        {
            return this.Request.ToOptimizedResultUsingCache(this.Cache,
                                                            UrnId.Create<GetComments>(request.TicketId),
                                                            () => this.Repository.Read(request));
        }

        public object Post(StoreComment request)
        {
            this.Request.RemoveFromCache(this.Cache,
                                         UrnId.Create<GetComments>(request.TicketId));
            return this.Repository.Store(request);
        }

        public object Put(StoreComment request)
        {
            this.Request.RemoveFromCache(this.Cache,
                                         UrnId.Create<GetComments>(request.TicketId));
            return this.Repository.Store(request);
        }
    }
}
