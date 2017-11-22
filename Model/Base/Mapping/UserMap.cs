using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Base.Mapping
{
    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            //设置主键
            this.HasKey(t => t.ID);

            //配置ID自动增长
            this.Property(t => t.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            //配置属性
            this.Property(t => t.NickName).IsRequired();
            this.Property(t => t.Password).IsRequired();
            this.Property(t => t.EMail).IsRequired();

            //一对多 关系配置 Role
            this.HasRequired(s => s.Role).WithMany(s => s.Users).HasForeignKey(s => s.RoleID).WillCascadeOnDelete(false);
        }
    }
}
