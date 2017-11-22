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
    public class MachineMap : EntityTypeConfiguration<Machine>
    {
        public MachineMap()
        {
            //配置主键
            this.HasKey(t => t.ID);

            //给ID配置自动增长
            this.Property(t => t.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            //配置字段属性
            this.Property(t => t.Code).IsRequired().HasMaxLength(500);
            this.Property(t => t.MaintainTime).IsRequired();
            this.Property(t => t.MarketTime).IsRequired();
            this.Property(t => t.Address).IsRequired().HasMaxLength(500);

            //表名
            this.ToTable("Machines");

        }
    }
}
