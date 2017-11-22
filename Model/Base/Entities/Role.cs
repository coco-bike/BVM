using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Role
    {
        public long Id { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }
        public DateTime BuildTime { get; set; }
        public DateTime UpateTime { get; set; }
        public int Status { get; set; }//0=正常，1=禁用
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<Authority> Authoritys { get; set;}
    }
}
