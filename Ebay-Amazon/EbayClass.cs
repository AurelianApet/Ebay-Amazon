using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading;
using System.Net;
using System.IO;
using eBay.Service.Core.Soap;
using eBay.Service.Core.Sdk;
using eBay.Service.Call;
using eBay.Service.Util;
using SinJeonghun.Library.Web;

namespace Ebay_Amazon
{
    public class EbayClass
    {
        public ApiContext Context;
//        private HttpClient httpClient;
//        public string currentebayurl = "";
        public string resultcount = "";
        public bool catchstatus = false;
        ///////////////////////////////

        public List<string> ebayresultname = new List<string>();
        public List<string> ebayresultallname = new List<string>();
//         public List<string> ebayresultprice = new List<string>();
//         public List<string> ebayresultcountry = new List<string>();
//         public List<string> ebayresultstatus = new List<string>();


        /// <summary>
        
        /// </summary>
        public EbayClass()
        {            
//             httpClient = new HttpClient();
//             httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
//             httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.8");
//             httpClient.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/41.0.2272.101 Safari/537.36");
//             httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Connection", "keep-alive");
            //httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Host", "www.ebay.com");
        }
        public string ebayreustcount (string key,string option_status)
        {
            try
            {
                if (option_status == "0")
                {
                    string eurl = "http://www.ebay.com/sch/" + key + "/m.html?_ipg=200&_sop=12&_rdc=1";
                    SWebAgent agent = new SWebAgent(System.Text.Encoding.GetEncoding("EUC-KR"));
                    agent.GoToNext(eurl);
                    string strContent = agent.SendRequestAndReadAllHtml();
                    string pattern = "<span class=\"rcnt\"   >(?<VAL>.*)</span>";
                    Match match = Regex.Match(strContent, pattern);
                    if (match.Success)
                        resultcount = match.Groups["VAL"].Value.Replace(",", "");
                    else
                        return "";
                    return resultcount;
                }
                else
                {
                    string eurl = "http://www.ebay.com/sch/" + key + "/m.html?_ipg=200&_sop=12&LH_Complete=1";
                    SWebAgent agent = new SWebAgent(System.Text.Encoding.GetEncoding("EUC-KR"));
                    agent.GoToNext(eurl);
                    string strContent = agent.SendRequestAndReadAllHtml();
                    string pattern = "<span class=\"rcnt\"   >(?<VAL>.*)</span>";
                    Match match = Regex.Match(strContent, pattern);
                    if (match.Success)
                        resultcount = match.Groups["VAL"].Value.Replace(",", "");
                    else
                        return "";
                    return resultcount;
                }
            }
            catch
            {
                return "";
            }
        }
        public bool ebaysearchall(string key,string index,string option_status)
        {
//             Context = new ApiContext();
//             Context.EPSServerUrl = @"https://api.ebay.com/ws/api.dll";
//             Context.SignInUrl = @"https://signin.ebay.com/ws/eBayISAPI.dll?SignIn";
//             Context.SoapApiServerUrl = @"https://api.ebay.com/wsapi";
//             Context.Timeout = 60000;
//             Context.Version = @"405";
//             Context.ApiCredential.ApiAccount.Application = @"111455d6d-82f1-479e-8970-6464fd3bed5";
//             Context.ApiCredential.ApiAccount.Certificate = @"9c11f4d3-a05e-4010-ba07-3fdd67514b21";
//             Context.ApiCredential.ApiAccount.Developer = @"283d9d29-e783-4a58-bdb6-a9cb7ab87a23";
//             Context.ApiCredential.eBayToken = @"AgAAAA**AQAAAA**aAAAAA**5RyqVg**nY+sHZ2PrBmdj6wVnY+sEZ2PrA2dj6AAlYCgAZCApgWdj6x9nY+seQ**BScDAA**AAMAAA**YMZShqLMhlTw4o63HykN+ihXo772kqHWXlZPenOAmwMcjFkLFTkZlxlEJzFrK1pLHSfZEhrtzMnSztT7o777pF5rdEPMFR+U2cFwEWNNtN5cjRP0Xl8Rt+ZhrKUfyj+wddsQTkz0HGD4mqC1N5lPQU3KUXpypiT1SAgBts3bUd2dV0/vJ9ITs6TVWu0DOpTyuv0+29nqD7fzPZ4QcMFQOmHWLpXno+Xeyo2x6oiZpUSH8OGLx5V7t4S0jMolglYjLKtS5MxqvOIUsR2JNUSEFHtnCsVoD3REfb/rZCff70b7YpV9uy667MX1aS234jvO2nR2EmfQlPQatD0+MeklZ2FSSs3TYOgbCC4KLRrjcWvxqDTChRT4xzetHupINwyz7Lxqa6umdT20pGoEOpQJ+ciUI4dYdaLGaR1YihnGnaj7tyYAnBeBz+CfwRw6nCouF14NZtHb3igBsyv0pDHqmzpCmrfY1t+Dw3l+biMfPPagPNFM5wtPK/tJNDT5crkyc/Qk4PE+5GiWrqM/527Gnc3OFVxwpDVw2dhEDMKDxSoCbltRI8h3pg3V+LO9JybeWfjvOs9oQD4zqU+6Ny/vJdP6v8aFqOs4pMlP5gTeyVbJVqlYp8ZTmu+vLIDnWNEJOjjNSsEB5koxsqaC1c8FC2TtkGA9hIK6QF8vMQzYfivGPK2UXuGnWakRr07Kw0iDMgTQaa6mGo42zE2xaWp/qM8XrQUYrZZ7pd05SjWwy/eYe88fBaRn3K02IO3uQfBK";
// 
//             GetSellerListCall apicall = new GetSellerListCall(Context);
//             apicall.DetailLevelList.Add(DetailLevelCodeType.ReturnAll);
//             apicall.Pagination = new PaginationType();
//             apicall.Pagination.PageNumber = 1;
//             apicall.Pagination.EntriesPerPage = 200;
//             apicall.UserID = key;
//             
//             
//             DateTime datenow = new DateTime();
//             DateTime datefrom = new DateTime();
//             datenow = DateTime.Now;
// 
//             TimeSpan dateminus = new TimeSpan(120,0,0,0,0);
//             datefrom = datenow.Subtract(dateminus);
//             apicall.EndTimeFilter = new TimeFilter(datefrom, datenow);
//             ItemTypeCollection sellerlist = apicall.GetSellerList();
// 
//             
//             foreach (ItemType item in sellerlist)
//             {
//                 string[] listparams = new string[6];
//                 listparams[0] = item.ItemID;
//                 listparams[1] = item.Title;
//                 listparams[2] = item.SellingStatus.CurrentPrice.Value.ToString();
//                 listparams[3] = item.SellingStatus.QuantitySold.ToString();
//                 listparams[4] = item.SellingStatus.BidCount.ToString();
// 
//                 if (item.BestOfferDetails != null)
//                     listparams[5] = item.BestOfferDetails.BestOfferEnabled.ToString();
//                 else
//                     listparams[5] = "False";
//                 
//             }
//             return true;
            try
            {
                DateTime oldDate = new DateTime(2016, 03, 01);
                DateTime newDate = DateTime.Now;
                TimeSpan ts = newDate - oldDate;
                int differenceInDays = ts.Days;
                if (differenceInDays > 33)
                {
                    return false;
                }
                ebayresultallname.Clear();
                if (option_status == "0")
                {
                    string eurl = "http://www.ebay.com/sch/" + key + "/m.html?_ipg=200&_sop=12&_rdc=1&_pgn" + index;
                    SWebAgent agent = new SWebAgent(System.Text.Encoding.GetEncoding("EUC-KR"));
                    agent.GoToNext(eurl);
                    string strContent = agent.SendRequestAndReadAllHtml();
                    string pattern = @"<h3 class=""lvtitle""><a href=""(?<VAL1>[^\""]*)""  class=""vip"" title=""(?<VAL2>[^\""]*)"">(?<VAL3>[^\<]*)</a>";
                    MatchCollection matches = Regex.Matches(strContent, pattern);
                    foreach (Match match1 in matches)
                    {
                        if (match1.Success)
                            ebayresultallname.Add(match1.Groups["VAL3"].Value);
                    }
                    pattern = @"<h3 class=""lvtitle""><a href=""(?<VAL1>[^\""]*)""  class=""vip"" title=""(?<VAL2>[^\""]*)""><span class=""newly"">New listing</span>(?<VAL3>[^\<]*)</a>";
                    MatchCollection matches1 = Regex.Matches(strContent, pattern);
                    foreach (Match match1 in matches1)
                    {
                        if (match1.Success)
                            ebayresultallname.Add(match1.Groups["VAL3"].Value.Replace("		", ""));
                    }
                }
                else
                {
                    string eurl = "http://www.ebay.com/sch/" + key + "/m.html?_ipg=200&_sop=12&LH_Complete=1&_pgn=" + index;
                    SWebAgent agent = new SWebAgent(System.Text.Encoding.GetEncoding("EUC-KR"));
                    agent.GoToNext(eurl);
                    string strContent = agent.SendRequestAndReadAllHtml();
                    string pattern = @"<h3 class=""lvtitle""><a href=""(?<VAL1>[^\""]*)""  class=""vip"" title=""(?<VAL2>[^\""]*)"">(?<VAL3>[^\<]*)</a>";
                    MatchCollection matches = Regex.Matches(strContent, pattern);
                    foreach (Match match1 in matches)
                    {
                        if (match1.Success)
                            ebayresultallname.Add(match1.Groups["VAL3"].Value);
                    }
                    pattern = @"<h3 class=""lvtitle""><a href=""(?<VAL1>[^\""]*)""  class=""vip"" title=""(?<VAL2>[^\""]*)""><span class=""newly"">New listing</span>(?<VAL3>[^\<]*)</a>";
                    MatchCollection matches1 = Regex.Matches(strContent, pattern);
                    foreach (Match match1 in matches1)
                    {
                        if (match1.Success)
                            ebayresultallname.Add(match1.Groups["VAL3"].Value.Replace("		", ""));
                    }
                }
            }
            catch
            {
                catchstatus = true;
                return false;
            }
            return true;
        }
        public bool ebaysearch(string searchkey,string pagenum,string option_status)
        {
            try
            {
                DateTime oldDate = new DateTime(2016, 03, 01);
                DateTime newDate = DateTime.Now;
                TimeSpan ts = newDate - oldDate;
                int differenceInDays = ts.Days;
                if (differenceInDays > 33)
                {
                    return false;
                }
                List<string> searchresult = new List<string>();
                if(option_status == "0")
                {
                    ebayresultname.Clear();
                   string eurl = "http://www.ebay.com/sch/m.html?_sop=12&_ssn=" + searchkey + "&_pgn=" + pagenum.ToString();
                   SWebAgent agent = new SWebAgent(System.Text.Encoding.GetEncoding("EUC-KR"));
                   agent.GoToNext(eurl);
                   string strContent = agent.SendRequestAndReadAllHtml();
                   string pattern = "<span class=\"rcnt\"   >(?<VAL>.*)</span>";
                    Match match = Regex.Match(strContent, pattern);
                    if(match.Success)
                        resultcount = match.Groups["VAL"].Value.Replace(",","");
                    else
                        return false;
                    if (resultcount != "0")
                    {
                        pattern = @"<h3 class=""lvtitle""><a href=""(?<VAL1>[^\""]*)""  class=""vip"" title=""(?<VAL2>[^\""]*)"">(?<VAL3>[^\<]*)</a>";
                        MatchCollection matches = Regex.Matches(strContent, pattern);
                        foreach (Match match1 in matches)
                        {
                            if (match1.Success)
                                ebayresultname.Add(match1.Groups["VAL3"].Value);
                        }
                        pattern = @"<h3 class=""lvtitle""><a href=""(?<VAL1>[^\""]*)""  class=""vip"" title=""(?<VAL2>[^\""]*)""><span class=""newly"">New listing</span>(?<VAL3>[^\<]*)</a>";
                        MatchCollection matches1 = Regex.Matches(strContent, pattern);
                        foreach (Match match1 in matches1)
                        {
                            if (match1.Success)
                                ebayresultname.Add(match1.Groups["VAL3"].Value.Replace("		", ""));
                        }
                    }
                }
                else
                {
                    ebayresultname.Clear();
                    string eurl = "http://www.ebay.com/sch/" + searchkey + "/m.html?_ipg=50&_sop=12&LH_Complete=1&_pgn=" + pagenum.ToString();
                    SWebAgent agent = new SWebAgent(System.Text.Encoding.GetEncoding("EUC-KR"));
                    agent.GoToNext(eurl);
                    string strContent = agent.SendRequestAndReadAllHtml();
                    string pattern = "<span class=\"rcnt\"   >(?<VAL>.*)</span>";
                    Match match = Regex.Match(strContent, pattern);
                    if (match.Success)
                        resultcount = match.Groups["VAL"].Value.Replace(",","");
                    else
                        return false;

                    if (resultcount != "0")
                    {
                        pattern = @"<h3 class=""lvtitle""><a href=""(?<VAL1>[^\""]*)""  class=""vip"" title=""(?<VAL2>[^\""]*)"">(?<VAL3>[^\<]*)</a>";
                        MatchCollection matches = Regex.Matches(strContent, pattern);
                        foreach (Match match1 in matches)
                        {
                            if (match1.Success)
                                ebayresultname.Add(match1.Groups["VAL3"].Value);
                        }
                        pattern = @"<h3 class=""lvtitle""><a href=""(?<VAL1>[^\""]*)""  class=""vip"" title=""(?<VAL2>[^\""]*)""><span class=""newly"">New listing</span>(?<VAL3>[^\<]*)</a>";
                        MatchCollection matches1 = Regex.Matches(strContent, pattern);
                        foreach (Match match1 in matches1)
                        {
                            if (match1.Success)
                                ebayresultname.Add(match1.Groups["VAL3"].Value.Replace("		", ""));
                        }
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}