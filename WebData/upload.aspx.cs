using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business;
using System.Configuration;

namespace WebData
{
    public partial class upload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["mysession"] == null)
                {
                    Response.Redirect("login.aspx");
                }


                else if ((string)Session["mytype"] != "Admin")
                {

                    Response.Redirect("data.aspx");
                }
            }
        }

        protected void submitRecord(object sender, EventArgs e)
        {
            //if (hdf_dateIni.Value != "" && hdf_dateFin.Value != "" && addfile.PostedFile != null && addfile.PostedFile.ContentLength > 0)
            //Se elimina la vigencia
            if (addfile.PostedFile != null && addfile.PostedFile.ContentLength > 0)
            {
                try
                {
                    string fn = System.IO.Path.GetFileName(addfile.PostedFile.FileName);
                    string SaveLocation = Server.MapPath("Data") + "\\" + fn;
                    if ((addfile.PostedFile != null) && (addfile.PostedFile.ContentLength > 0))
                    {
                        try
                        {
                            addfile.PostedFile.SaveAs(SaveLocation);
                            Response.Write("The file has been uploaded.");
                        }
                        catch (Exception ex)
                        {
                            Response.Write("Error: " + ex.Message);
                            //Note: Exception.Message returns a detailed message that describes the current exception. 
                            //For security reasons, we do not recommend that you return Exception.Message to end users in 
                            //production environments. It would be better to put a generic error message. 
                        }
                    }
                    else
                    {
                        Response.Write("Please select a file to upload.");
                    }
                    lblError.Visible = true;
                    Helper help = new Helper();
                    //string path = ConfigurationManager.AppSettings["UploadPath"].ToString();
                    //lblError.Text = help.loadDnsFile(SaveLocation, Session["mysession"].ToString(), Convert.ToDateTime(hdf_dateIni.Value), Convert.ToDateTime(hdf_dateFin.Value));
                    //Se elimina la vigencia
                    lblError.Text = help.loadDnsFile(SaveLocation, Session["mysession"].ToString());
                }
                catch (Exception ex)
                {
                    lblError.Visible = true;
                    lblError.Text = ex.Message;
                }
            }
            else
            {
                Response.Redirect("Upload.aspx");
            }
        }


    }
}