using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Base.Mapping
{
    public class RoleMap : EntityTypeConfiguration<Role>
    {
        public RoleMap()
        {
            //设置主键
            this.HasKey(t => t.Id);

            //配置ID自动增长
            this.Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            //配置属性
            this.Property(t => t.RoleName).IsRequired();
            this.Property(t => t.BuildTime).IsRequired();
            this.Property(t => t.Description).IsRequired();
            this.Property(t => t.Status).IsRequired();

            
        }
    }
}
