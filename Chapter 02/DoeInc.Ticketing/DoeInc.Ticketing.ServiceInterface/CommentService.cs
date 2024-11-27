using DoeInc.Ticketing.ServiceModel;
using ServiceStack;

namespace DoeInc.Ticketing.ServiceInterface
{
    public class CommentService : Service,
                                  IGet<GetComments>,
                                  IPost<StoreComment>,
                                  IPut<StoreComment>,
                                  IDelete<DeleteComment>
    {
        public object Get(GetComments request)
        {
            throw new System.NotImplementedException();
        }

        public object Post(StoreComment request)
        {
            throw new System.NotImplementedException();
        }

        public object Put(StoreComment request)
        {
            throw new System.NotImplementedException();
        }

        public object Delete(DeleteComment request)
        {
            throw new System.NotImplementedException();
        }
    }
}
