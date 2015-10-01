using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NMTSSTransfer
{
    class nmtssVO
    {
        public nmtssVO()
        {
            MeetingName = string.Empty;
            MeetingDate = string.Empty;
            Speaker = string.Empty;
            StartTime = string.Empty;
            EndTime = string.Empty;
            Host = string.Empty;
            Tel = string.Empty;
            Locate1 = string.Empty;
            Locate2 = string.Empty;
            Tel2 = string.Empty;
            Notes = string.Empty;
            EMail = string.Empty;
            STACKHOLDER = string.Empty;

        }

        public string MeetingName { get; set; } //會議名稱
        public string MeetingDate {get;set;} //日期
        public string Speaker { get; set; }  //特邀演講人
        public string StartTime { get; set; } //開始時間
        public string EndTime { get; set; }   //結束時間
        public string Host { get; set; }    //主辦人
        public string STACKHOLDER { get; set; } //主持人
        public string Tel { get; set; }     //電話
        public string Locate1 { get; set; } //會場地點
        public string Locate2 { get; set; } //會場地址
        public string Tel2 { get; set; }   //會場電話
        public string Notes { get; set; }  //意見
        public string EMail { get; set; } //EMail


        
    }
}
