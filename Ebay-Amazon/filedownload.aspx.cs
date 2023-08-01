using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ebay_Amazon
{
    public partial class filedownload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Params["filename"] == null)
            {
                return;
            }
            string filename = Request.Params["filename"];
            string path = Page.Server.MapPath(filename);
//            string path = "d:\\website\\" + filename;
            byte[] buf = System.IO.File.ReadAllBytes(path);
            Response.Clear();
            Response.ContentType = "text/plain";
            Response.AddHeader("Content-Length",buf.Length.ToString());
            Response.AddHeader("Content-Disposition", "attachment;filename=" +filename);
            Response.BinaryWrite(buf);
            Response.Flush();
            Response.End();
        }
    }
}