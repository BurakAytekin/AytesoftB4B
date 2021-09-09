using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Domain;
using Aytesoft.Models.View;
using Aytesoft.Models.Edit;
using Services.Interfaces;
using DataAccess;
using System.Web.Security;
using AutoMapper;

namespace Services
{
    public class LoginService : ILoginService
    {
        public bool AuthCheck(User user)
        {
            if (user.UserName != null && user.Password != null)
                return true;
            return false;
        }

        public UserView Authorization(UserEdit user)
        {
            User authed = DbContext_1.GetUserWithLogin(user.UserName,user.Password);
            if(AuthCheck(authed))
            {
                if (StatusCheck(authed)) { 
                    FormsAuthentication.SetAuthCookie(authed.ID.ToString(), false);
                    UserView returnUser = MapUser(authed);
                    return returnUser;
                }
            }
            return new UserView();
        }

        private UserView MapUser(User user)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<User, UserView>());
            var mapper = new Mapper(config);
            UserView mappeduser = mapper.Map<UserView>(user);
            return mappeduser;
        }

        private bool StatusCheck(User user)
        {
            return user.Status == 1;
        }
    }
}
