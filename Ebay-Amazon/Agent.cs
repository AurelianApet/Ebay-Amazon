using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Xml.Linq;
using System.Data.SqlClient;

using SinJeonghun.Library.Web;

/// <summary>
/// Summary description for DBConnection
/// </summary>
public class Agent
{
    public static long AllLength = 0;
    public Agent()
	{
		
	}

    public static string GetResponse(SWebAgent a)
    {
        string strTemp = a.SendRequestAndReadAllHtml();
        long size = a.Res.ContentLength;
        AllLength += size;

        return strTemp;
    }
}
