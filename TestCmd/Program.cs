using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HtmlAgilityPack;


namespace TestCmd
{
    class Program
    {
        static void Main(string[] args)
        {

            Program t = new Program();
            string htmlDoc = t.loadTESTData(@"c:\user\test1.txt");
            Console.WriteLine("This is :中文");
            //t.getWords(htmlDoc);
            string s = "[臺北科技大學 綜合科(10:00)";
            string s1 = Regex.Replace(s, @"[\u0000-\u007F]", string.Empty);


        }


        private void getWords(string strHTML)
        {
            string strDate = string.Empty; //日期
            string strSpeaker = string.Empty;//演講人
            string strStartTime = string.Empty;//開始時間
            string strEndTime = string.Empty;//結束時間
            string strStackholder = string.Empty;//主持人
            string strPhoneNum = string.Empty;//電話
            string strAddress = string.Empty;//會場地址
            string strComments = string.Empty;//意見
            string strEMail = string.Empty;//Email 
            HtmlDocument parser = new HtmlDocument();
            parser.LoadHtml(strHTML);
            var nodes = parser.DocumentNode.Descendants("div");
            List<string> l = new List<string>();

            var nodes1 = parser.DocumentNode.SelectSingleNode("//div[@class=\"column\"]");

            //主辦者
            strStackholder = nodes1.SelectSingleNode("./div[contains(label,'主辦者')]/text()").InnerText;

            //電子郵件:
            strEMail = nodes1.SelectSingleNode("./div[contains(label,'電子郵件')]/text()").InnerText;
            //主持人電話:
            strPhoneNum = nodes1.SelectSingleNode("./div[contains(label,'主持人電話')]/text()").InnerText;
            //演講嘉賓:
            strSpeaker = nodes1.SelectSingleNode("./div[contains(label,'演講嘉賓')]/text()").InnerText;
            //其他意見:
            strComments = nodes1.SelectSingleNode("./div[contains(label,'其他意見')]/text()").InnerText;
            //日期
            strDate = nodes1.SelectSingleNode("//div[@class=\"eventDate\"]/text()").InnerText;
            //Get Time Period ~
            string strTimePeriod = nodes1.SelectSingleNode("//div[@class=\"eventDate\"]/following-sibling::div[1]/text()").InnerText;
            strStartTime = strTimePeriod.Split('-')[0].Trim();
            strEndTime = strTimePeriod.Split('-')[1].Trim();

            //會場地址
            strAddress = nodes1.SelectSingleNode("//div[@class=\"eventDate\"]/following-sibling::div[3]/text()").InnerText.Substring(0, 3)
                  + " " + nodes1.SelectSingleNode("//div[@class=\"eventDate\"]/following-sibling::div[2]/text()").InnerText;

            //其他意見
            string strMemo = nodes1.SelectSingleNode("./div[contains(label,'其他意見')]/text()").InnerText;

            Console.WriteLine(strStackholder);
            Console.WriteLine(strEMail);
            Console.WriteLine(strPhoneNum);
            Console.WriteLine(strSpeaker);
            Console.WriteLine(strComments);


            //foreach (HtmlNode d in parser.DocumentNode.SelectNodes("//label[@class=\"wrapped\"]"))
            //{
            //    Console.Write(d.InnerText +":");
            //    Console.WriteLine(d.SelectSingleNode("../following-sibling::div[1]/following-sibling::text()").InnerText);
            //}


        }

        private string loadTESTData(string strPath)
        {
            try
            {   // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader(strPath,Encoding.UTF8))
                {
                    // Read the stream to a string, and write the string to the console.
                    String line = sr.ReadToEnd();
                    return line;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
                return string.Empty;
                   
            }

        }
    }
}
