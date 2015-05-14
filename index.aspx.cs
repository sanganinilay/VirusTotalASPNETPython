/*
 Version : 1.0
 About : This is a very simple project about implementing and calling VirusTotal's python scripts and APIs in ASP.NET.
 Special Thanks: Virus Total, Json.NET - Newtonsoft, html5up.net
 Notes : 
 -Please go though the comments in the code for further instructions. 
 -Enter your api key in index.aspx(hdnKey),index.aspx.cs and FileScan.py
 - Read the comments very carefully.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;
using System.Web.Script.Serialization;
using System.IO;
using System.Net;
using System.Text;
using System.Data;
using System.Xml;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


public partial class index : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void btnScan_Click(object sender, EventArgs e)
    {
        string s;
        Process proc = new Process();

        string strFilePAth = string.Empty;
        strFilePAth = txtFile.Text;
      
        proc.StartInfo.FileName = "python.exe";
       
        // proc.StartInfo.Arguments = 

            /* Uncomment the above line and enter the arguments as per the below example
             * 
             * Example : proc.StartInfo.Arguments =  @"VTPython\FileScan.py D:\test.LOG"; //(FileScan.py is the python script located in VTPython folder and full file path of test.Log is captured from textbox control as it is the file to be scanned )"
             * 
             */

        proc.StartInfo.UseShellExecute = false;
        proc.StartInfo.RedirectStandardOutput = true;
        proc.StartInfo.CreateNoWindow = true;
        proc.Start();
        while ((s = proc.StandardOutput.ReadLine()) != null)
            lblInput.Text += s;
        string strFinal = lblInput.Text.ToString();
        string strValue = string.Empty;
        JavaScriptSerializer ser = new JavaScriptSerializer();
        Dictionary<string,object> dictMD5 = ser.Deserialize<Dictionary<string, object>>(strFinal);
        object ovalueMD5 = dictMD5["md5"];

        WebRequest requestVirusTotalAPI = WebRequest.Create("https://www.virustotal.com/vtapi/v2/file/report");
        requestVirusTotalAPI.Method = "POST";

        //string postData = "resource=" + ovalueMD5 + "&apikey=" + "Enter your API Key";

        /* Uncomment the above line and enter the API key as per the below example
        Example :  string postData = "resource=" + ovalueMD5 + "&apikey=" + "Enter your API Key";
        */

        byte[] byteArrayPostData = Encoding.UTF8.GetBytes(postData);
        requestVirusTotalAPI.ContentType = "application/x-www-form-urlencoded";
        requestVirusTotalAPI.ContentLength = byteArrayPostData.Length;
        Stream dataStream = requestVirusTotalAPI.GetRequestStream();
        dataStream.Write(byteArrayPostData, 0, byteArrayPostData.Length);
        dataStream.Close();
        WebResponse responseAPI = requestVirusTotalAPI.GetResponse();
        string strStatus;
        strStatus = ((HttpWebResponse)responseAPI).StatusDescription;
        if(strStatus.Equals("OK"))
        {
            dataStream = responseAPI.GetResponseStream();
            StreamReader streaMreader = new StreamReader(dataStream);
            using(var serverResponse  = (WebResponse)requestVirusTotalAPI.GetResponse())
            {
                using (var reader = new StreamReader(serverResponse.GetResponseStream()))
                {
                    string strobjText = reader.ReadToEnd();
                    JObject json = JObject.Parse(strobjText);
                    var values = json.ToObject<Dictionary<string, object>>();
                    DataTable dt = new DataTable();
                    dt.Columns.Add("Vendor");
                    dt.Columns.Add("Detection");
                    foreach (JToken childJSON in json.Children())
                    {
                        var jproperty = childJSON as JProperty;
                        if (jproperty != null)
                        {
                            if (jproperty.Name.Equals("scans"))
                            {
                                foreach (JToken jTokenVal in childJSON)
                                {
                                    foreach (JToken jTokenValGchild in jTokenVal)
                                    {
                                        var propJSON = jTokenValGchild as JProperty;
                                        if (propJSON != null)
                                        {
                                            DataRow dr = dt.NewRow();
                                            dr["Vendor"] = propJSON.Name;
                                            if ((((Newtonsoft.Json.Linq.JProperty)(((propJSON).Value).First))).Name.Equals("detected"))
                                            {
                                                string strFalseTrue = (string)(((Newtonsoft.Json.Linq.JProperty)(((propJSON).Value).First))).Value;
                                                dr["Detection"] = strFalseTrue;
                                            }
                                            dt.Rows.Add(dr);
                                        }
                                    }
                                }
                            }
                        }
                        grdFileScanStatus.DataSource = dt;
                        grdFileScanStatus.DataBind();
                    }
                }
            }
        }

    }
}