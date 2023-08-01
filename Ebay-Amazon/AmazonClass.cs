using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Threading;
using System.Net;
using AmazonProductAdvtApi;
using SinJeonghun.Library.Web;


namespace Ebay_Amazon
{
    public class AmazonClass
    {
//        private HttpClient httpClient;
        public int pagenum = 0;
        public List<string> amazonresultname = new List<string>();
        public string amazonresultcount = "";
        public List<string> amazonresulturl = new List<string>();
        public List<string> amazonallurl = new List<string>();
        public List<string> amazon_ebay_sameurl = new List<string>();

        public bool catchstatus = false;
        public bool captcha = false;

        const String DESTINATION = "https://ecs.amazonaws.com/onca/soap?Service=AWSECommerceService";
        const String MY_AWS_ID = "AKIAJXW47F5ZN4BBMXIQ";
        const String MY_AWS_SECRET = "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";

        
        public AmazonClass()
        {            
//             httpClient = new HttpClient();
//             httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
//             httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.8");
//             httpClient.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/41.0.2272.101 Safari/537.36");
//             httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Connection", "keep-alive");
//             httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Host", "www.amazon.com");
//             var result2 = httpClient.GetAsync("http://www.amazon.com/").Result;
//             result2.EnsureSuccessStatusCode();
//             result2.Dispose();
        }

        string checkSearchKey(string searchkey)
        {
            string result="";
            int strlength = result.Length;
            for (int i = 0; i < strlength; i++)
            {

            }
            return result;
        }
        public bool amazonallurlviewer(string productname)
        {
            amazon_ebay_sameurl.Clear();
            AWSECommerceService ecs = new AWSECommerceService();
            ecs.Destination = new Uri(DESTINATION);
            AmazonHmacAssertion amazonHmacAssertion = new AmazonHmacAssertion(MY_AWS_ID, MY_AWS_SECRET);
            ecs.SetPolicy(amazonHmacAssertion.Policy());

            ItemSearch search = new ItemSearch();
            search.AWSAccessKeyId = MY_AWS_ID;
            search.AssociateTag = "xyz";
/*
            

            ItemSearchRequest error = new ItemSearchRequest();
            error.ResponseGroup = new string[] { "Errors" };
            error.SearchIndex = "All";
            error.Keywords = productname;
            search.Request = new ItemSearchRequest[] { error };

            string errors = ecs.ItemSearchstr(search);

            
            string err = @"<Error>";
            
            bool errFlag = Regex.IsMatch(errors, err);
*/
            ItemSearchRequest request = new ItemSearchRequest();
            request.ResponseGroup = new string[] { "ItemAttributes" };
            request.SearchIndex = "All";
            request.Keywords = productname;
            search.Request = new ItemSearchRequest[] { request };
            string response = ecs.ItemSearchstr(search);

/*


            FileStream file = null;
            StreamWriter write = null;
            try
            {
                string path = "c:\\inetpub\\wwwroot\\test\\";
                string filename = "testALL.txt";
                filename = path + filename;
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
                write.WriteLine(response + "!!!!!!!");
                write.Close();
                file.Close();
            }
            catch
            {
                write.Close();
                file.Close();
            }


*/




            if (response.Contains("We did not find any matches for your request."))
            {
/*
                file = null;
                write=null;
                try
                {
                     string path = "c:\\inetpub\\wwwroot\\test\\";
                     string filename = "test.txt";
                    filename = path + filename;
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
                    write.WriteLine(response + "!!!!!!!");
                    write.Close();
                    file.Close();
                }
                catch
                {
                    write.Close();
                    file.Close();
                }

*/
                string eurl = "http://www.amazon.com/s/ref=nb_sb_noss?url=search-alias=aps&field-keywords=" + productname;
                SWebAgent agent = new SWebAgent(System.Text.Encoding.GetEncoding("EUC-KR"));
                agent.GoToNext(eurl);
                string strcontent = agent.SendRequestAndReadAllHtml();
                string pat = @"<a class=""a-link-normal s-access-detail-page  a-text-normal"" title=""(?<VAL1>[^\""]*)"" href=""(?<VAL2>[^\""]*)""><h2 class=""a-size-medium a-color-null s-inline s-access-title a-text-normal"">(?<VAL3>[^\<]*)</h2>";
                MatchCollection mates = Regex.Matches(strcontent,pat);
                foreach (Match match in mates)
                {
                    amazon_ebay_sameurl.Add(match.Groups["VAL2"].Value);
                    break;
                }
            }
            else
            {
                

                string pattern = @"<Description>Technical Details</Description><URL>(?<VAL>[^\<]*)</URL>";
                MatchCollection matches = Regex.Matches(response, pattern);
                foreach (Match match in matches)
                {
                    amazon_ebay_sameurl.Add(match.Groups["VAL"].Value);
                    break;
                }
            }
            return true;
        }
        public bool amazonsearchall(string searchkey)
        {
            try
            {
                string eurl = "http://www.amazon.com/s/ref=nb_sb_noss?url=search-alias=aps&field-keywords=" + searchkey;
                SWebAgent agent = new SWebAgent(System.Text.Encoding.GetEncoding("EUC-KR"));
                agent.GoToNext(eurl);
                string strcontent = agent.SendRequestAndReadAllHtml();
//                var result = httpClient.GetAsync("http://www.amazon.com/s/ref=nb_sb_noss?url=search-alias=aps&field-keywords=" + searchkey).Result;
//                result.EnsureSuccessStatusCode();
//                string strcontent = result.Content.ReadAsStringAsync().Result;
                if (strcontent.Contains("did not match any products") == true)
                    return false;

                string pattern = @"<h2 id=""s-result-count"" class=""a-size-base a-spacing-small a-spacing-top-small a-text-normal"">(?<VAL>[^\<]*)<span>";
                Match match = Regex.Match(strcontent, pattern);
                if (match.Success)
                    amazonresultcount = match.Groups["VAL"].Value;
                if (amazonresultcount.Contains("result for"))
                {
                    amazonresultcount = amazonresultcount.Replace(" result for ", "").Replace(",", "");
                }
                else
                {
                    amazonresultcount = amazonresultcount.Replace(" results for ", "");
                    match = Regex.Match(amazonresultcount, "of (?<VAL>.*)");
                    if (match.Success)
                        amazonresultcount = match.Groups["VAL"].Value;
                    amazonresultcount = amazonresultcount.Replace("of ", "").Replace(",", "");
                }
//                result.Dispose();
                for (int i = 1; i <= int.Parse(amazonresultcount); i++ )
                {
                    eurl = "http://www.amazon.com/s/ref=nb_sb_noss?url=search-alias=aps&field-keywords=" + searchkey + "&page=" + i.ToString();
                    agent = new SWebAgent(System.Text.Encoding.GetEncoding("EUC-KR"));
                    agent.GoToNext(eurl);
                    strcontent = agent.SendRequestAndReadAllHtml();
//                    result = httpClient.GetAsync("http://www.amazon.com/s/ref=nb_sb_noss?url=search-alias=aps&field-keywords=" + searchkey + "&page=" + i.ToString()).Result;
//                    result.EnsureSuccessStatusCode();
//                    strcontent = result.Content.ReadAsStringAsync().Result;
                    if (strcontent.Contains("did not match any products") == true)
                        break;
                    string pat = @"<a class=""a-link-normal s-access-detail-page  a-text-normal"" title=""(?<VAL1>[^\""]*)"" href=""(?<VAL2>[^\""]*)""><h2 class=""a-size-medium a-color-null s-inline s-access-title a-text-normal"">(?<VAL3>[^\<]*)</h2>";
                    MatchCollection tr = Regex.Matches(strcontent, pat);
                    foreach (Match m in tr)
                    {
                        amazonallurl.Add(m.Groups["VAL2"].Value.Replace("&amp;", "&"));
                    }
//                    result.Dispose();
                }
                    return true;
            }
            catch
            {
                return false;
            }
            
        }

        public bool amazonsearch(string searchkey,string num)
        {
         
            pagenum = int.Parse(num);
            try
            {
                catchstatus = false;
                if(num == "1")
                {
                    string eurl = "http://www.amazon.com/s/ref=nb_sb_noss?url=search-alias=aps&field-keywords=" + searchkey;
                    SWebAgent agent = new SWebAgent(System.Text.Encoding.GetEncoding("EUC-KR"));
                    agent.GoToNext(eurl);
                    string strcontent = agent.SendRequestAndReadAllHtml();
//                    var result = httpClient.GetAsync("http://www.amazon.com/s/ref=nb_sb_noss?url=search-alias=aps&field-keywords=" + searchkey).Result;
//                    result.EnsureSuccessStatusCode();
//                     string strcontent = result.Content.ReadAsStringAsync().Result;
                    if (strcontent.Contains("did not match any products") == true)
                        return false;
                    if (strcontent.Contains("Robot Check") == true)
                    {
                        catchstatus = true;
                        return false;
                    }
                    string pattern = @"<h2 id=""s-result-count"" class=""a-size-base a-spacing-small a-spacing-top-small a-text-normal"">(?<VAL>[^\<]*)<span>";
                    Match match = Regex.Match(strcontent,pattern);
                    if(match.Success)
                        amazonresultcount=match.Groups["VAL"].Value;
                    if (amazonresultcount.Contains("result for"))
                    {
                        amazonresultcount = amazonresultcount.Replace(" result for ", "").Replace(",","");
                    }
                    else
                    {
                        amazonresultcount = amazonresultcount.Replace(" results for ", "");
                        match = Regex.Match(amazonresultcount, "of (?<VAL>.*)");
                        if (match.Success)
                            amazonresultcount = match.Groups["VAL"].Value;
                        amazonresultcount = amazonresultcount.Replace("of ", "").Replace(",","");
                    }
                    string pat = @"<a class=""a-link-normal s-access-detail-page  a-text-normal"" title=""(?<VAL1>[^\""]*)"" href=""(?<VAL2>[^\""]*)""><h2 class=""a-size-medium a-color-null s-inline s-access-title a-text-normal"">(?<VAL3>[^\<]*)</h2>";
                    MatchCollection tr = Regex.Matches(strcontent, pat);
                    foreach (Match m in tr)
                    {
                        amazonresultname.Add(m.Groups["VAL3"].Value);
                        amazonresulturl.Add(m.Groups["VAL2"].Value.Replace("&amp;", "&"));
                    }

//                    result.Dispose();                    
                }
                else
                {
                    string eurl = "http://www.amazon.com/s/ref=nb_sb_noss?url=search-alias=aps&field-keywords=" + searchkey + "&page=" + num;
                    SWebAgent agent = new SWebAgent(System.Text.Encoding.GetEncoding("EUC-KR"));
                    agent.GoToNext(eurl);
                    string strcontent = agent.SendRequestAndReadAllHtml();
//                    var result = httpClient.GetAsync("http://www.amazon.com/s/ref=nb_sb_noss?url=search-alias=aps&field-keywords=" + searchkey + "&page=" + num).Result;
//                    result.EnsureSuccessStatusCode();
//                     string strcontent = result.Content.ReadAsStringAsync().Result;

                    if (strcontent.Contains("did not match any products") == true)
                        return false;
                    if (strcontent.Contains("Robot Check") == true)
                    {
                        catchstatus = true;
                        return false;
                    }
                    string pattern = @"<h2 id=""s-result-count"" class=""a-size-base a-spacing-small a-spacing-top-small a-text-normal"">(?<VAL>[^\<]*)<span>";
                    Match match = Regex.Match(strcontent, pattern);
                    if (match.Success)
                        amazonresultcount = match.Groups["VAL"].Value;
                    if (amazonresultcount.Contains("result for"))
                    {
                        amazonresultcount = amazonresultcount.Replace(" result for ", "").Replace(",", "");
                    }
                    else
                    {
                        amazonresultcount = amazonresultcount.Replace(" results for ", "");
                        match = Regex.Match(amazonresultcount, "of (?<VAL>.*)");
                        if (match.Success)
                            amazonresultcount = match.Groups["VAL"].Value;
                        amazonresultcount = amazonresultcount.Replace("of ", "").Replace(",", "");
                    }


                    string pat = @"<a class=""a-link-normal s-access-detail-page  a-text-normal"" title=""(?<VAL1>[^\""]*)"" href=""(?<VAL2>[^\""]*)""><h2 class=""a-size-medium a-color-null s-inline s-access-title a-text-normal"">(?<VAL3>[^\<]*)</h2>";
                    MatchCollection tr = Regex.Matches(strcontent, pat);
                    foreach (Match m in tr)
                    {
                        amazonresultname.Add(m.Groups["VAL3"].Value);
                        amazonresulturl.Add(m.Groups["VAL2"].Value.Replace("&amp;", "&"));
                    }
//                    result.Dispose();
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