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
    public class ProductMap : EntityTypeConfiguration<Product>
    {
        public ProductMap()
        {
            //主键
            this.HasKey(t => t.ID);

            //设置自动增长
            this.Property(t => t.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            //配置字段属性
            this.Property(t => t.Name).IsRequired().HasMaxLength(500);
            this.Property(t => t.Category).IsRequired();
            this.Property(t => t.ImagePath).IsRequired().HasMaxLength(500);
            this.Property(t => t.MarketTime).IsRequired();
            this.Property(t => t.Number).IsRequired();
            this.Property(t => t.Price).IsRequired();
            this.Property(t => t.ProduceTime).IsRequired();
            this.Property(t => t.ProtectTime).IsRequired();
            this.Property(t => t.Status).IsRequired();

            //表名
            this.ToTable("Products");

            //多对多
            this.HasMany(s => s.Machines).WithMany(s => s.Products).Map(s => s.ToTable("ProductMachine").MapLeftKey("ProductID").MapRightKey("MachineID"));
        }
    }
}
