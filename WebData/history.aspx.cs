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
    public partial class history : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["mysession"] == null)
            {
                Response.Redirect("login.aspx");
            }else if( (string)Session["mytype"] != "Admin" ){
                Response.Redirect("data.aspx");
            } 

        }

        protected void exportRecords(object sender, EventArgs e)
        {
            Helper help = new Helper();
            DataTable records = help.GetRecordsVenta(Convert.ToDateTime(hdf_dateIni.Value).ToString("yyyyMMdd"), Convert.ToDateTime(hdf_dateFin.Value).ToString("yyyyMMdd"));
            if(records.Rows.Count>0)
            {
                StringBuilder sb = new StringBuilder();

                string[] columnNames = records.Columns.Cast<DataColumn>().
                                                  Select(column => column.ColumnName).
                                                  ToArray();
                sb.AppendLine(string.Join(",", columnNames));

                foreach (DataRow row in records.Rows)
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
                Response.AppendHeader("content-disposition", "attachment;filename=\"Ventas" + hdf_dateIni.Value+"-"+hdf_dateFin.Value + ".csv"+"\"");                
                Response.Write(text);
                Response.End();
            }
        }
    }
}