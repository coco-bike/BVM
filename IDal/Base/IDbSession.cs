using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDal
{
    public interface IDbSession
    {
        DbContext entity { get; }
        IProductAdminDal ProductDal { get; set; }
        ISaleAdminDal SaleDal { get; set; }
        IMachineAdminDal MachineDal { get; set; }
        IUserAdminDal UserDal { get; set; }
        IRoleAdminDal RoleDal { get; set; }
        IAuthorityAdminDal AuthorityDal { get; set; }
        int ExcuteSql(string sql, object[] parameters);
        bool SaveChanges();
    }
}
