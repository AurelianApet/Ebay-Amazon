using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ebay_Amazon
{
    public partial class ViewUrlAjax : System.Web.UI.Page
    {
        public string key = "";
        public string searchType = "";
        public int num = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Server.ScriptTimeout = 3600 * 5;
                if (Request.Params["key"] == null || Request.Params["num"] == null || Request.Params["searchType"] == null)
                {
                    return;
                }
                key = Request.Params["key"];
                num = int.Parse(Request.Params["num"]);
                searchType = Request.Params["searchType"];
                EbayClass ebay = new EbayClass();
                string ebayresultstotalcounts = ebay.ebayreustcount(key, searchType);
                if (ebayresultstotalcounts == "0")
                {
                    return;
                }
                string str = "";
                if (ebay.ebaysearchall(key, num.ToString(), searchType) == true)
                {
                    foreach (string ebayname in ebay.ebayresultallname)
                    {
                        str += ebayname + "!@#";
                    }
                }
                Response.Write(str);
                
            }
        }
        public void viewAllUrls()
        {
            EbayClass ebay = new EbayClass();
            AmazonClass amazon = new AmazonClass();
            string ebayresultstotalcounts = ebay.ebayreustcount(key, searchType);
            if (ebayresultstotalcounts == "0")
            {
                return;
            }
            int k = int.Parse(ebayresultstotalcounts) / 200;
            if (k == 0)
                k = k + 1;
            if (k > 49)
                k = 49;


            for (int i = 1; i <= k; i++)
            {
                if (ebay.ebaysearchall(key, i.ToString(), searchType) == true)
                {
                    foreach (string ebayname in ebay.ebayresultallname)
                    {
                        amazon.amazonallurlviewer(ebayname);
                        foreach (string amazonurl in amazon.amazon_ebay_sameurl)
                        {
                            Response.Write(amazonurl);
                            Response.Flush();
                        }
                    }
                }
            }
            return;
        }
    }

}