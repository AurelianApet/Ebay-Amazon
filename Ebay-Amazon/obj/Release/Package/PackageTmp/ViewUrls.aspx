<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewUrls.aspx.cs" Inherits="Ebay_Amazon.ViewUrls" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title>ViewAllUrls</title>
     <link rel="stylesheet" href="smoot-jquery-ui.css"/>
    <script src="jquery-1.10.2.js"></script>
     <script src="jquery-ui.js"></script>
     <link rel="stylesheet" href="style.css"/>
    

    <script type="text/javascript">
        var ajax;
        var key = "";
        var amazonkey = "";
        var pagenum;
        var sendpage;
        var ebayresult;
        var ebayresultnum=0;
        var ebayresultSize=0;
        var totalsize = 0;
        var filename = "";
        var searchType = "";
        var subwidth = 0;
        var totalwidth;
        var ebaywidth;
        var pagewidth;
        var percent = 0;
        var progressBar;
        var selectedPage = 1;
        var totalpagenumber = 0;
        var urlperpage = 100;
        var showpage = 10;
        var footer;
        function Load() {
            totalwidth = window.innerWidth;

            $("#progressbar").progressbar({
                value: percent
            });

            footer = document.getElementById("footer");
            key = document.getElementById("keys").value;
            pagenum = document.getElementById("pagenums").value;
            pagewidth = totalwidth / pagenum;
            searchType = document.getElementById("searchType").value;
            if (key == "") {
                return;
            } else {
                var d = new Date();
                filename = d.getFullYear().toString() + d.getMonth().toString() + d.getDay().toString() + d.getHours().toString() + d.getMonth().toString() + d.getSeconds().toString();
                filename += ".txt";
                document.getElementById("loading").style.display = "block";
                sendpage = 1;
                viewUrlAjax(sendpage);
            }
        }
        function filewrite(fname, string) {
            createAjax();
            var page = "download.aspx";
            var param = "filename=" + fname + "&string=" + string;
            ajax.open("POST", page);
            ajax.setRequestHeader("Content-Type", "application/x-www-form-urlencoded;charset=UTF-8");
            ajax.setRequestHeader("Cache-Control", "no-cache, must-revalidate");
            ajax.setRequestHeader("Pragma", "no-cache");
            ajax.onreadystatechange = downcheck;
            ajax.send(param);

        }
        function downcheck() {
            if (ajax.readyState === 4) {
                if (ajax.status === 200) {
                    return;
                } else {
                    return;
                }
                   
            }
        }
        function createAjax() {
            if (window.XMLHttpRequest) {
                ajax = new XMLHttpRequest();
            } else if (window.ActiveXObject) {
                ajax = new ActiveXObject("Microsoft.XMLHTTP");
            }
        }

        function checkAjax() {
            if (ajax.readyState === 4) {
                if (ajax.status === 200) {
                    detailView();
                } else {
                    var label = document.getElementById("Label1");
                    label.textContent = "Ebay's response is fail";

                    subwidth += ebaywidth;
                    percent = (subwidth / totalwidth) * 100;
                    $("#progressbar").progressbar({
                        value: percent
                    });

                    ebayresultnum++;
                    if (ebayresultSize > (ebayresultnum + 1)) {

                        sendEbayAjax(ebayresultnum);
                    } else {
                        subwidth = sendpage * pagewidth;
                        percent = (subwidth / totalwidth) * 100;
                        $("#progressbar").progressbar({
                            value: percent
                        });
                        sendpage++;
                        if (sendpage > pagenum) {
                            var gad = totalsize % urlperpage;
                            if (gad == 0) {
                                totalpagenumber = Math.floor(totalsize / urlperpage);
                            } else {
                                totalpagenumber = Math.floor(totalsize / urlperpage) + 1;
                            }
                            addFooter(selectedPage);
                            $("#progressbar").progressbar({
                                value: false
                            });
                            document.getElementById("loading").style.display = "none";
                            document.getElementById("progressbar").style.display = "none";
                            var label = document.getElementById("Label1");
                            label.textContent = "Total size is " + totalsize;
                            var but = "<a href=\"javascript:onDownload()\" style='display:inline-block;font-size:10pt;width:100px'>Download</a>";
                            var panel = document.getElementById("urlpanel");
                            panel.innerHTML += but;
                            
                            return;
                        } else {
                            viewUrlAjax(sendpage);
                        }
                    }

                }
            }
        }

        function checkAjaxAmazon() {

            if (ajax.readyState === 4) {
                if (ajax.status === 200) {
                    AmazonView();
                } else {

                    var label = document.getElementById("Label1");
                    label.textContent = key + "'s request is fail";
                    subwidth += ebaywidth;
                    percent = (subwidth / totalwidth) * 100;
                    $("#progressbar").progressbar({
                        value: percent
                    });
                    ebayresultnum++;
                    if (ebayresultSize > (ebayresultnum + 1)) {
                        sendEbayAjax(ebayresultnum);
                    } else {
                        subwidth = sendpage * pagewidth;
                        percent = (subwidth / totalwidth) * 100;
                        $("#progressbar").progressbar({
                            value: percent
                        });
                        sendpage++;
                        if (sendpage > pagenum) {
                            var gad = totalsize % urlperpage;
                            if (gad == 0) {
                                totalpagenumber = Math.floor(totalsize / urlperpage);
                            } else {
                                totalpagenumber = Math.floor(totalsize / urlperpage) + 1;
                            }
                            addFooter(selectedPage);
                            $("#progressbar").progressbar({
                                value: false
                            });
                            document.getElementById("loading").style.display = "none";
                            document.getElementById("progressbar").style.display = "none";
                            var label = document.getElementById("Label1");
                            label.textContent = "Total size is " + totalsize;
                            var but = "<a href=\"javascript:onDownload()\" style='display:inline-block;font-size:10pt;width:100px'>Download</a>";
                            var panel = document.getElementById("urlpanel");
                            panel.innerHTML += but;
                            
                            return;
                        } else {
                            viewUrlAjax(sendpage);
                        }
                    }
                }
            }
        }

        function viewUrlAjax(num) {
            //for (var i = 0; i < pagenum; i++) {
                if (num <= pagenum) {
                    createAjax();
                    ajax.onreadystatechange = checkAjax;
                    //      var page = "ViewUrlAjax.aspx?key=" + key + "&num=" + (i+1);
                    var page = "ViewUrlAjax.aspx?key=" + key + "&num=" + num + "&searchType=" + searchType;
                    ajax.open("GET", page);
                    ajax.send(null);
                }
            //}
        }
        function detailView() {
            var str = ajax.responseText;
            if (str == "") {
                ebayresultnum++;
                if (ebayresultSize > (ebayresultnum + 1)) {
                    sendEbayAjax(ebayresultnum);
                } else {
                    subwidth = sendpage * pagewidth;
                    percent = (subwidth / totalwidth) * 100;
                    $("#progressbar").progressbar({
                        value: percent
                    });
                    sendpage++;
                    if (sendpage > pagenum) {
                        var gad = totalsize % urlperpage;
                        if (gad == 0) {
                            totalpagenumber = Math.floor(totalsize / urlperpage);
                        } else {
                            totalpagenumber = Math.floor(totalsize / urlperpage) + 1;
                        }
                        addFooter(selectedPage);
                        $("#progressbar").progressbar({
                            value: false
                        });
                        document.getElementById("loading").style.display = "none";
                        document.getElementById("progressbar").style.display = "none";
                        var label = document.getElementById("Label1");
                        label.textContent = "Total size is " + totalsize;
                        var but = "<a href=\"javascript:onDownload()\" style='display:inline-block;font-size:10pt;width:100px'>Download</a>";
                        var panel = document.getElementById("urlpanel");
                        panel.innerHTML += but;
                        
                        return;
                    } else {
                        viewUrlAjax(sendpage);
                    }
                }
                return;
            }
            var arrstr = str.split("!@#");
            var size = arrstr.length;
            ebayresultSize = size;

            ebaywidth = pagewidth/(ebayresultSize-1);

            ebayresult = arrstr;
            ebayresultnum = 0;
            sendEbayAjax(ebayresultnum);
     //       for (var i = 0; i < size; i++) {
       //         if (arrstr[i] != "") {
                   
         //       } else if(i==(size-1)&&arrstr[i]=="") {
                 //   document.getElementById("loading").style.display = "none";
               // }
    //        }
        }
        function sendEbayAjax(num) {
            if (ebayresult[num] != "") {
                createAjax();
                ajax.onreadystatechange = checkAjaxAmazon;
                ebayresult[num] = ebayresult[num].replace("&", "@@!!");
                var page = "scrapAmazonAjax.aspx?key=" + ebayresult[num];
                amazonkey = ebayresult[num];
                var label = document.getElementById("Label1");
                label.textContent = amazonkey + "'s request is send";
                ajax.open("GET", page);
                ajax.send(null);
            }
        }
        function onDownload() {
            location.href = "filedownload.aspx?filename=" + filename;

        }
        function downloadCk() {
            if (ajax.readyState === 4) {
                if (ajax.status === 200) {
                    var label = document.getElementById("Label1");
                    label.textContent = "download complete";
                    return;
                } else {
                    var label = document.getElementById("Label1");
                    label.textContent = "download fail";
                    return;
                }
            }
        }
        function AmazonView() {

            var str = ajax.responseText;
            var arr = str.split("!!@@##");
            var kk = arr[0];
            if (str == "") {
                var label = document.getElementById("Label1");
                label.textContent = kk + "'s result is empty";
            }
            var arrstr = arr[1].split("#@!");
            if (arrstr == "") {
                var label = document.getElementById("Label1");
                label.textContent = kk + "'s result is empty";
            } else {
                var size = arrstr.length;
                var obj = document.getElementById("contentPanel");
                var ss = "";
                for (var i = 0; i < size; i++) {
                    if (arrstr[i] != "") {
                        if (totalsize < urlperpage) {

                            obj.innerHTML += "<a href=\"" + arrstr[i] + "\" style='display:inline-block;font-size:10pt;width:1400px'>" + arrstr[i] + "</a>";
                        }
                        totalsize++;
                        ss += arrstr[i] + ";\n";
                    }
                }
                filewrite(filename, ss);
            }
            subwidth += ebaywidth;
            percent = (subwidth / totalwidth) * 100;
            $("#progressbar").progressbar({
                value: percent
            });

            ebayresultnum++;
            if (ebayresultSize > (ebayresultnum + 1)) {
                sendEbayAjax(ebayresultnum);
            } else {
                subwidth = sendpage * pagewidth;
                percent = (subwidth / totalwidth) * 100;
                $("#progressbar").progressbar({
                    value: percent
                });
                sendpage++;
                if (sendpage > pagenum) {
                    var gad = totalsize % urlperpage;
                    if (gad == 0) {
                        totalpagenumber = Math.floor(totalsize / urlperpage);
                    } else {
                        totalpagenumber = Math.floor(totalsize / urlperpage) + 1;
                    }
                    addFooter(selectedPage);
                    $("#progressbar").progressbar({
                        value: false
                    });
                    document.getElementById("loading").style.display = "none";
                    document.getElementById("progressbar").style.display = "none";
                    var label = document.getElementById("Label1");
                    label.textContent = "Total size is " + totalsize;
                    var but = "<a href=\"javascript:onDownload()\" style='display:inline-block;font-size:10pt;width:100px'>Download</a>";
                    var panel = document.getElementById("urlpanel");
                    panel.innerHTML += but;

                    return;
                } else {
                    viewUrlAjax(sendpage);
                }
            }
            
        }
        function addFooter(number) {
            footer.innerHTML = "";
            var startpagenumber;
            if ((number % showpage) == 0) {
                startpagenumber = number - showpage;
            } else {
                startpagenumber = Math.floor(number / showpage) * showpage;
            }
            var endpagenumber = startpagenumber + showpage;
            var prenum = startpagenumber;
            var nextnum = endpagenumber + 1;
            ViewFooter(prenum,startpagenumber+1,endpagenumber,nextnum);
        }
        
        function ViewFooter(pre, start, end, next) {
            if (pre > 0) {
                footer.innerHTML += "<a href='javascript:selectFooter(\"" + pre + "\")' class='linkStyle' style='display:inline-block;font-size:10pt;width:30px;text-decoration:none'>pre</a>"
            }
            for (var i = start; i <= end; i++) {
                if(i <= totalpagenumber){
                    footer.innerHTML += "<a href='javascript:selectFooter(\"" + i + "\")' class='linkStyle' style='display:inline-block;font-size:10pt;width:30px;text-decoration:none'>"+i+"</a>"
                 }
            }
            if (next <= totalpagenumber) {
                footer.innerHTML += "<a href='javascript:selectFooter(\"" + next + "\")' class='linkStyle' style='display:inline-block;font-size:10pt;width:30px;text-decoration:none'>next</a>"
            }
        }
        var ssh;
        function selectFooter(num) {
            ssh = num;
            createAjax();
            ajax.onreadystatechange = footercheck;
            var page = "urlfooterView.aspx?num=" + num + "&urlperpage=" + urlperpage + "&filename=" + filename;
            ajax.open("GET", page);
            ajax.send(null);
        }
        function footercheck() {
            if (ajax.readyState === 4) {
                if (ajax.status === 200) {
                    viewUrlFooterResult();
                } else {
                    return;
                }
            }
        }
        function viewUrlFooterResult() {
            var str = ajax.responseText;
            if (str == "") {
                alert("No Result");
                return;
            }
            var arrstr = str.split(";");
            var size = arrstr.length;
            if (size == 1) {
                alert("No Result");
                return;
            }
            var content = document.getElementById("contentPanel");
            content.innerHTML = "";
            for (var j = 0; j < size; j++) {
                if (arrstr[j] == "") {
                    continue;
                } else {
                    content.innerHTML += "<a href=\"" + arrstr[j] + "\" style='display:inline-block;font-size:10pt;width:1400px'>" + arrstr[j] + "</a>";
                }
            }
            addFooter(ssh);

        }
</script>
</head>
<body onload="Load()">
    <form id="form1" runat="server">
        <asp:HiddenField ID="keys" runat="server" Value="" />
        <asp:HiddenField ID="pagenums" runat="server" Value="" />
        <asp:HiddenField ID="searchType" runat="server" Value="" />
        <asp:Panel  ID="loading" runat="server" CssClass="loading">
            <img id="loading-image" src="load.gif" alt="wait for a minute..." />
        </asp:Panel>
        
        <div id="progressbar"></div>

        <asp:Panel ID="urlpanel" runat="server">
            <asp:Label ID="Label1" runat="server"></asp:Label>
        </asp:Panel>
       <asp:Panel ID="contentPanel" runat="server">

       </asp:Panel>
        <asp:Panel HorizontalAlign="Center" ID="footer" runat="server" BorderStyle="None"> 

        </asp:Panel>
     </form>
</body>
</html>
