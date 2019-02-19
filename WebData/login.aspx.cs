using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business;

namespace WebData
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Helper help = new Helper();

            help.logout((string)Session["mysession"]);

            Session["mysession"] = null;
            //if (Session["user"] != null)
            //{
            //    HttpContext.Current.Cache.Remove((string)Session["user"]);
            //    Session["user"] = null;
            //}
        }





        protected void submitcred(object sender, EventArgs e)
        {
            string sKey = myusr.Value + mypwd.Value;
            string sUser = Convert.ToString(Cache[sKey]);
            // Let them in - redirect to main page, etc.
            Helper help = new Helper();

            if (myusr.Value != "" && mypwd.Value != "")
            {
                string userVal = help.checkLogin(myusr.Value, mypwd.Value);
                if (userVal == "no loggeado")
                {

                    Session["mysession"] = myusr.Value;
                    string profile = help.usrType(myusr.Value);
                    Session["mytype"] = profile;
                    switch (profile)
                    {
                        case "Despachador":
                            Response.Redirect("Home.aspx");
                            break;
                        case "Admin":
                            Response.Redirect("Home.aspx");
                            break;
                    }
                }
                else if (userVal == "no existe")
                {

                    divmsg.InnerHtml = "<div class='alert alert-danger' role='alert'>" +
                                       "<span class='glyphicon glyphicon-warning-sign' aria-hidden='true'></span>" +
                                       "<span class='sr-only'>Error:</span>    " +
                                        "<strong>&nbsp &nbsp &nbsp ¡UPS!</strong> Parece que los datos no son válidos. Por favor verifica tu usuario y contraseña." +
                                        "</div>";
                }
                else
                {
                    divmsg.InnerHtml = "<div class='alert alert-danger' role='alert'>" +
                                       "<span class='glyphicon glyphicon-warning-sign' aria-hidden='true'></span>" +
                                       "<span class='sr-only'>Error:</span>    " +
                                        "<strong>&nbsp &nbsp &nbsp ¡UPS!</strong> Parece que este usuario ya tiene otra sesión activa!" +
                                        "</div>";
                    return;
                }
            }
            else
            {
                divmsg.InnerHtml = "<div class='alert alert-danger' role='alert'>" +
                                       "<span class='glyphicon glyphicon-warning-sign' aria-hidden='true'></span>" +
                                       "<span class='sr-only'>Error:</span>    " +
                                        "<strong>&nbsp &nbsp &nbsp ¡Oh vamos ( ¬.¬) !</strong> Los campos no pueden ir vacíos, por favor ingresa tu nombre de usuario y contraseña." +
                                        "</div>";
            }
        }

        [System.Web.Services.WebMethod]

        public static void logOut()
        {
            Helper help = new Helper();
            help.logout((string)HttpContext.Current.Session["mysession"]);

        }
    }
}