using System;
using ServiceStack.AspNet;

namespace DoeInc.Ticketing.Web
{
    public partial class Default : ServiceStackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.SessionBag.Get<object>("foo");
            this.SessionBag.Remove("foo");
            this.SessionBag.RemoveAll();
        }
    }
}