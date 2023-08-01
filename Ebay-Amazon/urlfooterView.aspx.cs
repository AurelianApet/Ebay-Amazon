using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.IO;
using System.Web.UI.WebControls;

namespace Ebay_Amazon
{    
    public partial class urlfooterView : System.Web.UI.Page
    {
        public int num = 0;
        public int urlperpage = 0;
        public string filename = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Params["num"] == null || Request.Params["urlperpage"] == null || Request.Params["filename"] == null)
            {
                return;
            }
            num = int.Parse(Request.Params["num"]);
            urlperpage = int.Parse(Request.Params["urlperpage"]);
            filename = Request.Params["filename"];
//            string path = Page.Server.MapPath(filename);\
            string path = "d:\\website\\" + filename;
            string buf = System.IO.File.ReadAllText(path);
            string[] arrstr = buf.Split(';');
            int size = arrstr.Length;
            int start = (num-1) * urlperpage;
            int end = start + urlperpage;
            string str = "";
            for (int i = start; i < end; i++)
            {
                if (i >= size)
                {
                    break;
                }
                if (arrstr[i].Equals(""))
                {
                    continue;
                }
                if (i > arrstr.Length)
                {
                    break;
                }
                else
                {
                    str += arrstr[i] + ";";
                }
            }
            Response.Write(str);
        }
    }
}