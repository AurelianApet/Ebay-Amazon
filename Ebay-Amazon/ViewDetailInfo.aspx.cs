using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ebay_Amazon
{
    public partial class ViewDetailInfo : System.Web.UI.Page
    {
        public string amazonkey = "";
        public string responsestring = "";
        public int pagenum = 1;
        protected void Page_Load(object sender, EventArgs e)
        {
//            Label1.Font.Size = 10;
            if (Request.Params["str"] == null || Request.Params["num"]==null)
            {
                return;
            }
            amazonkey = (string)Request.Params["str"];
            pagenum = int.Parse(Request.Params["num"]);

            if (amazonkey == null)
                return;
            AmazonClass ama = new AmazonClass();
            if(ama.amazonsearch(amazonkey,pagenum.ToString()) == false)
            {                
 //               Label1.Text = "No results";
                return;
            }
            else if (ama.catchstatus == true)
            {
                ama.catchstatus = false;
                return;
            }
            else
            {
                for(int i = 0 ; i < ama.amazonresultname.Count ; i ++)
                {
 //                   responsestring += ama.amazonresultname[i] + "@#$" + ama.amazonresulturl[i] + "@#$" + ama.amazonresultprice[i]+"::!@#";
                    responsestring += ama.amazonresultname[i] + "@#$" + ama.amazonresulturl[i] + "::!@#";
                    
   /*                 Table table = new Table();
                    table.BorderStyle = BorderStyle.Solid;
                    table.BorderWidth = 2;
                    TableRow tableRow = new TableRow();
                    TableCell namecell = new TableCell();
                    TableCell pricecell = new TableCell();
                    TableCell urlcell = new TableCell();

                    namecell.Width = 500;
                    namecell.HorizontalAlign = HorizontalAlign.Center;
                    pricecell.Width = 100;
                    urlcell.Width = 500;
                    urlcell.HorizontalAlign = HorizontalAlign.Center;

                    HyperLink link = new HyperLink();
                    link.Text = ama.amazonresulturl[i].ToString();
                    link.NavigateUrl = ama.amazonresulturl[i].ToString();

                    namecell.Text = ama.amazonresultname[i].ToString();
                    pricecell.Text = ama.amazonresultprice[i].ToString();
                    urlcell.Controls.Add(link);
                    tableRow.Cells.Add(namecell);
                    tableRow.Cells.Add(pricecell);
                    tableRow.Cells.Add(urlcell);
                    table.Rows.Add(tableRow);
                    Panel1.Controls.Add(table);
                    */
                }
                responsestring += "@@@@@" + ama.amazonresultcount.ToString() + "@@@@@" + ama.pagenum.ToString();
                Response.Write(responsestring);
                return;
            }
            
        }
    }
}