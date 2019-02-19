using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Configuration;
using Business;


namespace WebData
{
    public partial class addusr : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            Helper help = new Helper();

            DataTable dt;
            dt = help.getUsers();

            //Building an HTML string.
            StringBuilder html = new StringBuilder();

            if (Session["mysession"] == null)
            {

                Response.Redirect("login.aspx");
            }
            else if ((string)Session["mytype"] != "Admin")
            {
                Response.Redirect("data.aspx");
            } 

            if (IsPostBack)
            {
              string eventTarget = Page.Request.Params.Get("__EVENTTARGET");
              string eventParam = Page.Request.Params.Get("__EVENTARGUMENT");

                switch (eventTarget)
                {
                    case "delreg":
                        help.deleteUser(eventParam);
                        Response.Redirect(Request.Url.AbsoluteUri);
                        break;

                    case "addreg":
                        string[] values = eventParam.Split(new string[] { ":" }, StringSplitOptions.None);
                        
                        help.addUser(values[0],values[1],values[2],values[3]);
                        Response.Redirect(Request.Url.AbsoluteUri);
                        break;
                }


            }


            //Table start.
            html.Append("<table id='usrtable' class='table table table-striped table-condensed table-responsive table-hover'>");

            //Building the Header row.
            html.Append("<thead>");
            html.Append("<tr>");
            foreach (DataColumn column in dt.Columns)
            {
                html.Append("<th class='info'>");
                html.Append(column.ColumnName);
                html.Append("</th>");
            }
            html.Append("<th class='info'>");
            html.Append("Delete");
            html.Append("</th>");
            html.Append("</tr>");
            html.Append("</thead>");

            html.Append("<tbody>");

            //Building the Data rows.
            foreach (DataRow row in dt.Rows)
            {
                if ((string)row["username"] != "root")
                {
                    html.Append("<tr>");
                    foreach (DataColumn column in dt.Columns)
                    {
                        html.Append("<td>");
                        html.Append(row[column.ColumnName]);
                        html.Append("</td>");
                    }

                    html.Append("<td>");


                    //                html.Append("<Button id='" + row[0].ToString() + "' runat='server' OnClick=\"javascript:__doPostBack('delreg','" + row[0].ToString() + "')\" Class='btn  btn-danger' data-toggle='tooltip' data-placement='right' title='Eliminar " + row[2].ToString() + "'>" +
                    html.Append("<Button id='" + row[0].ToString() + "' runat='server' OnClick=\"javascript:del('delreg','" + row[0].ToString() + "')\" Class='btn  btn-danger' data-toggle='tooltip' data-placement='right' title='Eliminar " + row[2].ToString() + "'>" +
                                "<span aria-hidden='true' class='glyphicon glyphicon-trash'></span> Eliminar" +
                                "</Button>");

                    html.Append("</td>");

                    html.Append("</tr>");
                }
            }
            
            html.Append("</tbody>");
            //Table end.
            html.Append("</table>");

            //Append the HTML string to Placeholder.
            userstable.Controls.Add(new Literal { Text = html.ToString() });

        }

    }
}