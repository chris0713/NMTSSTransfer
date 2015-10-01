using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using log4net;
using log4net.Config;


namespace NMTSSTransfer
{
    public partial class Form1 : Form
    {

        private static readonly ILog log = LogManager.GetLogger(typeof(Form1));
        CalTransfer ct = new CalTransfer();
        private int numberToCompute = 0;
        private int _max = 100;
        private int highestPercentageReached = 0;
        List<string> l_Items;
        
       


        public Form1()
        {
            
            InitializeComponent();           
            l_dtStart.Text = DateTime.Now.ToString("yyyy/MM/dd");
            l_dtEnd.Text = DateTime.Now.AddMonths(1).ToString("yyyy/MM/dd");
            InitialBackgroundWorker();
            setDefaultValues();
            log4net.Config.XmlConfigurator.Configure();

        }
        private void InitialBackgroundWorker()
        {
            backgroundWorker1.DoWork +=new DoWorkEventHandler(backgroundWorker1_DoWork);
            backgroundWorker1.RunWorkerCompleted+=new RunWorkerCompletedEventHandler(backgroundWorker1_RunWorkerCompleted);
            backgroundWorker1.ProgressChanged+=new ProgressChangedEventHandler(backgroundWorker1_Progresschanged);
        }

        private void backgroundWorker1_DoWork(object sender,
            DoWorkEventArgs e)
        {
            numberToCompute = 0;
            backgroundWorker1.WorkerReportsProgress = true;
            BackgroundWorker worker = sender as BackgroundWorker;
            DoAdd(worker, e);
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
                log.Error(e.Error.Message);
            else if (e.Cancelled)
            {
                toolStripStatusLabel1.Text = "Canceled";
                btnGo.Enabled = true;
            }
            else
            {
                if (InvokeRequired)
                {
                    BeginInvoke(new Action(() => toolStripStatusLabel1.Text = "作業已完成!!"));
                    BeginInvoke(new Action(() => toolStripStatusLabel1.ForeColor = Color.DarkBlue));
                }
                else
                {
                    toolStripStatusLabel1.Text = "作業已完成！";
                    toolStripStatusLabel1.ForeColor = Color.DarkBlue;
                }

            }
        }

        private void backgroundWorker1_Progresschanged(object sender, ProgressChangedEventArgs e)
        {
            this.toolStripProgressBar1.Value = e.ProgressPercentage;
        }

        private void  DoAdd(BackgroundWorker worker, DoWorkEventArgs e)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => toolStripProgressBar1.Maximum = _max));
            }
            
            int iComplete = (int)e.Argument;
            while (iComplete < _max)
            {
                if (worker.CancellationPending == true)
                {
                    e.Cancel = true;
                    break;
                }
                worker.ReportProgress(iComplete);
                iComplete = numberToCompute;
                System.Threading.Thread.Sleep(10);
            }
            if (iComplete >= _max)
                worker.ReportProgress(_max);

        }

        private void setDefaultValues()
        {
            try{
                this.toolStripStatusLabel1.ForeColor = Color.DarkBlue;
                toolStripStatusLabel1.Text = "";
                this.regionCode.SelectedIndex = 0;
                //Year
                DateTime dt = DateTime.Now;
                DateTime dt2 = dt.AddMonths(1);
                
                for (int i = 0; i < fYear.Items.Count;  i++)
                {
                    int year = Convert.ToInt32(fYear.Items[i].ToString());
                    if (dt.Year == year )
                        fYear.SelectedIndex = i;
                    
                    if(dt2.Year == year)
                        tYear.SelectedIndex = i;
                    
                }

                //Start Month
                for (int i = 0; i < fMonth.Items.Count; i++)
                {
                    int m = Convert.ToInt32(fMonth.Items[i].ToString());
                    if (dt.Month == m)
                        fMonth.SelectedIndex = i;
                }
                //End Month
                for (int i = 0; i < tMonth.Items.Count; i++)
                {
                    int m = Convert.ToInt32(tMonth.Items[i].ToString());
                    if (dt2.Month == m)
                        tMonth.SelectedIndex = i;
                }

                //Start Day
                for (int i = 0; i < fDay.Items.Count; i++)
                {
                    int d = Convert.ToInt32(fDay.Items[i].ToString());
                    if (dt.Day == d)
                        fDay.SelectedIndex = i;
                }
                //End Day
                for (int i = 0; i < tDay.Items.Count; i++)
                {
                    int d = Convert.ToInt32(tDay.Items[i].ToString());
                    if (dt2.Day == d)
                        tDay.SelectedIndex = i;
                }

                //載入ID/PWD 做為Default 值
                string strPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
                using (StreamReader sr = new StreamReader(strPath+"IDInfo.txt"))
                {
                    try
                    {
                        string l = sr.ReadLine();
                        string[] aryAccountInfo = l.Split(',');
                        this.txtUID.Text = aryAccountInfo[0];
                        this.txtPwd.Text = aryAccountInfo[1];
                    }
                    catch (Exception ex)
                    {
                        log.Error("Error at Load ID/PWD Default :" + ex.Message);
                    }
                }


            }catch(Exception ex)
            {
                log.Error("Error at setDefaultValues():" + ex.Message);
            }

        }

 
        private bool checkInputData()
        {
            if ("".Equals(this.txtUID.Text) || "".Equals(this.txtPwd.Text))
                return false;
            else
                return true;

        }

        private void composeCheckItems()
        {
            l_Items = new List<string>();
            l_Items.Add("minSearchDate=0");
            l_Items.Add("dateMask=yy/mm/dd");
            l_Items.Add("dateFilter=custom");
            if(ls.Checked)
                l_Items.Add("meetingTypeID=5"); //地方研討會
            if (sl.Checked)
                l_Items.Add("meetingTypeID=6"); //超連鎖事業說明會
            if (B5.Checked)
                l_Items.Add("meetingTypeID=7"); //B5
            if(ECCT.Checked)
                l_Items.Add("meetingTypeID=8"); //ECCT
            if(GPT.Checked)
                l_Items.Add("meetingTypeID=9"); //產品訓練
            if (nd.Checked)
                l_Items.Add("meetingTypeID=17"); //新超連鎖店主訓練
            if (to.Checked)
                l_Items.Add("meetingTypeID=52"); //全新生活入門概論
            if (tsm.Checked)
                l_Items.Add("meetingTypeID=19"); //全新生活支援課程
            if (tls1.Checked)
                l_Items.Add("meetingTypeID=21"); //全新生活基礎起步訓練
            if (tls2.Checked)
                l_Items.Add("meetingTypeID=22");//全新生活進階應用訓練
            if (D1.Checked)
                l_Items.Add("meetingTypeID=69");  //莫蒂膚產品知識運用與行銷 
            if (D2.Checked)
                l_Items.Add("meetingTypeID=13"); //莫蒂膚入門概論
            if (GTL.Checked)
                l_Items.Add("meetingTypeID=12"); //莫蒂膚事業說明會
            if (AAE.Checked)
                l_Items.Add("meetingTypeID=80"); //莫蒂膚®實務操作課程-線上派對& Shopbox科技
            if (D3.Checked)
                l_Items.Add("meetingTypeID=70");//肌膚保養產品知識運用與行銷
            if (WCT.Checked)
                l_Items.Add("meetingTypeID=24"); //網路中心入門概論
            //l_Items.Add("fYear="+fYear.Text +"&fMonth="+fMonth.Text +"&fDay="+fDay.Text);
            //l_Items.Add("tYear=" + tYear.Text + "&tMonth=" + tMonth.Text + "&tDay=" + tDay.Text);
            //l_Items.Add("regionCode="+ regionCode.Text);
 
            l_Items.Add("startDate=" +this.l_dtStart.Text ); //Start Date
            l_Items.Add("endDate=" + this.l_dtEnd.Text);    // End Date
            
            l_Items.Add("state=" + regionCode.Text + "*TWN"); //Region
            l_Items.Add("language=CMN"); //Language

        }
        private void btnGo_Click(object sender, EventArgs e)
        {
            if (checkInputData() == true)
            {
                this.toolStripStatusLabel1.ForeColor = Color.DarkBlue;
                this.toolStripStatusLabel1.Text = "";
                composeCheckItems();
                this.startGen();

                //Thread th = new Thread(new ThreadStart(startGen));
                //th.Start();
                
                //Start the asynchronous operation.
                //backgroundWorker1.RunWorkerAsync(numberToCompute);
                log.Info("完成資料下載作業");
                
            }
            else
            {
                MessageBox.Show("Please input your ID and Password");
            }
        }
        public string getCalendarFromNMTSS(string uid , string pwd)
        {
            return "";
        }
        private string OutputMsg(JobResult j)
        {
            StringBuilder sb = new StringBuilder();
            if (j.ERRORURL.Count > 0)
            {
                sb.Append("\r\n").Append("無法下載內容的URL如下：").Append("\r\n");
                foreach (string s in j.ERRORURL)
                {
                    sb.Append(s).Append(";").Append("\r\n");
                }
                sb.Append("----------------------------------------------------------------");
            }
            if (j.ErrMsg.Count > 0)
            {
                sb.Append("\r\n").Append("執行過程發生之錯誤訊息清單如下：").Append("\r\n");
                foreach (string s2 in j.ErrMsg)
                {
                    sb.Append(s2).Append(";").Append("\r\n");
                }
            }
            return sb.ToString();
        }
        private bool startGen()
        {
            this.toolStripStatusLabel1.Text = @"已開始執行資料查詢及下載作業，請稍候...";
            log.Info("已開始執行資料查詢及下載作業，請稍候...");
            this.btnGo.Enabled = false;

            JobResult r = ct.getCVS(this.txtSaveLoc.Text, this.txtUID.Text, this.txtPwd.Text, this.l_Items, ref numberToCompute, ref _max, ref backgroundWorker1);
            bool isOK = r.IsDownloadSuccess;
            if (isOK == false)
            {
                this.toolStripStatusLabel1.ForeColor = Color.Red;
                this.toolStripStatusLabel1.Text = @"過程中產生異常，請查看Log目錄下的log檔.";
                log.Error(OutputMsg(r));
            }
            else
            {
                this.toolStripStatusLabel1.ForeColor = Color.DarkBlue;
                this.toolStripStatusLabel1.Text = @"作業已完成!!";
            }
            this.btnGo.Enabled = true;
            return isOK;
            
          
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (this.backgroundWorker1.IsBusy)
            {
                try
                {
                    backgroundWorker1.WorkerSupportsCancellation = true;
                    if(backgroundWorker1.WorkerSupportsCancellation == true)
                        backgroundWorker1.CancelAsync();
                }
                catch (Exception ex)
                {
                    log.Error(ex.Message);
                }

            }
            
        }

        private void menuForm2_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.Visible = true;
        }

        private void dtp_Start_ValueChanged(object sender, EventArgs e)
        {
            this.l_dtStart.Text = dtp_Start.Value.ToString("yyyy/MM/dd");
        }

        private void dtp_End_ValueChanged(object sender, EventArgs e)
        {
            this.l_dtEnd.Text = dtp_End.Value.ToString("yyyy/MM/dd");
        }
    }
}
