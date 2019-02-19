using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business;

namespace WebData
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Helper help = new Helper();
            help.logout((string)HttpContext.Current.Session["mysession"]);
        }
    }
}