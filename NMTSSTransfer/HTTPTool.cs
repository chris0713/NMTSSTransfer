using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading;
using log4net;
using log4net.Config;
using System.IO;

namespace NMTSSTransfer
{
    public class HTTPTool
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(HTTPTool));

        /// <summary>
        /// Get Data From Remote HttpServer 
        /// </summary>
        /// <param name="url"> URL </param>
        /// <param name="postData">POST Data String</param>
        /// <param name="ActionType">Action Type 1:POST , 2: GET</param>
        /// <returns></returns>
        public  static string DownloadContentsUTF8(CookieContainer c , string url, string postData, int ActionType)
        {
            HttpWebRequest Request =null;
            try
            {
                
                //Login Unfranchise.com.tw
                string strPostData = postData;
                byte[] data= new UTF8Encoding().GetBytes(strPostData);
                               
                Request = (HttpWebRequest)WebRequest.Create(url);
                Request.ContentType = "application/x-www-form-urlencoded";
                //Request.ContentType = "text/html; charset=UTF-8";
                Request.ContentLength = data.Length;
                Request.KeepAlive = false;
                Request.CookieContainer = c;
                
                switch (ActionType)
                {
                    case 1:
                        Request.Method = "POST";
                        break;
                    case 2:
                        Request.Method = "GET";
                        break;
                }
                
                //send the request Data
                if (ActionType == 1)
                {
                    Stream newStream = Request.GetRequestStream();
                    newStream.Write(data, 0, data.Length);
                    newStream.Close();
                }
                //Get Response 
                HttpWebResponse Response = (HttpWebResponse)Request.GetResponse();
                string rtnValues;
                using (StreamReader sr = new StreamReader(Response.GetResponseStream(),Encoding.UTF8))
                {
                    rtnValues = sr.ReadToEnd();
                }
                return rtnValues;
            }
            catch (Exception ex)
            {
                log.Error("Error at DownloadContents() :" + ex.Message);
                
                throw new Exception("Error at DownloadContents():" + ex.Message);
            }
            finally
            {
                try
                {
                    if (Request != null)
                    {
                        Request.GetResponse().Close();
                        Request.GetRequestStream().Close();
                    }
                }
                catch (Exception exx)
                {}
            }

        }



        public static string[] GetAttribute(string strHtml, string strTagName, string strAttributeName)
        {
            List<string> lstAttribute = new List<string>();
            string strPattern = string.Format(
              "<\\s*{0}\\s+.*?(({1}\\s*=\\s*\"(?<attr>[^\"]+)\")|({1}\\s*=\\s*'(?<attr>[^']+)')|({1}\\s*=\\s*(?<attr>[^\\s]+)\\s*))[^>]*>"
              , strTagName
              , strAttributeName);
            MatchCollection matchs = Regex.Matches(strHtml, strPattern, RegexOptions.IgnoreCase);
            foreach (Match m in matchs)
            {
                lstAttribute.Add(m.Groups["attr"].Value);
            }
            return lstAttribute.ToArray();
        }
    }
}
