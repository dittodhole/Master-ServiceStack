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
            this.Repository.Delete(request);
        }

        public object Get(GetComments request)
        {
            return this.Repository.Read(request);
        }

        public object Post(StoreComment request)
        {
            return this.Repository.Store(request);
        }

        public object Put(StoreComment request)
        {
            return this.Repository.Store(request);
        }
    }
}
