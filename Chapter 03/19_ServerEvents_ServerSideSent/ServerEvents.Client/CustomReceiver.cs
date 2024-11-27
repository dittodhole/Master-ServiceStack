using ServiceStack;
using ServiceStack.Text;

namespace ServerEvents.Client
{
    public class CustomReceiver : ServerEventReceiver
    {
        public void Say(Say say)
        {
            say.PrintDump();
        }
    }
}
