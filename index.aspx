<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="index" %>

<!DOCTYPE HTML>
<!--
	Prologue by HTML5 UP
	html5up.net | @n33co
	Free for personal and commercial use under the CCA 3.0 license (html5up.net/license)
-->

<html>
	<head>
		<title>Virus Total Implementation ( Dot Net + HTML5 + Python )</title>
		<meta http-equiv="content-type" content="text/html; charset=utf-8" />
		<meta name="description" content="" />
		<meta name="keywords" content="" />
		<!--[if lte IE 8]><script src="css/ie/html5shiv.js"></script><![endif]-->
		<script src="js/jquery.min.js"></script>
		<script src="js/jquery.scrolly.min.js"></script>
		<script src="js/jquery.scrollzer.min.js"></script>
		<script src="js/skel.min.js"></script>
		<script src="js/skel-layers.min.js"></script>
		<script src="js/init.js"></script>
		<noscript>
			<link rel="stylesheet" href="css/skel.css" />
			<link rel="stylesheet" href="css/style.css" />
			<link rel="stylesheet" href="css/style-wide.css" />
		</noscript>
		<!--[if lte IE 9]><link rel="stylesheet" href="css/ie/v9.css" /><![endif]-->
		<!--[if lte IE 8]><link rel="stylesheet" href="css/ie/v8.css" /><![endif]-->
	</head>
<body>
    <form id="form1" runat="server">
    <!-- Header -->
			<div id="header" class="skel-layers-fixed">

				<div class="top">

					<!-- Logo -->
						<div id="logo">
							
							<h1 id="title">Virus Total</h1>
							<p>ASP.NET Python Responsive</p>
						</div>

					<!-- Nav -->
						<nav id="nav">
							<ul>
								<li><a href="index.aspx" id="top-link" class="skel-layers-ignoreHref"><span class="icon fa-home">File Scan</span></a></li>
							</ul>
						</nav>
						
				</div>
				
				<div class="bottom">				
				</div>
			
			</div>
            
		<!-- Main -->
			<div id="main">

				<!-- Intro -->

					<section id="top" class="one dark cover">
                  <asp:HiddenField ID="hdnVirusTotal" runat="server" Value="www.virustotal.com" />  
                  <asp:HiddenField ID="hdnKey" runat="server" Value="Enter your API Key" />
						<div class="container">
                            <table runat="server">
                                <tr runat="server" id="trFileUpload">
                                    <td>
                                      <asp:TextBox runat="server" ID="txtFile" Width="50%" placeholder="Enter File Path"></asp:TextBox><br />
                                        <asp:Button ID="btnScan" runat="server" Text="Scan" OnClick="btnScan_Click" />
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <table runat="server" width="100%">
                             <tr runat="server">
                                    <td>
                                        <asp:GridView ID="grdFileScanStatus" runat="server"></asp:GridView>
                                    </td>
                                </tr>
                            </table>
						</div>
                        <asp:Label id="lblInput" runat="server" Visible="false"></asp:Label>
                        
					</section>
					
			</div>

		<!-- Footer -->
			<div id="footer">
				<!-- Copyright -->
					<ul class="copyright">
                        <li>Special Thanks: <a href="http://www.virustotal.com">VirusTotal</a> | <a href="http://html5up.net">HTML5 UP</a> | <a href="www.newtonsoft.com/json">Json.Net</a>  </li>
					</ul>
				
			</div>

	 </form>
</body>
</html>
