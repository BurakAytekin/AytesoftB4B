﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aytesoft.Models.Domain;
using Aytesoft.Models.Edit;
using Aytesoft.Models.View;

namespace Services.Interfaces
{
    public interface ILoginService
    {
        UserView Authorization(UserEdit User);
        bool AuthCheck(User User);
        bool StatusCheck(User User);
        UserView MapUser(User user);
    }
}