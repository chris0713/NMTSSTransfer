using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using log4net;
using log4net.Config;

namespace NMTSSTransfer
{
    public static class Tools
    {
        //public static string m_host = ConfigurationManager.AppSettings["SMTP"];
        //public static string m_ToList = ConfigurationManager.AppSettings["ToAddress"];
        //public static string m_OnOffMail = ConfigurationManager.AppSettings["OnOffMail"].Trim();
        //string m_ErrItems = (ConfigurationManager.AppSettings["IgnoreException"] == null) ? "" : ConfigurationManager.AppSettings["IgnoreException"].ToString();
        static readonly ILog log = LogManager.GetLogger(typeof(Tools));
         


        static string m_host = "smtp.gmail.com";
        static string m_from = "chrisgogogo@gmail.com";
        static int m_port = 587;
        static string m_ToList = string.Empty;
        static bool m_IsBodyHtml = true;
        static string m_DCSMailList = string.Empty;
        static string m_MailSubject = "地方研討會自動訂票結果";
             


        /// <summary>
        /// 寄出郵件通知
        /// </summary>
        /// <param name="content"></param>
        /// <param name="TOList">收件人清單split bye ";"</param>
        /// <returns></returns>
        public static bool sendMail(string content, string sendtolist , string mailsubject)
        {
            try
            {
                m_ToList = sendtolist;
                if ("".Equals(m_MailSubject))
                    m_MailSubject = mailsubject;
              
                var smtp = new SmtpClient
                {
                    Host = m_host,
                    Port = m_port,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("chrisgogogo@gmail.com", "A8656008"),
                     EnableSsl= true
                };

                using (var message = new MailMessage(m_from, m_ToList)
                {
                    Subject = m_MailSubject,
                    Body = content,
                    IsBodyHtml = m_IsBodyHtml
                })
                {
                    smtp.Send(message);
                }
                return true;
            }
            catch (Exception ex)
            {
                log.Error("Error at sednMail():" + ex.Message);
                return false;
            }
        }
                   
        
    }
}
