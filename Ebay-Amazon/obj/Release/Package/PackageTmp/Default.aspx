<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Ebay_Amazon.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

    
<head runat="server">
    <title></title>
    <script>
        var openindex = "";
        var ajax;
        var index;
        var linkUrl;
        var openPage;
        function viewURL(str) {

            var str1 = str.replace("!@#", "\"");
            str1 = str1.replace("#@!", "'");
            location.href = "URLView.aspx?key="+str1;
        }
        function createAjax() {
            if (window.XMLHttpRequest) { 
                ajax = new XMLHttpRequest();
            } else if (window.ActiveXObject) { 
                ajax = new ActiveXObject("Microsoft.XMLHTTP");
            }
        }
        function Refresh(id) {
            
            var text = document.getElementById(id).innerHTML.split("<input id=\"!@#$%\" type=\"hidden\">");
  
            document.getElementById(id).innerHTML = text[0];
        
            openindex = "";
        }
        function sendUrlRequest(url, str, num) {
            if (openindex == str && num == 0) {
                Refresh(str);
                
            } else {
                if (num == 0) {
                    num = 1;
                }
                document.getElementById("loading").style.display = "block";
                var str1 = url.replace("!@#", "\"");
                str1 = str1.replace("#@!","'");
                linkUrl = url;
                index = str;
                createAjax();
                ajax.onreadystatechange = checkAjax;
                var page = "ViewDetailInfo.aspx?str=" + str1 + "&num=" + num;
                ajax.open("GET", page);
                ajax.send(null);

            }
        }
        function checkAjax() {
            if (ajax.readyState === 4) {
                if (ajax.status === 200) {
                    detailView();
                } else {
                    alert('There was a problem with the request.');
                    document.getElementById("loading").style.display = "none";
                }
            }
        }
        function detailView() {
            if (openindex != "") {
                Refresh(openindex);
            }
            var str = ajax.responseText;
            if (str == "") {
                alert("No Result");
                document.getElementById("loading").style.display = "none";
                return;
            }
            

            var info = str.split("@@@@@");
            var num = info[2];
            var pages;
            var totalnum = info[1];
            if (info[0] == "") {
                alert("Request result is Capthcha!");
                document.getElementById("loading").style.display = "none";

                return;
            }
            
            var rows = info[0].split("::!@#");
            var size = rows.length;
            var count = 0;
            for(var i=0;i<size;i++){
                if(rows[i]!=""){
                    count++;
                 }
            }
            size = count;
            if ((totalnum % size) == 0) {
                pages = totalnum / size;
            } else {
                pages = totalnum / size + 1;
            }
            var row = document.getElementById(index);
            row.innerHTML += "<input id=\"!@#$%\" type=\"hidden\">";
            for (var i = 0; i < size; i++) {
                if (rows[i] == "") {
                    continue;
                }
                var value = rows[i].split("@#$");
                //var addrow = "<table class='tableSubstyle'><tr style='height:10px;'><td align='center' style='width:500px;'><a href='" + value[1] + "' style='font-size:10pt;font-weight:bold;'>" + value[0] + "</a></td><td style='width:200px;'></td></tr></table>";
                var addrow = "<table class='tableSubstyle'><tr style='height:10px;'><td align='center' style='width:500px;'><a href='" + value[1] + "' style='font-size:10pt;font-weight:bold;'>" + value[0] + "</a></td></tr></table>";
                
                row.innerHTML += addrow;
            }
            var footer = "<div id='footerPanel' style='border-style:None;text-align:center;' class='divstyle'>";
            var minnum = num - (num % 10);
            var maxnum = minnum + 10;
            
            if (minnum > 0) {
                footer += "<a href='javascript:sendUrlRequest(\"" + linkUrl + "\",\"" + index + "\"," + minnum + ")' class='linkStyle' style='display:inline-block;font-size:10pt;width:30px;text-decoration:none'>prev</a>"
            }
            for (var i = minnum; i < maxnum; i++) {
                if (i < pages) {
                    footer += "<a href='javascript:sendUrlRequest(\"" + linkUrl + "\",\"" + index + "\"," + (i + 1) + ")' class='linkStyle' style='display:inline-block;font-size:10pt;width:30px;text-decoration:none'>" + (i + 1) + "</a>"
                }

            }

            if (maxnum < pages) {
                footer += "<a href='javascript:sendUrlRequest(\"" + linkUrl + "\",\"" + index + "\"," + (maxnum + 1) + ")' class='linkStyle' style='display:inline-block;font-size:10pt;width:30px;text-decoration:none'>next</a>";
            }
            footer += "</div>"
            row.innerHTML += footer;
            openindex = index;
            document.getElementById("loading").style.display = "none";
        }
        function convertPage(num) {
            document.getElementById("footer").value = num;
            document.getElementById("next").value = "";
            document.getElementById("prev").value = "";
            document.getElementById("form1").submit();
        }
        function nextPage(shownum) {
            document.getElementById("footer").value = "";
            document.getElementById("next").value = shownum;
            document.getElementById("prev").value = "";
            document.getElementById("form1").submit();
        }
        function prevPage(shownum) {
            document.getElementById("footer").value = "";
            document.getElementById("next").value = "";
            document.getElementById("prev").value = shownum;
            document.getElementById("form1").submit();
        }
        function Button2_Click() {
            var pagenum = document.getElementById("pagenum").value;
//            alert(pagenum);
//            document.getElementById("contentPanel").innerHTML = "";
//            document.getElementById("footPanel").innerHTML = "";
//            document.getElementById("loading").style.display = "block";
            
            var searchkey = document.getElementById("TextBox1").value;
            var searchtype = document.getElementById("sType").value;
//            sendAjaxUrls(searchkey);
            location.href = "ViewUrls.aspx?key=" + searchkey + "&pagenum=" + pagenum + "&searchType=" + searchtype;
        }
        function sendAjaxUrls(str) {
            createAjax();
            ajax.onreadystatechange = viewAjax;
            var page = "ViewUrlAjax.aspx?key=" + str;
            ajax.open("GET", page);
            ajax.send(null);
        }
        function viewAjax() {
            if (ajax.readyState === 4) {
                if (ajax.status === 200) {
                    AllUrls();
                } else {
                    alert('There was a problem with the request.');
                    document.getElementById("loading").style.display = "none";
                }
            }
        }
        function AllUrls() {
            var str = ajax.responseText;
            var content = document.getElementById("contentPanel");
            content.innerHTML += "<a href=\"str\" style='display:inline-block;font-size:10pt;width:1400px;text-decoration:none'>" + str + "</a>";
        }
    </script>
    <link rel="stylesheet" href="Style.css" type="text/css"/> 
</head>
<body>
    <asp:panel ID="loading" runat="server"> 
        <img id="loading-image" src="load.gif" alt="wait for a minute..." />
    </asp:panel>
    <form id="form1" runat="server">
        <asp:HiddenField ID="pagenum" runat="server" Value="" />
        <asp:HiddenField ID="sType" runat="server" />
        <input type="hidden" value="" id="footer" name="footer" />
        <input type="hidden" value="" id="next" name="next"/>
        <input type="hidden" value="" id="prev" name="prev"/>
        <asp:Panel runat="server" ID="titleHeader" CssClass="headerStyle">
            Product Seller
        </asp:Panel>
         <asp:Panel runat="server" ID="subtitlePanel1" CssClass="subheaderStyle">
             www.ebay.com  &  www.amazon.com
        </asp:Panel>
        <asp:Panel runat="server" ID="Panel1" HorizontalAlign="Center">
             <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                 <asp:ListItem Value="0">All Listing</asp:ListItem>
                 <asp:ListItem Value="1" Selected="True">Only Sold Listing</asp:ListItem>
             </asp:RadioButtonList>
        </asp:Panel>
        <asp:Panel runat="server" ID="body" HorizontalAlign="Center">
            <asp:TextBox ID="TextBox1" runat="server" CssClass="textStyle"></asp:TextBox>
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Search" CssClass="buttonStyle"/>

            <p style="width: 653px; margin-left: 98px">
                <asp:Label ID="Label1" runat="server" CssClass="textStyle"></asp:Label>
            </p>
            <asp:Panel ID="aaa" runat="server" HorizontalAlign="center" CssClass="panelStyle">

            </asp:Panel>
            <asp:Panel ID="contentPanel" runat="server" HorizontalAlign="Center" CssClass="panelStyle">

            </asp:Panel>
        </asp:Panel>
        <asp:Panel runat="server" ID="footPanel" EnableTheming="True" HorizontalAlign="Center" BorderStyle="None">
        </asp:Panel>
    </form>
</body>
</html>
