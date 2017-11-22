using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Model
{
    public class Product
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 产品价格
        /// </summary>
        public double Price { get; set; }
        /// <summary>
        /// 产品状态//0 正常 1 售罄
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 产品类别1碳酸饮料，2果蔬汁饮料，3乳品饮料，4功能饮料，5茶类饮料，6咖啡饮料，7包装饮用水饮料
        /// </summary>
        public int Category { get; set; }
        /// <summary>
        /// 上市时间
        /// </summary>
        public DateTime MarketTime { get; set; }
        /// <summary>
        /// 生产日期
        /// </summary>
        public DateTime ProduceTime { get; set; }
        /// <summary>
        /// 保质期
        /// </summary>
        public int ProtectTime { get; set; }
        /// <summary>
        /// 库存数量
        /// </summary>
        public int Number { get; set; }
        /// <summary>
        /// 图片地址
        /// </summary>
        public string ImagePath { get; set; }
        //virtual延时加载
        public virtual ICollection<Machine> Machines { get; set; }

        public virtual ICollection<Sale> Sales { get; set; }
    }
}

