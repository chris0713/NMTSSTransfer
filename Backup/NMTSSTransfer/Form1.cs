using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using log4net;
using log4net.Config;


namespace NMTSSTransfer
{
    public partial class Form1 : Form
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Form1));
        CalTransfer ct = new CalTransfer();


        public Form1()
        {
            
            InitializeComponent();
        }

 
        private bool checkInputData()
        {
            if ("".Equals(this.txtUID.Text) || "".Equals(this.txtPwd.Text))
                return false;
            else
                return true;

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string tmp = this.txtURL.Text;
            listLinks.Items.Add(tmp);
            this.txtURL.Text = "";
        }

        private void txtEarse_Click(object sender, EventArgs e)
        {
            listLinks.Items.Remove(listLinks.SelectedItem);

        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            if (checkInputData() == true)
            {
                if (log.IsDebugEnabled) log.Debug("Into GoBtn Click");
                string[] links = new string[this.listLinks.Items.Count];
                for (int j = 0; j < this.listLinks.Items.Count; j++)
                {
                    links[j] = this.listLinks.Items[j].ToString();
                }
                ct.getCVS(this.txtUID.Text, this.txtPwd.Text,links);

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
    }
}
