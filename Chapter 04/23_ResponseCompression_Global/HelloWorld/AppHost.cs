using Funq;
using ServiceStack;
using ServiceStack.Web;

namespace HelloWorld
{
    public class AppHost : AppSelfHostBase
    {
        public AppHost()
            : base("Hello World",
                   typeof (HelloService).Assembly)
        {
        }

        public override void Configure(Container container)
        {
        }

        public override object OnAfterExecute(IRequest req,
                                              object requestDto,
                                              object response)
        {
            response = base.OnAfterExecute(req,
                                           requestDto,
                                           response);

            if (response != null &&
                !(response is CompressedResult))
            {
                response = req.ToOptimizedResult(response);
            }

            return response;
        }
    }
}
