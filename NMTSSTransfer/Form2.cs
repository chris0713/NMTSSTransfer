using System;
using System.Globalization;
using System.Configuration;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using log4net;
using log4net.Config;
using HtmlAgilityPack;

namespace NMTSSTransfer
{
    public partial class Form2 : Form
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Form2));
        static string m_Prefix_LoadURL = ConfigurationManager.AppSettings["URL_Prefix1"].ToString();
        static string m_Prefix_SubmitURL = ConfigurationManager.AppSettings["URL_Prefix2"].ToString();
        static bool m_Switch = false; //是否執行預約登記功能
        static bool m_JobRunning = false; //是否job 執行中。
        static CookieContainer m_Cookie = new CookieContainer();

        public Form2()
        {
            InitializeComponent();
            initFormData();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.Link.LinkData.ToString());
        }
        private void initFormData()
        {
            SetupTargetLinks();
            SetupGridViewMainStyle();
        }


        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.Link.LinkData.ToString());
        }

        private void btnAddGrid_Click(object sender, EventArgs e)
        {
            int n = gvMain.Rows.Add();
            gvMain.Rows[n].Cells[0].Value = txtName.Text.Trim();
            gvMain.Rows[n].Cells[1].Value = txtID4.Text.Trim();
            gvMain.Rows[n].Cells[2].Value = txtDisNum.Text.Trim();
            gvMain.Rows[n].Cells[6].Value = txtEmail.Text.Trim();

        }

        private void SetupGridViewMainStyle()
        {
            gvMain.AutoGenerateColumns = false;
            gvMain.DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomLeft;
            gvMain.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);
            gvMain.Columns.Add("uname", labl01.Text);
            gvMain.Columns.Add("uid", labl02.Text);
            gvMain.Columns.Add("dnumber", labl03.Text);
            gvMain.Columns.Add("status", "狀態");
            gvMain.Columns.Add("qnumber", "查詢序號");
            gvMain.Columns.Add("qpwd", "查詢密碼");
            gvMain.Columns.Add("email", "EMail");
            gvMain.Columns["status"].Width = 20;

        }
        private void SetupTargetLinks()
        { 
            //init URL Target List in ComboList
            List<comboItems> l = new List<comboItems>();
            try
            {
                string strCurrentAP = System.Reflection.Assembly.GetExecutingAssembly().Location;
                using (StreamReader sr = new StreamReader(strCurrentAP + "TargetLinks.txt", Encoding.GetEncoding(950)))
                {
                    string line = string.Empty;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] s = line.Split(',');
                        l.Add(new comboItems(s[0], s[1]));
                    }
                }
            }
            catch (Exception ex1)
            {
                log.Fatal("Read Target URL From TextFile has Error : " + ex1.Message);
                //throw new Exception("Read Target URL From TextFile has Error : "  + ex1.Message);
            }

            cbTarget.DataSource = l;
            cbTarget.DisplayMember = "DisplayText";
            cbTarget.ValueMember = "ValueText";
        }

        private void btnDelGrid_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection c = gvMain.SelectedRows;
            for (int i = 0; i < c.Count; i++)
            {
                gvMain.Rows.Remove(c[i]);
            }
        }

        private Dictionary<string,string> TouchFormAndGetDynData()
        {
            try
            {
                
                string rtn1 = HTTPTool.DownloadContentsUTF8(m_Cookie, m_Prefix_LoadURL + cbTarget.SelectedValue.ToString(), "", 2);

                Dictionary<string, string> dic = new Dictionary<string, string>();
                HtmlAgilityPack.HtmlDocument htmlDoc = new HtmlAgilityPack.HtmlDocument();
                htmlDoc.LoadHtml(rtn1);
                var inputs = htmlDoc.DocumentNode.Descendants("input");
                foreach (var input in inputs)
                {
                    if (input.Attributes["type"].Value == "hidden")
                        dic.Add(input.Attributes["name"].Value, input.Attributes["value"].Value);
                    if(input.Attributes["type"].Value == "checkbox")
                        dic.Add(input.Attributes["name"].Value, input.Attributes["value"].Value);
                }
                return dic;
               
            }
            catch (Exception ex)
            {
                log.Error("error at TouchFormAndGetDynData():" + ex.Message);
                throw new ApplicationException("error at TouchFormAndGetDynData():" + ex.Message);
            }
        }

        private struct OrderResult
        {
            public string UserName;
            public string UID;
            public string DID;
            public string QueryNumber;
            public string QueryPwd;
        }
        private OrderResult ParsOrderSult(string rtnValues)
        {
            try
            {
                OrderResult or = new OrderResult();
                HtmlAgilityPack.HtmlDocument docResult = new HtmlAgilityPack.HtmlDocument();
                docResult.LoadHtml(rtnValues);
                var divs = docResult.DocumentNode.Descendants("div");
                foreach (var div in divs)
                {
                    if (div.InnerHtml.IndexOf("<span style=\"color:blue\">查詢序號：") > 0)
                    {
                        int p1 = div.InnerHtml.IndexOf("查詢序號：</span>");
                        int p11 = div.InnerHtml.IndexOf("<BR>", p1 + 12, StringComparison.OrdinalIgnoreCase); //找到查詢序號後的那個"<br>"字樣

                        //取出查詢序號
                        or.QueryNumber = div.InnerHtml.Substring(p1 + 12, p11 - (p1 + 12));
                        //取出查詢密碼
                        int p2 = div.InnerHtml.IndexOf("查詢密碼：</span>");
                        int p22 = div.InnerHtml.IndexOf("<BR>", p2 + 12, StringComparison.OrdinalIgnoreCase); //找到查詢密碼後的那個"<br>"字樣
                        or.QueryPwd = div.InnerHtml.Substring(p2 + 12, p22 - (p2 + 12));
                        break;
                    }
                }
                return or;
            }
            catch (ApplicationException ex)
            {
                throw new Exception("error at ParsOrderSult()" + ex.Message);
            }
        }

        private void GetTicket()
        {
            foreach (DataGridViewRow r in gvMain.Rows)
            {
                try
                {
                    
                    if (r.Cells["uname"].Value == null || r.Cells["uid"].Value == null || r.Cells["dnumber"].Value==null)
                        continue;
                    string strName = r.Cells["uname"].Value.ToString();
                    string strUid = r.Cells["uid"].Value.ToString();
                    string strDNumber = r.Cells["dnumber"].Value.ToString();
                    string strEmail = r.Cells["email"].Value.ToString();
                    
                    
                    StringBuilder sb = new StringBuilder();
                    sb.Append(txtHName.Text.Trim()).Append("=").Append(strName).Append("&")
                      .Append(txtHID4.Text.Trim()).Append("=").Append(strUid).Append("&")
                      .Append(txtHDisNum.Text.Trim()).Append("=").Append(strDNumber).Append("&");
                    
                    //先載入填寫資料頁，以便取得該次的序號
                    Dictionary<string,string> tags = TouchFormAndGetDynData();
                    foreach(KeyValuePair<string,string> k in tags)
                    {
                        sb.Append(k.Key).Append("=").Append(k.Value).Append("&");
                    }
                    //加入動態資料後，送出訂單並將結果寫入
                    DateTime t1 = DateTime.Now;
                    string v = HTTPTool.DownloadContentsUTF8(m_Cookie, m_Prefix_SubmitURL + cbTarget.SelectedValue.ToString(), sb.ToString(), 1);
                    DateTime t2 = DateTime.Now;
                    double dd = new TimeSpan(t2.Ticks - t1.Ticks).TotalSeconds;
                    
                    if (v.IndexOf("完成報名程序") > 0)
                    {
                        r.Cells["status"].Style.ForeColor = Color.DarkBlue;
                        r.Cells["status"].Value = "S";
                        OrderResult or = ParsOrderSult(v);
                        r.Cells["qnumber"].Value = or.QueryNumber;
                        r.Cells["qpwd"].Value = or.QueryPwd;
                        log.InfoFormat("Finished {0} , Spend Time : {1}", strName, dd.ToString());
                    }
                    else
                    {
                        r.Cells["status"].Style.ForeColor = Color.Red;
                        r.Cells["status"].Value = "F";
                        log.ErrorFormat("Error at Order by {0}, Spend Time : {1} , ErrMsg:{2} ", strName, dd.ToString(), v);
                    }
                    //string strCurrentAP = System.Reflection.Assembly.GetExecutingAssembly().Location;
                    //using (StreamWriter sw = new StreamWriter(strCurrentAP + "Result.txt", false, Encoding.GetEncoding(950)))
                    //{
                    //    sw.WriteLine(sb.ToString());
                    //    sw.WriteLine(rtnValues01);
                    //}
                    //MessageBox.Show(v);

                    Thread.Sleep(900);
                }
                catch (Exception ex)
                {
                    log.Error("Error at button1_Click()" + ex.Message);
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            comboItems selectItemInfo = (comboItems) this.cbTarget.SelectedItem;
            string strInfo = "確認是要訂購：【" + selectItemInfo.DisplayText  + "】此場地的票嗎？";
            if (MessageBox.Show(strInfo, "Confirm delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                GetTicket();
            }
            
        }

        private void Go_Click(object sender, EventArgs e)
        {
            m_Switch = true;
            m_JobRunning = false;
            DateTime dtOrder = DateTime.ParseExact(txtOTime.Text, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
            while (m_Switch == true && m_JobRunning==false)
            {
                Go.Enabled = false;
                button2.Enabled = true;
                if (txtOTime.Text.Equals(DateTime.Now.ToString("yyyy-MM-dd HH:mm")))
                {
                    m_JobRunning = true;
                    GetTicket();
                    m_Switch = false;
                    Go.Enabled = true;
                    button2.Enabled = false;
                    lblRemaining.Text = "";
                    Application.DoEvents();
                    
                }
                else
                {
                    Thread.Sleep(1000);
                    TimeSpan ts = dtOrder.Subtract(DateTime.Now);
                    lblRemaining.Text = string.Format("{0:0.0}", ts.TotalMinutes);
                    Application.DoEvents();
                }
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            m_Switch = false;
            m_JobRunning = false;
            Go.Enabled = true;
            button2.Enabled = false;
            lblRemaining.Text = "";
        }

        private void txtOTime_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnLoadFile_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "CSV File|*.csv";
            openFileDialog1.Title = "選取訂購人員清單";
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                using(StreamReader sr = new StreamReader(openFileDialog1.FileName,Encoding.UTF8))
                {
                    string strLine = string.Empty;
                    while ((strLine = sr.ReadLine()) != null)
                    {
                        string[] items = strLine.Split(',');
                        int n = gvMain.Rows.Add();
                        gvMain.Rows[n].Cells[0].Value = items[0].Trim();
                        gvMain.Rows[n].Cells[1].Value = items[1].Trim();
                        gvMain.Rows[n].Cells[2].Value = items[2].Trim();
                        gvMain.Rows[n].Cells[6].Value = (items[3] == null) ? "" : items[3].Trim();
                    }
                }
            }
            
        }

        private void btnSendMail_Click(object sender, EventArgs e)
        {

            foreach (DataGridViewRow r in gvMain.Rows)
            {
                try
                {
                    StringBuilder sb = new StringBuilder("<html><body><div>");
                    string strName = r.Cells["uname"].Value.ToString();
                    string strUid = r.Cells["uid"].Value.ToString();
                    string strDNumber = r.Cells["dnumber"].Value.ToString();
                    string strEmail = r.Cells["email"].Value.ToString();
                    string strStatus = (r.Cells["status"].Value==null)?"":r.Cells["status"].Value.ToString();
                    //string strCStatus = string.Empty;
                    //if (string.Compare(strStatus, "S", true) == 0)
                    //    strCStatus = "成功";
                    //else
                    //    strCStatus = "失敗";

                    string strQNum = (r.Cells["qnumber"].Value==null)?"": r.Cells["qnumber"].Value.ToString();
                    string strQPwd = (r.Cells["qpwd"].Value == null) ? "" : r.Cells["qpwd"].Value.ToString();
                    string strEMail =  r.Cells["email"].Value.ToString();

                    sb.Append("<table style='margin: 1em; border-collapse: collapse;'>");
                    sb.Append("<tr>");
                    sb.Append("<th style='padding:.3em; border: 1px #ccc solid;'>").Append("<b>").Append("姓名").Append("</b>").Append("</th>");
                    sb.Append("<th style='padding:.3em; border: 1px #ccc solid;'>").Append("<b>").Append("身份證後4碼").Append("</b>").Append("</th>");
                    sb.Append("<th style='padding:.3em; border: 1px #ccc solid;'>").Append("<b>").Append("經銷商後6碼").Append("</b>").Append("</th>");
                    sb.Append("<th style='padding:.3em; border: 1px #ccc solid;'>").Append("<b>").Append("訂票狀態").Append("</b>").Append("</th>");
                    sb.Append("<th style='padding:.3em; border: 1px #ccc solid;'>").Append("<b>").Append("查詢ID").Append("</b>").Append("</th>");
                    sb.Append("<th style='padding:.3em; border: 1px #ccc solid;'>").Append("<b>").Append("查詢密碼").Append("</b>").Append("</th>");
                    sb.Append("</tr><tr>");
                    sb.Append("<td style='padding:.3em; border: 1px #ccc solid;'>").Append("<b>").Append(strName).Append("</b>").Append("</td>");
                    sb.Append("<td style='padding:.3em; border: 1px #ccc solid;'>").Append("<b>").Append(strUid).Append("</b>").Append("</td>");
                    sb.Append("<td style='padding:.3em; border: 1px #ccc solid;'>").Append("<b>").Append(strDNumber).Append("</b>").Append("</td>");
                    if (string.Compare(strStatus, "S", true) == 0)
                        sb.Append("<td style='padding:.3em; border: 1px #ccc solid;'>").Append("<b style='color:#0000FF'> 成功 ^_^").Append("</b>").Append("</td>");
                    else
                        sb.Append("<td style='padding:.3em; border: 1px #ccc solid;'>").Append("<b style='color:#FF0000'> 失敗 -_-||").Append("</b>").Append("</td>");
                    
                    sb.Append("<td style='padding:.3em; border: 1px #ccc solid;'>").Append("<b>").Append(strQNum).Append("</b>").Append("</td>");
                    sb.Append("<td style='padding:.3em; border: 1px #ccc solid;'>").Append("<b>").Append(strQPwd).Append("</b>").Append("</td>");
                    sb.Append("</tr></table>");
                    sb.Append("</br></br>");
                    sb.Append(txtContent2.Text);
                    sb.Append("</div></body></html>");
                    Tools.sendMail(sb.ToString(), strEmail, @"地方研討會訂票結果通知");

                }
                catch (Exception ex)
                {
                    log.Error("Error at button1_Click()" + ex.Message);
                }
            }
            

        }


    }
}

