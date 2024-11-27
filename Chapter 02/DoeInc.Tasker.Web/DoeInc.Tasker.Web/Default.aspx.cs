using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ServiceStack;
using ServiceStack.AspNet;

namespace DoeInc.Tasker.Web
{
    public partial class Default : ServiceStackPage
    {
        public TodoRepository TodoRepository { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            var a = 1;
        }
    }
}