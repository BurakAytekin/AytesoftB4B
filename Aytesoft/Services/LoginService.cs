using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aytesoft.Models.Domain;
using Aytesoft.Models.Edit;
using Services.Interfaces;
using DataAccess;
using System.Web.Security;
using AutoMapper;
using Aytesoft.Models.View;

namespace Services
{
    public class LoginService : ILoginService
    {
        public bool AuthCheck(User User)
        {
            if (User.UserName != null && User.Password != null)
                return true;
            return false;
        }

        public UserView Authorization(UserEdit User)
        {
            User Authed = DbContext.GetUserWithLogin(User.UserName, User.Password);
            if(AuthCheck(Authed))
            {
                if (StatusCheck(Authed)) { 
                    FormsAuthentication.SetAuthCookie(Authed.ID.ToString(), false);
                    UserView ReturnUser = MapUser(Authed);
                    return ReturnUser;
                }
            }
            return new UserView();
        }

        public UserView MapUser(User user)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<User, UserView>());
            var mapper = new Mapper(config);
            UserView mappeduser = mapper.Map<UserView>(user);
            return mappeduser;
        }

        public bool StatusCheck(User User)
        {
            if (User.Status == 1)
                return true;
            return false;
        }
    }
}
