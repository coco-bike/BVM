﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IService
{
    public partial interface IUserAdminService:IUserService
    {
        bool AddMachine(int uid, int pid);

    }
}
