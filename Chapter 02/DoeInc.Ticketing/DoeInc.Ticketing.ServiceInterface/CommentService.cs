using System;
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
            
        }

        public object Get(GetComments request)
        {
            throw new NotImplementedException();
        }

        public object Post(StoreComment request)
        {
            throw new NotImplementedException();
        }

        public object Put(StoreComment request)
        {
            throw new NotImplementedException();
        }
    }
}
