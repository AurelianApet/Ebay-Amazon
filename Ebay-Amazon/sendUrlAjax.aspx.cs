using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ebay_Amazon
{
    public partial class sendUrlAjax : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            string key = "";
            if (Request.Params["name"] != null)
            {
                AmazonClass amazon = new AmazonClass();
                string responseString = "";
                key = Request.Params["name"];
                amazon.amazonallurlviewer(key);
                foreach (string amazonurl in amazon.amazon_ebay_sameurl)
                {
                    responseString += amazonurl +"!@#!@#";
                }
                Response.Write(responseString);
            }
            else
            {
                return;
            }
            return;
        }
    }
}