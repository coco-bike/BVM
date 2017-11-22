using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Authority
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        public string Description { get; set; }
        public DateTime BuildTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public int Status { get; set; }//1=禁用，0=正常
        public virtual ICollection<Role> Roles { get; set; }
    }
}
