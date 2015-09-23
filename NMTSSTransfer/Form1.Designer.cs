namespace NMTSSTransfer
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改這個方法的內容。
        ///
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.txtUID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPwd = new System.Windows.Forms.TextBox();
            this.btnGo = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.sl = new System.Windows.Forms.CheckBox();
            this.ls = new System.Windows.Forms.CheckBox();
            this.dr = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.nd = new System.Windows.Forms.CheckBox();
            this.tls2 = new System.Windows.Forms.CheckBox();
            this.to = new System.Windows.Forms.CheckBox();
            this.tls1 = new System.Windows.Forms.CheckBox();
            this.tsm = new System.Windows.Forms.CheckBox();
            this.GPT = new System.Windows.Forms.CheckBox();
            this.ECCT = new System.Windows.Forms.CheckBox();
            this.B5 = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.AAE = new System.Windows.Forms.CheckBox();
            this.GTL = new System.Windows.Forms.CheckBox();
            this.D2 = new System.Windows.Forms.CheckBox();
            this.D1 = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.checkBox12 = new System.Windows.Forms.CheckBox();
            this.WCT = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.fYear = new System.Windows.Forms.ComboBox();
            this.fMonth = new System.Windows.Forms.ComboBox();
            this.fDay = new System.Windows.Forms.ComboBox();
            this.tDay = new System.Windows.Forms.ComboBox();
            this.tMonth = new System.Windows.Forms.ComboBox();
            this.tYear = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.regionCode = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtSaveLoc = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtUID
            // 
            this.txtUID.Location = new System.Drawing.Point(49, 7);
            this.txtUID.Name = "txtUID";
            this.txtUID.Size = new System.Drawing.Size(98, 22);
            this.txtUID.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "UserID";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(163, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "Password";
            // 
            // txtPwd
            // 
            this.txtPwd.Location = new System.Drawing.Point(217, 7);
            this.txtPwd.Name = "txtPwd";
            this.txtPwd.PasswordChar = '*';
            this.txtPwd.Size = new System.Drawing.Size(98, 22);
            this.txtPwd.TabIndex = 3;
            // 
            // btnGo
            // 
            this.btnGo.Location = new System.Drawing.Point(361, 294);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(124, 71);
            this.btnGo.TabIndex = 6;
            this.btnGo.Text = "Get outlook CVS File from NMTSS";
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.sl);
            this.groupBox1.Controls.Add(this.ls);
            this.groupBox1.Controls.Add(this.dr);
            this.groupBox1.Location = new System.Drawing.Point(-1, 38);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(142, 212);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "研討會";
            // 
            // sl
            // 
            this.sl.AutoSize = true;
            this.sl.Checked = true;
            this.sl.CheckState = System.Windows.Forms.CheckState.Checked;
            this.sl.Location = new System.Drawing.Point(11, 65);
            this.sl.Name = "sl";
            this.sl.Size = new System.Drawing.Size(120, 16);
            this.sl.TabIndex = 2;
            this.sl.Text = "超連鎖事業說明會";
            this.sl.UseVisualStyleBackColor = true;
            // 
            // ls
            // 
            this.ls.AutoSize = true;
            this.ls.Checked = true;
            this.ls.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ls.Location = new System.Drawing.Point(11, 43);
            this.ls.Name = "ls";
            this.ls.Size = new System.Drawing.Size(84, 16);
            this.ls.TabIndex = 1;
            this.ls.Text = "地方研討會";
            this.ls.UseVisualStyleBackColor = true;
            // 
            // dr
            // 
            this.dr.AutoSize = true;
            this.dr.Location = new System.Drawing.Point(11, 21);
            this.dr.Name = "dr";
            this.dr.Size = new System.Drawing.Size(72, 16);
            this.dr.TabIndex = 0;
            this.dr.Text = "區域大會";
            this.dr.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.nd);
            this.groupBox2.Controls.Add(this.tls2);
            this.groupBox2.Controls.Add(this.to);
            this.groupBox2.Controls.Add(this.tls1);
            this.groupBox2.Controls.Add(this.tsm);
            this.groupBox2.Controls.Add(this.GPT);
            this.groupBox2.Controls.Add(this.ECCT);
            this.groupBox2.Controls.Add(this.B5);
            this.groupBox2.Location = new System.Drawing.Point(147, 38);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(142, 212);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "經銷商訓練";
            // 
            // nd
            // 
            this.nd.AutoSize = true;
            this.nd.Location = new System.Drawing.Point(12, 84);
            this.nd.Name = "nd";
            this.nd.Size = new System.Drawing.Size(96, 16);
            this.nd.TabIndex = 7;
            this.nd.Text = "新經銷商訓練";
            this.nd.UseVisualStyleBackColor = true;
            // 
            // tls2
            // 
            this.tls2.AutoSize = true;
            this.tls2.Location = new System.Drawing.Point(12, 172);
            this.tls2.Name = "tls2";
            this.tls2.Size = new System.Drawing.Size(120, 16);
            this.tls2.TabIndex = 6;
            this.tls2.Text = "全新生活進階應用";
            this.tls2.UseVisualStyleBackColor = true;
            // 
            // to
            // 
            this.to.AutoSize = true;
            this.to.Location = new System.Drawing.Point(11, 106);
            this.to.Name = "to";
            this.to.Size = new System.Drawing.Size(120, 16);
            this.to.TabIndex = 5;
            this.to.Text = "全新生活入門概論";
            this.to.UseVisualStyleBackColor = true;
            // 
            // tls1
            // 
            this.tls1.AutoSize = true;
            this.tls1.Location = new System.Drawing.Point(12, 150);
            this.tls1.Name = "tls1";
            this.tls1.Size = new System.Drawing.Size(120, 16);
            this.tls1.TabIndex = 4;
            this.tls1.Text = "全新生活基礎起步";
            this.tls1.UseVisualStyleBackColor = true;
            // 
            // tsm
            // 
            this.tsm.AutoSize = true;
            this.tsm.Location = new System.Drawing.Point(11, 128);
            this.tsm.Name = "tsm";
            this.tsm.Size = new System.Drawing.Size(120, 16);
            this.tsm.TabIndex = 3;
            this.tsm.Text = "全新生活支援課程";
            this.tsm.UseVisualStyleBackColor = true;
            // 
            // GPT
            // 
            this.GPT.AutoSize = true;
            this.GPT.Location = new System.Drawing.Point(11, 62);
            this.GPT.Name = "GPT";
            this.GPT.Size = new System.Drawing.Size(72, 16);
            this.GPT.TabIndex = 2;
            this.GPT.Text = "產品訓練";
            this.GPT.UseVisualStyleBackColor = true;
            // 
            // ECCT
            // 
            this.ECCT.AutoSize = true;
            this.ECCT.Location = new System.Drawing.Point(11, 40);
            this.ECCT.Name = "ECCT";
            this.ECCT.Size = new System.Drawing.Size(108, 16);
            this.ECCT.TabIndex = 1;
            this.ECCT.Text = "授證經理級訓練";
            this.ECCT.UseVisualStyleBackColor = true;
            // 
            // B5
            // 
            this.B5.AutoSize = true;
            this.B5.Checked = true;
            this.B5.CheckState = System.Windows.Forms.CheckState.Checked;
            this.B5.Location = new System.Drawing.Point(11, 21);
            this.B5.Name = "B5";
            this.B5.Size = new System.Drawing.Size(84, 16);
            this.B5.TabIndex = 0;
            this.B5.Text = "成功五要訣";
            this.B5.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.AAE);
            this.groupBox3.Controls.Add(this.GTL);
            this.groupBox3.Controls.Add(this.D2);
            this.groupBox3.Controls.Add(this.D1);
            this.groupBox3.Location = new System.Drawing.Point(295, 38);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(194, 144);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "莫蒂膚訓練";
            // 
            // AAE
            // 
            this.AAE.AutoSize = true;
            this.AAE.Location = new System.Drawing.Point(12, 84);
            this.AAE.Name = "AAE";
            this.AAE.Size = new System.Drawing.Size(168, 16);
            this.AAE.TabIndex = 7;
            this.AAE.Text = "莫蒂膚彩妝訓練之眼妝教學";
            this.AAE.UseVisualStyleBackColor = true;
            // 
            // GTL
            // 
            this.GTL.AutoSize = true;
            this.GTL.Location = new System.Drawing.Point(11, 62);
            this.GTL.Name = "GTL";
            this.GTL.Size = new System.Drawing.Size(168, 16);
            this.GTL.TabIndex = 2;
            this.GTL.Text = "莫蒂膚彩妝訓練之基礎美妝";
            this.GTL.UseVisualStyleBackColor = true;
            // 
            // D2
            // 
            this.D2.AutoSize = true;
            this.D2.Location = new System.Drawing.Point(11, 40);
            this.D2.Name = "D2";
            this.D2.Size = new System.Drawing.Size(180, 16);
            this.D2.TabIndex = 1;
            this.D2.Text = "莫蒂膚第二級彩妝與技巧訓練";
            this.D2.UseVisualStyleBackColor = true;
            // 
            // D1
            // 
            this.D1.AutoSize = true;
            this.D1.Location = new System.Drawing.Point(11, 21);
            this.D1.Name = "D1";
            this.D1.Size = new System.Drawing.Size(180, 16);
            this.D1.TabIndex = 0;
            this.D1.Text = "莫蒂膚第一級化妝及護膚訓練";
            this.D1.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.checkBox12);
            this.groupBox4.Controls.Add(this.WCT);
            this.groupBox4.Location = new System.Drawing.Point(295, 188);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(194, 62);
            this.groupBox4.TabIndex = 10;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "網路訓練";
            // 
            // checkBox12
            // 
            this.checkBox12.AutoSize = true;
            this.checkBox12.Location = new System.Drawing.Point(12, 84);
            this.checkBox12.Name = "checkBox12";
            this.checkBox12.Size = new System.Drawing.Size(156, 16);
            this.checkBox12.TabIndex = 7;
            this.checkBox12.Text = "莫蒂膚彩妝訓之眼妝教學";
            this.checkBox12.UseVisualStyleBackColor = true;
            // 
            // WCT
            // 
            this.WCT.AutoSize = true;
            this.WCT.Location = new System.Drawing.Point(11, 21);
            this.WCT.Name = "WCT";
            this.WCT.Size = new System.Drawing.Size(96, 16);
            this.WCT.TabIndex = 0;
            this.WCT.Text = "網路中心訓練";
            this.WCT.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(-1, 256);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 11;
            this.label3.Text = "語言：國語";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(-1, 285);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 13;
            this.label5.Text = "開始日期";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(-1, 308);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 14;
            this.label6.Text = "結束日期";
            // 
            // fYear
            // 
            this.fYear.FormattingEnabled = true;
            this.fYear.Items.AddRange(new object[] {
            "2012",
            "2013",
            "2014",
            "2015",
            "2016",
            "2017",
            "2018",
            "2019",
            "2020"});
            this.fYear.Location = new System.Drawing.Point(58, 282);
            this.fYear.Name = "fYear";
            this.fYear.Size = new System.Drawing.Size(92, 20);
            this.fYear.TabIndex = 15;
            // 
            // fMonth
            // 
            this.fMonth.FormattingEnabled = true;
            this.fMonth.Items.AddRange(new object[] {
            "01",
            "02",
            "03",
            "04",
            "05",
            "06",
            "07",
            "08",
            "09",
            "10",
            "11",
            "12"});
            this.fMonth.Location = new System.Drawing.Point(156, 282);
            this.fMonth.Name = "fMonth";
            this.fMonth.Size = new System.Drawing.Size(59, 20);
            this.fMonth.TabIndex = 16;
            // 
            // fDay
            // 
            this.fDay.FormattingEnabled = true;
            this.fDay.Items.AddRange(new object[] {
            "01",
            "02",
            "03",
            "04",
            "05",
            "06",
            "07",
            "08",
            "09",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30",
            "31"});
            this.fDay.Location = new System.Drawing.Point(221, 282);
            this.fDay.Name = "fDay";
            this.fDay.Size = new System.Drawing.Size(59, 20);
            this.fDay.TabIndex = 17;
            // 
            // tDay
            // 
            this.tDay.FormattingEnabled = true;
            this.tDay.Items.AddRange(new object[] {
            "01",
            "02",
            "03",
            "04",
            "05",
            "06",
            "07",
            "08",
            "09",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30",
            "31"});
            this.tDay.Location = new System.Drawing.Point(220, 308);
            this.tDay.Name = "tDay";
            this.tDay.Size = new System.Drawing.Size(59, 20);
            this.tDay.TabIndex = 20;
            // 
            // tMonth
            // 
            this.tMonth.FormattingEnabled = true;
            this.tMonth.Items.AddRange(new object[] {
            "01",
            "02",
            "03",
            "04",
            "05",
            "06",
            "07",
            "08",
            "09",
            "10",
            "11",
            "12"});
            this.tMonth.Location = new System.Drawing.Point(155, 308);
            this.tMonth.Name = "tMonth";
            this.tMonth.Size = new System.Drawing.Size(59, 20);
            this.tMonth.TabIndex = 19;
            // 
            // tYear
            // 
            this.tYear.FormattingEnabled = true;
            this.tYear.Items.AddRange(new object[] {
            "2012",
            "2013",
            "2014",
            "2015",
            "2016",
            "2017",
            "2018",
            "2019",
            "2020"});
            this.tYear.Location = new System.Drawing.Point(57, 308);
            this.tYear.Name = "tYear";
            this.tYear.Size = new System.Drawing.Size(92, 20);
            this.tYear.TabIndex = 18;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(81, 254);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 12;
            this.label4.Text = "轄區：";
            // 
            // regionCode
            // 
            this.regionCode.FormattingEnabled = true;
            this.regionCode.Items.AddRange(new object[] {
            "N",
            "C",
            "S"});
            this.regionCode.Location = new System.Drawing.Point(128, 251);
            this.regionCode.Name = "regionCode";
            this.regionCode.Size = new System.Drawing.Size(59, 20);
            this.regionCode.TabIndex = 21;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(190, 256);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(114, 12);
            this.label7.TabIndex = 22;
            this.label7.Text = "N:北區,C:中區,S:南區";
            // 
            // txtSaveLoc
            // 
            this.txtSaveLoc.Location = new System.Drawing.Point(77, 343);
            this.txtSaveLoc.Name = "txtSaveLoc";
            this.txtSaveLoc.Size = new System.Drawing.Size(203, 22);
            this.txtSaveLoc.TabIndex = 23;
            this.txtSaveLoc.Text = "c:\\Calendar.csv";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(-2, 347);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(80, 12);
            this.label8.TabIndex = 24;
            this.label8.Text = "輸出csv位置：";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripProgressBar1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 376);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(492, 22);
            this.statusStrip1.TabIndex = 25;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(104, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Maximum = 180;
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 16);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(492, 398);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtSaveLoc);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.regionCode);
            this.Controls.Add(this.tDay);
            this.Controls.Add(this.tMonth);
            this.Controls.Add(this.tYear);
            this.Controls.Add(this.fDay);
            this.Controls.Add(this.fMonth);
            this.Controls.Add(this.fYear);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnGo);
            this.Controls.Add(this.txtPwd);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtUID);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "NMTSS Search";
            this.TransparencyKey = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtUID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPwd;
        private System.Windows.Forms.Button btnGo;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox sl;
        private System.Windows.Forms.CheckBox ls;
        private System.Windows.Forms.CheckBox dr;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox nd;
        private System.Windows.Forms.CheckBox tls2;
        private System.Windows.Forms.CheckBox to;
        private System.Windows.Forms.CheckBox tls1;
        private System.Windows.Forms.CheckBox tsm;
        private System.Windows.Forms.CheckBox GPT;
        private System.Windows.Forms.CheckBox ECCT;
        private System.Windows.Forms.CheckBox B5;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox AAE;
        private System.Windows.Forms.CheckBox GTL;
        private System.Windows.Forms.CheckBox D2;
        private System.Windows.Forms.CheckBox D1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox checkBox12;
        private System.Windows.Forms.CheckBox WCT;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox fYear;
        private System.Windows.Forms.ComboBox fMonth;
        private System.Windows.Forms.ComboBox fDay;
        private System.Windows.Forms.ComboBox tDay;
        private System.Windows.Forms.ComboBox tMonth;
        private System.Windows.Forms.ComboBox tYear;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox regionCode;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtSaveLoc;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        
    }
}

