using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebData
{
    public partial class login : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            System.Web.UI.HtmlControls.HtmlGenericControl menu = (System.Web.UI.HtmlControls.HtmlGenericControl)this.FindControl("menu"); ;
            menu.Visible = false;

            if (Session["mysession"] != null)
            {                
                menu.Visible = true;
                switch (Session["mytype"].ToString())
                {
                    case "Despachador":
                        lidata.Attributes["style"] = "display:block;";
                        lihistory.Attributes["style"] = "display:none;";
                        liupload.Attributes["style"] = "display:none;";
                        liaddusr.Attributes["style"] = "display:none;";
                        lihistoryFiles.Attributes["style"] = "display:none;";
                        break;                    
                    case "Admin":
                        lidata.Attributes["style"] = "display:block;";
                        lihistory.Attributes["style"] = "display:block;";
                        liupload.Attributes["style"] = "display:block;";
                        liaddusr.Attributes["style"] = "display:block;";
                        lihistoryFiles.Attributes["style"] = "display:block;";
                        break;
                }
                switch (Request.FilePath.Replace("/", ""))
                {
                    case "data.aspx":
                        lidata.Attributes["class"] = "active";
                        break;
                    case "history.aspx":
                        lihistory.Attributes["class"] = "active";
                        break;                    
                    case "upload.aspx":
                        liupload.Attributes["class"] = "active";
                        break;
                    case "historyFiles.aspx":
                        lihistoryFiles.Attributes["class"] = "active";
                        break;
                    case "addusr.aspx":
                        liaddusr.Attributes["class"] = "active";
                        break;
                    default:
                        break;
                }
            }
        }                      
    }
}