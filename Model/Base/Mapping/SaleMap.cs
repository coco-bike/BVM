using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Model.Base.Mapping
{
    public class SaleMap : EntityTypeConfiguration<Sale>
    {
        public SaleMap()
        {
            //设置主键
            this.HasKey(t => t.ID);

            //配置ID自动增长
            this.Property(t => t.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            //配置属性
            this.Property(t => t.Change).IsRequired();
            this.Property(t => t.Number).IsRequired();
            this.Property(t => t.Price).IsRequired();
            this.Property(t => t.Time).IsRequired();

            //一对多配置 Machine
            this.HasRequired(s => s.Machine).WithMany(s => s.Sales).HasForeignKey(s => s.MachineID).WillCascadeOnDelete(true);

            //一对多配置 Product
            this.HasRequired(s => s.Product).WithMany(s => s.Sales).HasForeignKey(s => s.ProductID).WillCascadeOnDelete(true);
        }
    }
}
