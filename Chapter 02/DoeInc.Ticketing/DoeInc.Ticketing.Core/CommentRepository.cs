using DoeInc.Ticketing.ServiceModel;
using DoeInc.Ticketing.ServiceModel.Types;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace DoeInc.Ticketing.Core
{
    public class CommentRepository
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

        public void Delete(DeleteComment request)
        {
            throw new System.NotImplementedException();
        }

        public Comment[] Read(GetComments request)
        {
            throw new System.NotImplementedException();
        }

        public Comment Store(StoreComment request)
        {
            throw new System.NotImplementedException();
        }
    }
}
