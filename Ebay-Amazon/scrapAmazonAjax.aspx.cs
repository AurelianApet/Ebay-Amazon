using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ebay_Amazon
{
    public partial class scrapAmazonAjax : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (Request.Params["key"] == null)
            {
                return;
            }
            
            string key = Request.Params["key"];
            key = key.Replace("@@!!", "&");
            string res = key+"!!@@##";
            AmazonClass amazon = new AmazonClass();
            amazon.amazonallurlviewer(key);
            foreach (string amazonurl in amazon.amazon_ebay_sameurl)
            {
                res += amazonurl + "#@!";
            }
            Response.Write(res);
        }
    }
}