using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using log4net;
using log4net.Config;
using System.IO;

namespace GCalTranfer
{
    class CalTransfer
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(CalTransfer));

       /**
         * url : login URL
         * uid : User ID
         * pwd : User Pwd
         */


        public void getCVS(string url,string uid , string pwd , string[] links)
        {
            string strPostData = @"usrID=" + uid +@"&usrPwd=" + pwd ;
            byte[] data = new ASCIIEncoding().GetBytes(strPostData);

            HttpWebRequest Request = (HttpWebRequest)WebRequest.Create(@"http://www.unfranchise.com.tw/index.cfm?action=main.authUUser");
            
            Request.Method = "POST";
            Request.ContentType = "application/x-www-form-urlencoded";  
            Request.ContentLength = data.Length;
            Request.KeepAlive = false;
            Request.CookieContainer = new CookieContainer(); 
            
            //send the request Data
            Stream newStream = Request.GetRequestStream();
            newStream.Write(data, 0, data.Length);
            newStream.Close();
            //Get Response 
            HttpWebResponse Response = (HttpWebResponse)Request.GetResponse();
            StreamReader sr = new StreamReader(Response.GetResponseStream(), Encoding.UTF8);
            foreach (Cookie cookie in Response.Cookies)
            {
                Request.CookieContainer.Add(cookie);
            }
            
            // get each meeting data from all links
            StringBuilder meetings = new StringBuilder();

            for (int k = 0; k < links.Length; k++)
            {
                try
                {
                    Request = (HttpWebRequest)WebRequest.Create(links[k]);
                    Request.Method = "POST";
                    Request.ContentType = "application/x-www-form-urlencoded";
                    Request.ContentLength = data.Length;
                    Request.KeepAlive = false;
                    Request.CookieContainer = new CookieContainer();

                    //send the request Data
                    newStream = Request.GetRequestStream();
                    newStream.Write(data, 0, data.Length);
                    newStream.Close();
                    //Get Response 
                    Response = (HttpWebResponse)Request.GetResponse();
                    sr = new StreamReader(Response.GetResponseStream(), Encoding.UTF8);
                    foreach (Cookie cookie in Response.Cookies)
                    {
                        Request.CookieContainer.Add(cookie);
                    }

                    string tmpStr = sr.ReadToEnd();
                    string newURL = string.Empty;
                    int i = tmpStr.IndexOf(@"location.href='");
                    if (i > 0)
                    {
                        tmpStr = tmpStr.Substring(i + 15);
                        newURL = tmpStr.Substring(0, tmpStr.IndexOf("'"));

                    }
                    sr.Close();

                    
                    //Get Meetings Info
                    meetings.AppendLine(getMeetingInfo(tmpStr));

                }
                catch (Exception ex)
                {
                    log.Fatal(ex.Message, ex);
                }

            }//end-forloop

                
            //Compose to csvFile
            string header = @"""主旨"",""開始日期"",""開始時間"",""結束日期"",""結束時間"",""全天"",""提醒開啟/關閉"",""提醒日期"",""提醒時間"",""會議召集人"",""出席者"",""列席者"",""會議資源"",""地點"",""忙閒狀態"",""私人"",""津貼"",""帳目資訊"",""敏感度"",""描述"",""優先順序"",""類別"" ";

                try
                {
                  using (StreamWriter sw = new StreamWriter(@"c:\Calendar.csv"))
                    {

                        sw.WriteLine(header);
                        sw.Write(meetings.ToString());
                    }
                }
                catch (Exception ex1)
                {
                    log.Fatal(@"Method:[getCVS]" + ex1.Message);
                }
            
         
        }
        

        public string getMeetingInfo(string htmlCode)
        {
            string strDate = string.Empty; //日期
            string strSpeaker = string.Empty;//演講人
            string strStartTime = string.Empty;//開始時間
            string strEndTime = string.Empty;//結束時間
            string strStackholder = string.Empty;//主持人
            string strPhoneNum = string.Empty;//電話
            string strAddress = string.Empty;//會場地址
            string strComments = string.Empty;//意見
            string strSubject = string.Empty;//主旨

           
            // Date 
            strDate = getWords(ref htmlCode, @"日期：</td>");
            
            // Speaker Name 
            strSpeaker = getWords(ref htmlCode, @"演講人");

            //strStartTime
            strStartTime = getWords(ref htmlCode, @"開始時間");

            //strEndTime
            strEndTime = getWords(ref htmlCode, @"結束時間");

            //Stackholder
            strStackholder = getWords(ref htmlCode, @"主持人");

            //Phone
            strPhoneNum = getWords(ref htmlCode, @"電話");

            //Address
            strAddress = getWords(ref htmlCode, @"會場地址");

            //Comments
            strComments = getWords(ref htmlCode, @"意見");

            //Subjects
            strSubject = getWords(ref htmlCode, @"演講者機構成員" ,true) ;

            //Compose the csv file for Google Canlendar 

            return composeCanlenderText(strSubject, strDate, strStartTime, strEndTime, strStackholder, strPhoneNum, strAddress, strComments, strSpeaker);
                        
        }

        public string composeCanlenderText(string strSubject , string strDate,string strStartTime, string strEndTime,string strStackholder,string strPhoneNum,string strAddress,string strComments,string strSpeaker)
        {
            
            //string contents = @""主旨,"2009/5/14","上午 09:00:00","2009/5/14","上午 09:30:00","假","假","2009/5/14","上午 08:45:00",,,,,"地點","2","假",,,"普通","comments","普通"

            StringBuilder sb = new StringBuilder();
            sb.Append("\"").Append(strSubject).Append("\",")
              .Append("\"").Append(strDate).Append("\",")
              .Append("\"").Append(strStartTime).Append(":00\",")
              .Append("\"").Append(strDate).Append("\",")
              .Append("\"").Append(strEndTime).Append(":00\",")
              .Append("\"假\",\"假\"").Append(",")
              .Append("\"").Append(strDate).Append("\",")
              .Append("\"").Append(strStartTime).Append(":00\"").Append(@",,,,,")
              .Append("\"").Append(strAddress).Append("\"").Append(",\"2\",\"假\",,,\"普通\",")
              .Append("\"").Append(@"主持人:").AppendLine(strStackholder).Append("演講人:").AppendLine(strSpeaker).Append(@"(").Append(strPhoneNum).Append(")")
              .AppendLine("").Append(strComments).Append("\"").Append(",\"普通\"");

            return sb.ToString();

        }
        /**
         * Search the keyword from HTML Code
         */

        public string getWords(ref string htmlCode , string keyword)
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

        public static string getWords(ref string htmlCode, string keyword, bool flag)
        {
            try
            {
                string targetString = string.Empty;
                int j1 = 0;
                j1 = htmlCode.IndexOf(keyword);
                if (j1 > 1)
                {
                    int j2 = htmlCode.IndexOf(@"<td colspan=", j1);
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

    }
}
