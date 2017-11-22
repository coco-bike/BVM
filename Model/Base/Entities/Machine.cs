using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Machine
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 编号
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 投入使用时间
        /// </summary>
        public DateTime MarketTime { get; set; }
        /// <summary>
        /// 最近一次维护时间
        /// </summary>
        public DateTime MaintainTime { get; set; }
        /// <summary>
        /// 投入使用位置
        /// </summary>
        public string Address { get; set; }
        public int Status { get; set; }//1=禁用，0=正常

        public virtual ICollection<Product> Products { get; set; }

        public virtual ICollection<Sale> Sales { get; set; }
    }
}
