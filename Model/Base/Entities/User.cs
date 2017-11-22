using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class User
    {
        public int ID{get;set;}
        public string NickName { get; set; }
        public string EMail { get; set; }
        public string Password { get; set; }
        public int Status { get; set; }//1=禁用，0=正常
        public long Count { get; set; }
        public DateTime LoginTime { get; set; }
        public DateTime BuildTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public long RoleID { get; set; }
        public virtual Role Role { get; set; }

    }
}
