using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Sale
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 单次交易付款价格
        /// </summary>
        public double Price { get; set; }
        /// <summary>
        /// 找零价格
        /// </summary>
        public double Change { get; set; }
        /// <summary>
        /// 交易时间
        /// </summary>
        public DateTime Time { get; set; }
        /// <summary>
        /// 交易数量
        /// </summary>
        public int Number { get; set; }
        /// <summary>
        /// 产品的ID
        /// </summary>
        public int ProductID { get; set; }
        /// <summary>
        /// 机器的ID
        /// </summary>
        public int MachineID { get; set; }
        public virtual Product Product { get; set; }
        public virtual Machine Machine { get; set; }
    }
}
