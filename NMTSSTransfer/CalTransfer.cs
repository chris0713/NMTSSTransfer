﻿using System;
using System.Configuration;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading;
using log4net;
using log4net.Config;
using System.IO;
using System.Windows.Forms;
using HtmlAgilityPack;


namespace NMTSSTransfer
{
    class CalTransfer
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(CalTransfer));
        HttpWebRequest Request;
        CookieContainer m_Cookies = new CookieContainer();
        HtmlAgilityPack.HtmlDocument m_EvtDoc = new HtmlAgilityPack.HtmlDocument();


        /**
         *  loc : Save Location
          * url : login URL
          * uid : User ID
          * pwd : User Pwd
          */


        public JobResult getCVS(string loc , string uid, string pwd , List<string> items , ref int n , ref int max , ref System.ComponentModel.BackgroundWorker worker)
        {

            //string searchPage = @"http://www.unfranchise.com.tw/index.cfm?action=meetings.nmSvSe&mtgSrchType=b";
            string searchPage = @"https://tw.unfranchise.com/index.cfm?action=meetings.unfMeetingSearchResults";

            JobResult results = new JobResult();
            // Login into Unfranchise System.
            bool LoginSuccess = false;
            //Login Unfranchise.com.tw
            for (int i = 1; i <= 3; i++)
            {
                LoginSuccess = login(uid, pwd);
                if (LoginSuccess == true)
                    break;
                else
                    Thread.Sleep(3000);
            }

            if (LoginSuccess == false)
            {
                log.Error("登入失敗，無法執行下載作業");
                results.IsDownloadSuccess = false;
                results.ErrMsg.Add("登入失敗，無法執行下載作業");
                return results;
            }

            try
            {
                //Get all Hidden input tag in Search Page 
                string searchMainPage = @"https://tw.unfranchise.com/index.cfm?action=meetings.unfMeetingSearch";
                string strSPageCnt = HTTPTool.DownloadContentsUTF8(Request.CookieContainer, searchMainPage, string.Empty, 2);
                HtmlAgilityPack.HtmlDocument htmlDoc = new HtmlAgilityPack.HtmlDocument();
                htmlDoc.LoadHtml(strSPageCnt);
                var inputs = htmlDoc.DocumentNode.Descendants("input");
                foreach (var input in inputs)
                {
                     if ((input.Attributes["type"].Value == "hidden") && (input.Attributes["name"] != null))
                         items.Add(input.Attributes["name"].Value + "=" + input.Attributes["value"].Value);
                }
            }
            catch (Exception ex9)
            {
                log.Fatal("Error at getCVS()-Step0- (Get All Hide Input Tag):" + ex9.Message);
                throw new Exception("Error at getCVS()-Step0(Get All Hide Input Tag):" + ex9.Message);
            }

            //Compose Post Data
            StringBuilder sbPostData = new StringBuilder();
            for (int i = 0; i < items.Count; i++)
            {
                if (i == items.Count - 1)
                    sbPostData.Append(items[i]);
                else
                    sbPostData.Append(items[i]).Append("&");
            }
            string strPostData = sbPostData.ToString();
            //HttpWebRequest Request;
            byte[] data = new ASCIIEncoding().GetBytes(strPostData);

            // get each meeting data from all links
            StringBuilder meetings = new StringBuilder();
            try
            {
                #region 保留程式碼
                //Request = (HttpWebRequest)WebRequest.Create(searchPage);
                //Request.Method = "POST";
                //Request.ContentType = "application/x-www-form-urlencoded";
                //Request.ContentLength = data.Length;
                //Request.KeepAlive = true;
                //Request.CookieContainer = m_Cookies;

                ////send the request Data
                //Stream newStream = Request.GetRequestStream();
                //newStream.Write(data, 0, data.Length);
                //newStream.Close();
                ////Get Response 
                //HttpWebResponse Response = (HttpWebResponse)Request.GetResponse();
                
                //using (StreamReader sr = new StreamReader(Response.GetResponseStream(), Encoding.UTF8))
                //{
                //    string tmpStr = sr.ReadToEnd();
                //    List<string> MeetingLinks = new List<string>();
                //    string[] aryValue = GetAttribute(tmpStr, "a", "href");
                //    for (int k = 0; k < aryValue.Length; k++)
                //    {
                //        //只取出我要的Meeting Links
                //        if (aryValue[k].IndexOf("index.cfm?action=meetings.nmMtgDtl&ID=") >= 0)
                //            MeetingLinks.Add(aryValue[k].Trim());
                //    }
                //}
                #endregion
                DateTime t1 = DateTime.Now;

                

                string SearchResults = HTTPTool.DownloadContentsUTF8(Request.CookieContainer, searchPage, strPostData, 1);
                DateTime t2 = DateTime.Now;
                double dd = new TimeSpan(t2.Ticks - t1.Ticks).TotalSeconds;
                

                List<string> MeetingLinks = new List<string>();
                //確認是否被導到登入頁
                if (SearchResults.IndexOf("顯示活動的") < 0)
                    log.Error("無法取得查詢結果-_-|||");
                else
                    results.IsDownloadSuccess = true;

                //string[] aryValue = GetAttribute(SearchResults, "a", "href");
                //for (int k = 0; k < aryValue.Length; k++)
                //{
                //    //只取出我要的Meeting Links
                //    if (aryValue[k].IndexOf("index.cfm?action=meetings.nmMtgDtl&ID=") >= 0)
                //        MeetingLinks.Add(aryValue[k].Trim());
                //}
                HtmlAgilityPack.HtmlDocument resultDoc = new HtmlAgilityPack.HtmlDocument();
                resultDoc.LoadHtml(SearchResults);
                var links = resultDoc.DocumentNode.Descendants("a");
                foreach (var link in links)
                {
                    if ((link.Attributes["class"]!= null) && (link.Attributes["href"] != null))
                       if(link.Attributes["class"].Value == "event")
                         MeetingLinks.Add(link.Attributes["href"].Value);
                }
                max = MeetingLinks.Count;
                worker.RunWorkerAsync(0); 
                
                //Get all Meeting Links Detail Contents.
                for (int j = 0; j < MeetingLinks.Count; j++)
                {
                    n = j;
                    string url = @"https://tw.unfranchise.com/" + MeetingLinks[j];
                    string SingleInfo = HTTPTool.DownloadContentsUTF8(m_Cookies , url, "", 2);
                    string strMeetingInfo = getMeetingInfo2(SingleInfo);
                    if (strMeetingInfo != string.Empty)
                        meetings.AppendLine(getMeetingInfo2(SingleInfo));
                    else
                    {
                        results.ERRORURL.Add(url);
                        results.ErrMsg.Add("此筆資料查無其細節內容(" + url + "), 行事曆產生將跳過此筆資料。");
                        results.IsDownloadSuccess = false;
                    }
                    Application.DoEvents();   
                }
                
            }
            catch (Exception ex)
            {
                log.Fatal("Error at getCVS()-Step1:" + ex.Message);
                throw new Exception("Error at getCVS()-Step1:" + ex.Message);
            }

            //Compose to csvFile
            string header = @"""主旨"",""開始日期"",""開始時間"",""結束日期"",""結束時間"",""全天"",""提醒開啟/關閉"",""提醒日期"",""提醒時間"",""會議召集人"",""出席者"",""列席者"",""會議資源"",""地點"",""忙閒狀態"",""私人"",""津貼"",""帳目資訊"",""敏感度"",""描述"",""優先順序"",""類別""";
            try
            {
                //清除不必要的字詞
                meetings.Replace("&nbsp;", "");
                //輸出成CSV檔
                using (StreamWriter sw = new StreamWriter(loc , false,Encoding.UTF8))
                {
                    sw.WriteLine(header);
                    sw.Write(meetings.ToString());
                }
            }
            catch (Exception ex1)
            {
                log.Fatal(@"Error at getCVS()-Step 2:" + ex1.Message);
                //throw new Exception("Error at getCVS()-Step 2:" + ex1.Message);
                results.IsDownloadSuccess = false;
                results.ErrMsg.Add(@"Error at getCVS()-Step 2:" + ex1.Message);
            }

            //背景作業設為可取消
            worker.WorkerSupportsCancellation = true;
            return results;
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


        private bool login(string uid, string pwd)
        {
            bool IsLoginSuccess = false;
            try
            {
                //Login Unfranchise.com.tw
                //string strPostData = @"usrID=" + uid + @"&usrPwd=" + pwd;
                string strPostData = @"username=" + uid + @"&password=" + pwd;
                byte[] data = new ASCIIEncoding().GetBytes(strPostData);
                Request = (HttpWebRequest)WebRequest.Create(@"https://tw.unfranchise.com/index.cfm?action=main.unfLogin-process");
                Request.Method = "POST";
                Request.ContentType = "application/x-www-form-urlencoded";
                Request.ContentLength = data.Length;
                Request.KeepAlive = true;
                Request.CookieContainer = m_Cookies;
                

                //send the request Data
                Stream newStream = Request.GetRequestStream();
                newStream.Write(data, 0, data.Length);
                newStream.Close();
                //Get Response 
                HttpWebResponse Response = (HttpWebResponse)Request.GetResponse();


                //判斷是否是在首頁：

                //StreamReader sr = new StreamReader(Response.GetResponseStream(), Encoding.UTF8);
                //String rt_Login = sr.ReadToEnd();
                //if (rt_Login.IndexOf("新聞與通告") > 0)
                //{
                //    IsLoginSuccess = true;
                //    //foreach (Cookie cookie in Response.Cookies)
                //    //    m_Cookies.Add(cookie);
                //}
                //else
                //{
                //    log.Error("Can not find KeyWord:新聞與通告, It may be redirect to a Offical Error Page.");
                //}

                //if (Response.ResponseUri.ToString().IndexOf("action=news.naMain") < 0)
                //    throw new Exception("Can not find Response URI with 'action=news.naMain', It may be redirect to a Offical Error Page.");
                //else
                //    IsLoginSuccess = true;

                StreamReader sr = new StreamReader(Response.GetResponseStream(), Encoding.UTF8);
                String rt_Login = sr.ReadToEnd();

                string[] AryMainPageConfirmWords = ConfigurationManager.AppSettings["MainPageConfirm"].ToString().Split(',');
                foreach (string keyWord in AryMainPageConfirmWords)
                {
                    if (Response.ResponseUri.ToString().IndexOf(keyWord) > 0)
                        IsLoginSuccess = true;
                }
                if (IsLoginSuccess == false)
                    throw new Exception("Can not find Response URI with 'action=news.naMain', It may be redirect to a Offical Error Page.");


                
                //記錄UID ,PWD
                try
                {
                    string strCurrentAP = System.Reflection.Assembly.GetExecutingAssembly().Location;
                    using (StreamWriter sw = new StreamWriter(strCurrentAP + "IDInfo.txt", false, Encoding.GetEncoding(950)))
                    {
                        sw.WriteLine(uid + "," + pwd);
                    }
                }
                catch (Exception ex1)
                {
                    log.Fatal(@"Write ID/PWD : " + ex1.Message);
                    throw new Exception("Write ID/PWD : " + ex1.Message);
                }

                return IsLoginSuccess;
            }
            catch (Exception ex)
            {
                log.Error("Error at login() :" + ex.Message);
                return IsLoginSuccess;
            }
            
        }
        #region 改放至HTTPTool工具區
        /*
        /// <summary>
        /// Get Data From Remote HttpServer 
        /// </summary>
        /// <param name="url"> URL </param>
        /// <param name="postData">POST Data String</param>
        /// <param name="ActionType">Action Type 1:POST , 2: GET</param>
        /// <returns></returns>
        private string DownloadContents(string url , string postData , int ActionType )
        {
            try
            {
                //Login Unfranchise.com.tw
                string strPostData = postData;
                byte[] data = new ASCIIEncoding().GetBytes(strPostData);
                Request = (HttpWebRequest)WebRequest.Create(url);
                switch (ActionType)
                {
                    case 1:
                        Request.Method = "POST";
                    break;
                    case 2:
                        Request.Method = "GET";
                    break;
                }
                Request.ContentType = "application/x-www-form-urlencoded";
                Request.ContentLength = data.Length;
                Request.KeepAlive = true;
                Request.CookieContainer = m_Cookies;
                //send the request Data
                if (ActionType == 1)
                {
                    Stream newStream = Request.GetRequestStream();
                    newStream.Write(data, 0, data.Length);
                    newStream.Close();
                }
                //Get Response 
                HttpWebResponse Response = (HttpWebResponse)Request.GetResponse();
                string rtnValues ; 
                using (StreamReader sr = new StreamReader(Response.GetResponseStream(), Encoding.UTF8))
                {
                    rtnValues = sr.ReadToEnd();
                }
                return rtnValues;
            }
            catch (Exception ex)
            {
                log.Error("Error at login() :" + ex.Message);
                throw new Exception ("Error at DownloadContents():"+ex.Message);
            }

        }
        */
        #endregion
        /** 舊寫法不用
        private string getMeetingInfo(string htmlCode)
        {
            nmtssVO vo = new nmtssVO();
            //string strDate = string.Empty; //日期
            //string strSpeaker = string.Empty;//演講人
            //string strStartTime = string.Empty;//開始時間
            //string strEndTime = string.Empty;//結束時間
            //string strStackholder = string.Empty;//主持人
            //string strPhoneNum = string.Empty;//電話
            //string strAddress = string.Empty;//會場地址
            //string strComments = string.Empty;//意見
            //string strSubject = string.Empty;//主旨

            // Date 
            vo.MeetingDate = getWords(ref htmlCode, @"日期：</td>");
            //先做基本格式確認，若不符，此筆資料就不Parsing 了。
            if (vo.MeetingDate == string.Empty)
                return string.Empty;
            
 
            // Speaker Name 
            vo.Speaker = getWords(ref htmlCode, @"演講人");

            //strStartTime
            string strStartTime = getWords(ref htmlCode, @"開始時間");
            strStartTime = strStartTime.Insert(strStartTime.IndexOf(":") + 3, ":00");
            vo.StartTime = changeFormate(strStartTime);

            //strEndTime
            string strEndTime = getWords(ref htmlCode, @"結束時間");
            strEndTime = strEndTime.Insert(strEndTime.IndexOf(":") + 3, ":00");
            vo.EndTime =  changeFormate(strEndTime);
            
            //Stackholder
            vo.Host = getWords(ref htmlCode, @"主持人");

            //Phone
            vo.Tel = getWords(ref htmlCode, @"電話");

            //會場電話
            vo.Tel2 = getWords(ref htmlCode, @"會場電話");

            //Address
            string strAddress = getWords(ref htmlCode, @"會場地址");
            vo.Locate2 = strAddress.Replace("<br />", "-").Replace("\r", "").Replace("\n", "").Trim();

            //Comments
            string strNotes = getWords(ref htmlCode, @"意見");
            vo.Notes = strNotes.Replace("<br />", "-").Replace("\r", "").Replace("\n", "").Trim();

            
            //會場地點
            string strLocate1 = getWords(ref htmlCode, @"會場地點");
            vo.Locate1 = strLocate1.Replace("<br />", "-").Replace("\r", "").Replace("\n", "").Trim();

            //會議主旨
            string strSubject = getSubject(ref htmlCode);
            vo.MeetingName = strSubject.Replace("<br />", "-").Replace("\r", "").Replace("\n", "").Trim();

            //Compose the csv file for Google Canlendar 

            return composeCanlenderText(vo);
                       
        }
*/

        //2015-09-29 Modified.
        private string getMeetingInfo2(string htmlCode)
        {
            nmtssVO vo = new nmtssVO();
            m_EvtDoc.LoadHtml(htmlCode);
            var node= m_EvtDoc.DocumentNode.SelectSingleNode("//div[@class=\"column\"]");
            //主辦者
            vo.Host = node.SelectSingleNode("./div[contains(label,'主辦者')]/text()").InnerText.Replace(System.Environment.NewLine,string.Empty);
            //電子郵件:
             vo.EMail = node.SelectSingleNode("./div[contains(label,'電子郵件')]/text()").InnerText.Trim().Replace(System.Environment.NewLine, string.Empty); ;
            //主持人(可能會帶入演講人姓名)
            vo.STACKHOLDER = node.SelectSingleNode("./div[contains(label,'主持人姓名')]/text()").InnerText.Replace(System.Environment.NewLine, string.Empty);
            //主持人電話:
            vo.Tel = node.SelectSingleNode("./div[contains(label,'主持人電話')]/text()").InnerText.Trim().Replace(System.Environment.NewLine, string.Empty); ;
            //演講嘉賓:
            vo.Speaker = node.SelectSingleNode("./div[contains(label,'演講嘉賓')]/text()").InnerText.Trim().Replace(System.Environment.NewLine, string.Empty); ;
            //若沒填演講嘉賓，改查"主講人"
            if (vo.Speaker == string.Empty)
                vo.Speaker = node.SelectSingleNode("//p/label[contains(.,'主講人')]/following-sibling::div[1]").InnerText.Trim();
            //其他意見:
            vo.Notes = node.SelectSingleNode("//div/label[contains(.,'其他意見')]/following-sibling::div[1]").InnerText.Trim();
            //日期
            vo.MeetingDate = node.SelectSingleNode("//div[@class=\"eventDate\"]/text()").InnerText.Trim();
            //Get Time Period ~
            string strTimePeriod = node.SelectSingleNode("//div[@class=\"eventDate\"]/following-sibling::div[1]/text()").InnerText.Trim();
            vo.StartTime = strTimePeriod.Split('-')[0].Trim();
            vo.EndTime = strTimePeriod.Split('-')[1].Trim();

            //會場地址
            vo.Locate1 = node.SelectSingleNode("//div[@class=\"eventDate\"]/following-sibling::div[3]/text()").InnerText.Trim()
                  + " " + node.SelectSingleNode("//div[@class=\"eventDate\"]/following-sibling::div[2]/text()").InnerText.Trim();
            vo.Locate2 = getReginNameInString(node.SelectSingleNode("//div[@class=\"eventDate\"]/following-sibling::div[3]/text()").InnerText.Trim());
            //會議主旨
            vo.MeetingName = node.SelectSingleNode("//h3/span[@class='meetingID']/following-sibling::text()").InnerText.Trim();
            
            return composeCanlenderText(vo);

        }

        private static string changeFormate(string dt)
        {
            string newFormate = string.Empty;
            if ("am".Equals(dt.Substring(dt.Length - 2)))
                newFormate = @"上午 " + dt.Substring(0, dt.Length - 2);
            if ("pm".Equals(dt.Substring(dt.Length - 2)))
                newFormate = @"下午 " + dt.Substring(0, dt.Length - 2);
            return newFormate;
        }

        private string composeCanlenderText(nmtssVO vo)
        {

            //string contents = @""主旨,"2009/5/14","上午 09:00:00","2009/5/14","上午 09:30:00","假","假","2009/5/14","上午 08:45:00",,,,,"地點","2","假",,,"普通","comments","普通"
            //地方研討會，有人不填演講人姓名。在此進行判斷
            if (vo.Speaker.Equals(string.Empty))
            {
                string strPatten = string.Empty;
                if (vo.STACKHOLDER.LastIndexOf(":") > 0) strPatten = ":";
                if (vo.STACKHOLDER.LastIndexOf("：") > 0) strPatten = "：";
                if(strPatten != string.Empty)
                    vo.Speaker = vo.STACKHOLDER.Substring(vo.STACKHOLDER.LastIndexOf(strPatten)+1,3);
            }
            StringBuilder sb = new StringBuilder();
            sb.Append("\"").Append("[").Append(vo.Locate2).Append("]").Append(converter(vo.MeetingName)).Append("-").Append(vo.Speaker).Append("\",")
              .Append("\"").Append(vo.MeetingDate).Append("\",")
              .Append("\"").Append(vo.StartTime).Append("\",")
              .Append("\"").Append(vo.MeetingDate).Append("\",")
              .Append("\"").Append(vo.EndTime).Append("\",")
              .Append("\"假\",\"假\"").Append(",")
              .Append("\"").Append(vo.MeetingDate).Append("\",")
              .Append("\"").Append(vo.StartTime).Append("\"").Append(@",,,,,")
              .Append("\"").Append(vo.Locate1).Append("\"").Append(",\"2\",\"假\",,,\"普通\",")
              .Append("\"").Append(@"主辦人:").AppendLine(vo.Host).Append("演講人:").AppendLine(vo.Speaker).Append(@"(").Append("主持人:").Append(vo.STACKHOLDER).Append("-").Append(vo.Tel).Append(")")
              .AppendLine("").Append(vo.Notes).Append("\"").Append(",\"普通\"");

            return sb.ToString();

        }

        private string converter(string orginal)
        {
            string strValue = orginal;
            try
            {
                switch (orginal)
                {
                    case "超連鎖事業說明會":
                            strValue = "UBP";
                        break;
                    case "成功五要訣":
                            strValue = "B5";
                        break;
                    case "地方研討會":
                            strValue = "LS";
                        break;
                    default:
                    break;
                }

                return strValue;
            }
            catch (Exception ex)
            {
                log.Error("error at converter()"+ex.Message);
                return strValue;
            }

        }
        /**
         * Search the keyword from HTML Code
         */

        private string getWords(ref string htmlCode, string keyword)
        {
            
            string targetString = string.Empty;
            int j1 = 0;
            j1 = htmlCode.IndexOf(keyword);
            if (j1 > 1)
            {
                int j2 = htmlCode.IndexOf(@"<td>", j1);
                int j3 = htmlCode.IndexOf(@"</td>", j2 + 4);
                targetString = htmlCode.Substring(j2 + 4, (j3 - (j2 + 4)));
            }

            return targetString;


        }

        /**
         * Search the Subject from HTML Code
         */

        private static string getSubject(ref string htmlCode)
        {
            try
            {
                string targetString = string.Empty;
                int j1 = 0;
                j1 = htmlCode.IndexOf("<table border=\"0\" cellpadding=\"2\" cellspacing=\"2\">");
                if (j1 > 1)
                {
                    int j2 = htmlCode.IndexOf("<td colspan=\"2\">", j1);
                    int j3 = htmlCode.IndexOf(@"</td>", j2 + 16);
                    targetString = htmlCode.Substring(j2 + 16, (j3 - (j2 + 16)));
                }

                return targetString;
            }
            catch (Exception ex)
            {
                log.Fatal(@"Method[getWords]-" +ex.Message, ex);
                return "";
            }
        }

        private string getReginNameInString(string addr)
        {
            return  Regex.Replace(addr, @"[\u0000-\u007F]", string.Empty).Substring(0,2);

        }

    }
}
