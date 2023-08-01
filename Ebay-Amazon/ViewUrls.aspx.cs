using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using System.Windows.Forms;


namespace Ebay_Amazon
{
    public partial class ViewUrls : System.Web.UI.Page
    {
        public string key = "";
        public int pagenum = 0;
        public string searchTypeValue;
        protected void Page_Load(object sender, EventArgs e)
        {
            keys.Value = "";
            pagenums.Value = "";

//            progressPanel.Width = 0;
            if (!IsPostBack)
            {
                Server.ScriptTimeout = 3600 * 5;
                if (Request.Params["key"] == null || Request.Params["searchType"]==null)
                {
                    return;
                }
                searchTypeValue = Request.Params["searchType"];
                searchType.Value = searchTypeValue;
                key = Request.Params["key"];
                EbayClass ebay = new EbayClass();
                string ebayresultstotalcounts = ebay.ebayreustcount(key, searchTypeValue);
                if (ebayresultstotalcounts == "0")
                {
                    Label1.Text = "No results";
                    loading.Dispose();
                    return;
                }
                int k = int.Parse(ebayresultstotalcounts) / 200;
                if (k == 0)
                    k = k + 1;
                if (k > 49)
                    k = 49;

                
                keys.Value = key;
                pagenums.Value = k.ToString();

            }
            
        }
       
 
    }
}