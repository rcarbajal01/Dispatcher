using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business;
using System.Data;
using System.Text;
using System.IO;

namespace WebData
{
    public partial class historyFiles : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["mysession"] == null)
            {
                Response.Redirect("login.aspx");
            }else if( (string)Session["mytype"] != "Admin" ){
                Response.Redirect("Home.aspx");
            } 

            Helper help = new Helper();
            //Building an HTML string.
            StringBuilder html = new StringBuilder();


            DataTable dt;
            dt = help.getHistory();



            if (IsPostBack)
            {
                string eventTarget = Page.Request.Params.Get("__EVENTTARGET");
                string eventParam = Page.Request.Params.Get("__EVENTARGUMENT");

                switch (eventTarget)
                {


                    case "activatedb":
                        help.activateDB(eventParam);
                        Response.Redirect("history.aspx");
                        
                        break;

                    case "downhist":

                        string[] values = eventParam.Split(new string[] { "@" }, StringSplitOptions.None);
                        
                        DataTable data;
                        int id;
                        string filename = Path.GetFileName(values[1]);
                        
                        
                        id = Convert.ToInt32(values[0]);
                        data = help.getFileId(id);


                        if (data.Rows.Count > 0)
                        {
                            StringBuilder sb = new StringBuilder();

                            string[] columnNames = data.Columns.Cast<DataColumn>().
                                                              Select(column => column.ColumnName).
                                                              ToArray();
                            sb.AppendLine(string.Join(",", columnNames));

                            foreach (DataRow row in data.Rows)
                            {
                                string[] fields = row.ItemArray.Select(field => field.ToString()).
                                                                ToArray();
                                sb.AppendLine(string.Join(",", fields));
                            }

                            string text = sb.ToString();
                            Response.Clear();
                            Response.ClearHeaders();

                            Response.AddHeader("Content-Length", text.Length.ToString());
                            Response.ContentType = "text/plain";
                            Response.AppendHeader("content-disposition", "attachment;filename=\"" + filename + ".csv" + "\"");
                            Response.Write(text);
                            Response.End();
                        }

                        //help.deleteUser(eventParam);
                        //Response.Redirect(Request.Url.AbsoluteUri);
                        break;

                    case "deldb":
                        help.delDB(eventParam);
                        Response.Redirect("historyFiles.aspx");

                        break;

                }


            }



            //Table start.
            html.Append("<table id='histtable' class='table table table-striped table-condensed table-responsive table-hover'>");

            //Building the Header row.
            html.Append("<thead>");
            html.Append("<tr>");

            html.Append("<th class='info'>");
            html.Append("Nombre");
            html.Append("</th>");

            html.Append("<th class='info'>");
            html.Append("Descargar");
            html.Append("</th>");
            
            
            html.Append("<th class='info'>");
            html.Append("Eliminar Base");
            html.Append("</th>");
            
            
            html.Append("</tr>");
            html.Append("</thead>");

            html.Append("<tbody>");
            //Building the Data rows.
            foreach (DataRow row in dt.Rows)
            {
                html.Append("<tr>");
                html.Append("<td>");
                html.Append(row["Nombre"].ToString());
                html.Append("</td>");
                

                html.Append("<td>");

                string filename2 = Path.GetFileName( row[1].ToString());
                html.Append("<Button id='" + row[0].ToString() + "' runat='server' OnClick=\"javascript:__doPostBack('downhist','" + row[0].ToString() + "@" + filename2 + "')\" Class='btn  btn-success' data-toggle='tooltip' data-placement='bottom' title='Descargar " + filename2 + "'>" +
                            "<span aria-hidden='true' class='glyphicon glyphicon-download'></span> Descargar" +
                            "</Button>");

                html.Append("</td>");



                html.Append("<td>");

                html.Append("<Button id='del^" + row[0].ToString() + "' runat='server' OnClick=\"javascript:__doPostBack('deldb','" + row[0].ToString()+ "')\" Class='btn  btn-danger' data-toggle='tooltip' data-placement='bottom' title='Eliminar " + filename2 + "'>" +
                            "<span aria-hidden='true' class='glyphicon glyphicon-trash'></span> Eliminar" +
                            "</Button>");

                html.Append("</td>");



                html.Append("</tr>");
            }

            html.Append("</tbody>");
            //Table end.
            html.Append("</table>");

            //Append the HTML string to Placeholder.
            historytable.Controls.Add(new Literal { Text = html.ToString() });





        }
    }
}