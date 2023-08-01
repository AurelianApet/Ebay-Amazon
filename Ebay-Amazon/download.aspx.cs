using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace Ebay_Amazon
{
    public partial class download : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (Request.Params["filename"] == null || Request.Params["string"] == null)
            {
                return;
            }
            else
            {
                FileStream file = null;
                StreamWriter write=null;
                try
                {
                     string path = Page.Server.MapPath("./download.aspx");
                     int len = path.Length;
                     path = path.Substring(0, len - 13);
//                    string path = "d:\\website\\";
                    string filename = Request.Params["filename"];
                    filename = path + filename;
                    string str = Request.Params["string"];
                    
                    if (!File.Exists(filename))
                    {
                        file = File.Create(filename);

                    }
                    else
                    {
                        file = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                    }
                    write = new StreamWriter(file, System.Text.Encoding.Default);
                    file.Seek(file.Length, SeekOrigin.Begin);
                    write.WriteLine(str + "\n");
                    write.Close();
                    file.Close();
                    Response.Write("OK");
                }
                catch
                {
                    write.Close();
                    file.Close();
                    return;
                }
            }
        }
    }
}