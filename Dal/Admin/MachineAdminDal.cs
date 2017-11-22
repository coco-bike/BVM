using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDal;
using Model;

namespace Dal
{
    public partial class MachineAdminDal:MachineDal,IMachineAdminDal
    {

        ///// <summary>
        ///// 添加机器
        ///// </summary>
        ///// <param name="code"></param>
        ///// <param name="marketTime"></param>
        ///// <param name="maintainTime"></param>
        ///// <param name="Address"></param>
        ///// <returns></returns>
        //public bool AddMachine(string code, DateTime marketTime, DateTime maintainTime, string address)
        //{
        //    Machine machine = this.entity.Set<Machine>().Add(new Machine()
        //    {
        //        Code = code,
        //        Address = address,
        //        MaintainTime = maintainTime,
        //        MarketTime = marketTime
        //    });
        //    if(this.entity.SaveChanges()>0)
        //    {
        //        return true;
        //    }
        //    return false;
        //}

        ///// <summary>
        ///// 更新机器信息
        ///// </summary>
        ///// <param name="id"></param>
        ///// <param name="code"></param>
        ///// <param name="marketTime"></param>
        ///// <param name="maintainTime"></param>
        ///// <param name="address"></param>
        ///// <returns></returns>
        //public bool UpateMachine(long id, string code, DateTime marketTime, DateTime maintainTime, string address)
        //{
        //    var machine = this.entity.Set<Machine>().Where(t => t.ID == id).FirstOrDefault();

        //    machine.Address = address;
        //    machine.Code = code;
        //    machine.MaintainTime = maintainTime;
        //    machine.MarketTime = marketTime;

        //    this.entity.Entry<Machine>(machine).State = System.Data.Entity.EntityState.Modified;

        //    if(this.entity.SaveChanges()>0)
        //    {
        //        return true;
        //    }
        //    return false;
        //}
    }
}
