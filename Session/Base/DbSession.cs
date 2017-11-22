using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal;
using IDal;

namespace Session
{
    public class DbSession:IDbSession
    {
        /// <summary>
        /// 创建线程内唯一上下文对象
        /// </summary>
        public DbContext entity
        {
            get
            {
                return DbContextOnly.CreateEF();
            }
        }
        private IProductAdminDal _ProductDal;
        public IProductAdminDal ProductDal
        {
            get
            {
                if(_ProductDal==null)
                {
                    _ProductDal = DalFactory.CreateProductDal();
                }
                return _ProductDal;
            }
            set { _ProductDal = value; }
        }

        private IMachineAdminDal _MachineDal;
        public IMachineAdminDal MachineDal
        {
            get
            {
                if(_MachineDal==null)
                {
                    _MachineDal = DalFactory.CreateMachineDal();
                }
                return _MachineDal;
            }
            set { _MachineDal = value; }
        }

        public ISaleAdminDal _SaleDal;
        public ISaleAdminDal SaleDal
        {
            get
            {
                if(_SaleDal==null)
                {
                    _SaleDal = DalFactory.CreateSaleDal();
                }
                return _SaleDal;
            }
            set { _SaleDal = value; }
        }

        public IUserAdminDal _UserDal;
        public IUserAdminDal UserDal
        {
            get
            {
                if(_UserDal==null)
                {
                    _UserDal = DalFactory.CreateUserDal();
                }
                return _UserDal;
            }
            set { _UserDal = value; }
        }

        public IRoleAdminDal _RoleDal;
        public IRoleAdminDal RoleDal
        {
            get
            {
                if(_RoleDal==null)
                {
                    _RoleDal = DalFactory.CreateRoleDal();
                }
                return _RoleDal;
            }
            set { _RoleDal = value; }
        }

        public IAuthorityAdminDal _AuthorityDal;
        public IAuthorityAdminDal AuthorityDal
        {
            get
            {
                if(_AuthorityDal==null)
                {
                    _AuthorityDal = DalFactory.CreateAuthortiyDal();
                }
                return _AuthorityDal;
            }
            set { _AuthorityDal = value; }
        }
        /// <summary>
        /// 执行Sql
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public int ExcuteSql(string sql, object[] parameters)
        {
            return this.entity.Database.ExecuteSqlCommand(sql, parameters);
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        public bool SaveChanges()
        {
            try
            {
                if (this.entity.SaveChanges() > 0)
                {
                    return true;
                }
            }
            catch(Exception ex){
                throw;
            }

              return false;
        }

    }
}
