using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ebay_Amazon
{
    public partial class URLView : System.Web.UI.Page
    {
        public string key = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Params["key"] == null)
            {
                return;
            }
            key = Request.Params["key"];
            Label1.Text = key;
            AmazonClass am = new AmazonClass();
            if(am.amazonsearchall(key) == false)
            {
                Label1.Text = "No results";
                return;
            }
            else
            {
                foreach(string amaurl in am.amazonallurl)
                {
                    Table table = new Table();
                    TableCell cell = new TableCell();
                    TableRow row = new TableRow();
                    HyperLink link = new HyperLink();
                    link.Font.Size = 10;
                    link.Width=1400;
                    link.Text = amaurl;
                    link.NavigateUrl = amaurl;
                    cell.Width = 1400;
                    cell.Height=20;
                    cell.Controls.Add(link);
                    row.Cells.Add(cell);
                    table.Rows.Add(row);
                    urlpanel.Controls.Add(table);
                }
            }
        }

    }
}