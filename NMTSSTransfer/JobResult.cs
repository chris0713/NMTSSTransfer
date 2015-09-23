using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NMTSSTransfer
{
    public class JobResult
    {
        public JobResult()
        {
            ERRORURL = new List<string>();
            ErrMsg = new List<string>();
        }
        public List<string> ERRORURL { get; set; } //無法順利下載的資料links 清單
        public bool IsDownloadSuccess { get; set; } //是否全下載成功
        public List<string> ErrMsg { get; set; } // 錯誤訊息清單

    }
}
