using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCafe
{
    public class UserDetails
    {
        private static int s_userID = 1000;
        public string UserName { get; set; }
        public string FatherName { get; set; }
        public string WorkStationNumber { get; set; }
        public GenderDetails Gender { get; set; }
        public string MobilNumber { get; set; }
        public string MailID { get; set; }
        private int _balance { get; set; }
        public int Balance { get { return _balance; } }
    }
}