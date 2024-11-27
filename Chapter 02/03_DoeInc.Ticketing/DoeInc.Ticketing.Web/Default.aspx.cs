using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
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