using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Threading;

namespace Ebay_Amazon
{
    
    public partial class index : System.Web.UI.Page
    {
        public string searchkey = "";
        public int perPage = 50;
        public int linknum = 10;
        public int totalnum = 0;
        public int showmaxnum = 0;
        public int showminnum = 0;
        public string ebayuser = "";
        public string searchType = "";
        public EbayClass ec;
        protected void Page_Load(object sender, EventArgs e)
        {
            Server.ScriptTimeout = 3600 * 5;
            int numfooter=0;
            int numnext=0;
            int numprev = 0;
            Label1.Font.Size = 9;
            
            //Table1.GridLines = GridLines.Horizontal;
            //Table1.BorderWidth = 2;

//             if (Request.Params["key"] != null)
//             {
//                 
//                 searchkey = Request.Params["key"];
//                 Thread th = new Thread(ViewAllUrls);
//                 th.Start();
// //                ViewAllUrls();
//             }
            /*else*/
 //           {
            searchType = RadioButtonList1.SelectedValue;
            sType.Value = searchType;
            if (Request.Params["footer"] != null || Request.Params["next"] != null || Request.Params["prev"] != null)
            {
                if (Request.Params["footer"].Equals(""))
                {
                    numfooter = 0;
                }
                else
                {
                    numfooter = int.Parse(Request.Params["footer"]);
                    showTable(numfooter);
                    addfooter(numfooter, 1);
                }
                if (Request.Params["next"].Equals(""))
                {
                    numnext = 0;
                }
                else
                {
                    numnext = int.Parse(Request.Params["next"]);
                    showTable(numnext + 1);
                    addfooter(numnext, 2);
                }
                if (Request.Params["prev"].Equals(""))
                {
                    numprev = 0;
                }
                else
                {
                    numprev = int.Parse(Request.Params["prev"]);
                    showTable(numprev - 1);
                    addfooter(numprev, 3);
                }
                if (numfooter == 0 && numnext == 0 && numprev == 0)
                {

                }



                //           }
            }
        }

        protected void addfooter(int num,int type)
        {
            switch(type){
                case 1:
                    showminnum = num - (num % linknum);
                    showmaxnum = showminnum + 10;
                    showFooter(showmaxnum,showminnum);
                    break;
                case 2:
                    showminnum = num +1 ;
                    showmaxnum = showminnum + 10;
                    showFooter(showmaxnum, showminnum);
                    break;
                case 3:
                    showmaxnum = num;
                    showminnum = showmaxnum-10;
                    showFooter(showmaxnum, showminnum);
                    break;
            }
        }

        protected void showFooter(int max,int min)
        {
            int pages;
            int gad = totalnum % perPage;
            if (gad == 0)
            {
                pages = totalnum / perPage;
            }
            else
            {
                pages = totalnum / perPage + 1;
            }

            pagenum.Value = pages.ToString();
            if (min > 0)
            {
                HyperLink link = new HyperLink();
                link.Text = "<";
                link.NavigateUrl = "javascript:prevPage(" + min + ")";
                link.Width = 30;
                link.Font.Size = 10;
                footPanel.Controls.Add(link);
            }
            for(int i=min;i<max;i++){
                if (i < pages)
                {
                    HyperLink link = new HyperLink();
                    link.Text = (i + 1).ToString();
                    link.NavigateUrl = "javascript:convertPage(" + i + ")";
                    link.Width = 30;
                    link.Font.Size = 10;
                    footPanel.Controls.Add(link);
                }
            }
            if (max < pages)
            {
                HyperLink link = new HyperLink();
                link.Text = ">";
                link.NavigateUrl = "javascript:nextPage(" + (max-1) + ")";
                link.Width = 30;
                link.Font.Size = 10;
                footPanel.Controls.Add(link);
            }
            
        }
        protected void showTable(int number)
        {        
            number = number + 1;
            ebayuser = TextBox1.Text.ToString();
            ec = new EbayClass();
//            string eurl = "http://www.ebay.com/sch/m.html?_sop=12&_ssn=" + ebayuser + "&_pgn=" + number.ToString();
            if(ec.ebaysearch(ebayuser,number.ToString(),searchType) == false)
            {
                Label1.Text = "No results";
                return;
            }
            else
                Label1.Text = "ebay site:   " + ec.resultcount + "  " + "results";
            if (int.Parse(ec.resultcount.Replace(",", "")) != 0)
            {
//                Button2.Visible = true;
                HyperLink bbb = new HyperLink();
                bbb.Text = "ViewAllUrls";
                bbb.NavigateUrl = "javascript:Button2_Click()";
                bbb.CssClass = "linkStyle";
                aaa.Controls.Add(bbb);
            }
            
            for (int i = 0; i < ec.ebayresultname.Count; i++)
            {
                if (i <= int.Parse(ec.resultcount.Replace(",", "")))
                {
                    
                    Panel panel = new Panel();
                    panel.Width = 800;
                    Table Table1 = new Table();
                    TableRow row = new TableRow();
                    panel.ID = "row" + i.ToString();
                    row.Height = 60;
                    TableCell urlcell = new TableCell();
                    TableCell urlViewCell = new TableCell();
                    HyperLink urlview = new HyperLink();
                    urlview.Width = 50;
                    urlview.Text = "View Urls";


                    urlcell.HorizontalAlign = HorizontalAlign.Center;
                    TableCell pricecell = new TableCell();
                    urlcell.Width = 800;
                    HyperLink link = new HyperLink();
                    
                    link.Text = ec.ebayresultname[i];
                    ec.ebayresultname[i] = ec.ebayresultname[i].Replace("\"", "!@#");
                    link.NavigateUrl = "javascript:sendUrlRequest('" + ec.ebayresultname[i] + "','" + panel.ID + "', 0)";
                    urlview.NavigateUrl = "javascript:viewURL('" + ec.ebayresultname[i] + "')";
                    link.Font.Size = 15;
                    link.Font.Bold = true;
                    urlcell.Controls.Add(link);
                    urlViewCell.Controls.Add(urlview);
                    row.Cells.Add(urlcell);
                    row.Cells.Add(urlViewCell);
                    Table1.CssClass = "tableRowStyle";
                    
                    Table1.Rows.Add(row);
                    panel.Controls.Add(Table1);
                    contentPanel.Controls.Add(panel);
                }
            }
            totalnum = int.Parse(ec.resultcount.Replace(",",""));
        }
   
        protected void Button1_Click(object sender, EventArgs e)
        {
           if (TextBox1.Text == "")
               return;
            ebayuser = TextBox1.Text.ToString();
            ec = new EbayClass();
            if (ec.ebaysearch(ebayuser,"1",searchType) == false)
            {
                Label1.Text = "No result";
                return;
            }
            else
                Label1.Text = "ebay site:   " + ec.resultcount + "  " + "results";
            if (int.Parse(ec.resultcount.Replace(",", "")) != 0)
            {
//                    Button2.Visible = true;
                HyperLink bbb = new HyperLink();
                bbb.Text = "ViewAllUrls";
                bbb.NavigateUrl = "javascript:Button2_Click()";
                bbb.CssClass = "linkStyle";
                aaa.Controls.Add(bbb);
            }
            for(int i=0 ;i< ec.ebayresultname.Count; i++)
            {
                Panel panel = new Panel();
                panel.Width = 800;
                Table Table1 = new Table();
                TableRow row = new TableRow();
                panel.ID = "row" + i.ToString();
                row.Height = 60;
                TableCell urlcell = new TableCell();
                
                urlcell.HorizontalAlign = HorizontalAlign.Center;
//                TableCell pricecell = new TableCell();
                TableCell loadingCell = new TableCell();
                TableCell urlViewCell = new TableCell();
                HyperLink urlview = new HyperLink();
                urlview.Width = 50;
                urlview.Text = "View Urls";
                
                urlcell.Width = 700;
//                pricecell.Width = 100;
                HyperLink link = new HyperLink();
                
                link.Text = ec.ebayresultname[i];
                ec.ebayresultname[i] = ec.ebayresultname[i].Replace("\"", "!@#");
                ec.ebayresultname[i] = ec.ebayresultname[i].Replace("'", "#@!");
                link.NavigateUrl = "javascript:sendUrlRequest('" + ec.ebayresultname[i] + "','" + panel.ID + "', 0)";
                urlview.NavigateUrl = "javascript:viewURL('"+ec.ebayresultname[i]+"')";
                link.Font.Size = 15;
                link.Font.Bold = true;
                urlcell.Controls.Add(link);
                urlViewCell.Controls.Add(urlview);
                row.Cells.Add(urlcell);
                row.Cells.Add(urlViewCell);
  //              row.Cells.Add(pricecell);
                
                Table1.CssClass = "tableRowStyle";
                Table1.Rows.Add(row);
                panel.Controls.Add(Table1);
                contentPanel.Controls.Add(panel);
            }
            string totalstring = ec.resultcount.Replace(",", "");
            totalnum = Int32.Parse(totalstring);
            int pages;
            int gad = totalnum % perPage;
            if (gad == 0)
            {
                pages = totalnum / perPage;
            }
            else
            {
                pages = totalnum / perPage +1;
            }
            pagenum.Value = pages.ToString();
             showminnum = 0;
            for (int i = 0; i < linknum; i++)
            {
                if (i < pages)
                {
                    HyperLink link = new HyperLink();
                    link.Text = (i + 1).ToString();
                    link.NavigateUrl = "javascript:convertPage(" + i + ")";
                    link.Width = 30;
                    link.Font.Size = 10;
                    
                    footPanel.Controls.Add(link);
                    showmaxnum = i+1;
                }
            }
            if (showmaxnum < pages)
            {
                HyperLink nextLink = new HyperLink();
                nextLink.Text = ">";
                nextLink.NavigateUrl = "javascript:nextPage(" + showmaxnum + ")";
                nextLink.Font.Size = 10;
                nextLink.Width = 30;
                footPanel.Controls.Add(nextLink);    
            }
            
            

        }
        
  
        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            searchType = RadioButtonList1.SelectedValue;
            sType.Value = searchType;
        }

    }
}