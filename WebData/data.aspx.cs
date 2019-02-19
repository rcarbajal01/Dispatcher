using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business;
using System.Data;
using System.Text;

namespace WebData
{
    public partial class Data : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            //Building an HTML string.
            StringBuilder html = new StringBuilder();

            if (Session["mysession"] == null)
            {

                Response.Redirect("login.aspx");
            }


            if (!IsPostBack)
            {

                Helper help = new Helper();
                DataTable dt;
                dt = help.getInfo();
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    recordId.Value = row[0].ToString();
                    lblDn.Text = row[1].ToString();
                    lblNombre.Text = row[2].ToString();
                    txtReferencia1.Text = row[3].ToString();
                    txtReferencia2.Text = row[4].ToString();
                    txtCac.Text = row[5].ToString();
                    lblFvc.Text = row[6].ToString();
                }
            }
        }

        protected void SubmitRecord(object sender, EventArgs e)
        {
            if (recordId.Value != "")
            {
                Helper help = new Helper();
                string estatus = "Activo";
                string tipificacion = "";
                if (hdfEfectivoOption.Value == "Promesa")
                {
                    estatus = hdfEfectivoOption.Value;
                }
                else if (hdfNoefectivoOption.Value == "Rechazo" || hdfNoefectivoOption.Value == "Rechazo con queja")
                {
                    estatus = "Rechazo";
                }
                if (hdfContacto.Value == "Contacto efectivo")
                {
                    tipificacion = hdfEfectivoOption.Value;
                }
                else if (hdfContacto.Value == "Contacto no efectivo")
                {
                    tipificacion = hdfNoefectivoOption.Value;

                }

                help.updateReg(Convert.ToInt32(recordId.Value), estatus, hdfContacto.Value, tipificacion, hdfQuejaOption.Value, Session["mysession"].ToString(), DateTime.Now, txtReferencia1.Text, txtReferencia2.Text, txtCac.Text);
                Response.Redirect("Home.aspx");
            }
        }
    }
}