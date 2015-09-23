using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using log4net;
using log4net.Config;

namespace NMTSSTransfer
{
    static class Program
    {
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        //private static readonly ILog log = LogManager.GetLogger("NMTSSTransfer");
        static void Main()
        {
            //XmlConfigurator.Configure(new System.IO.FileInfo(@".\config.xml")); 
            log4net.Config.XmlConfigurator.Configure();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
            //Application.Run(new Form2());
        }
    }
}
