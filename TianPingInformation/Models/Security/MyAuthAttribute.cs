/**********************************************************************
 * MyAuthAttribute:自定义authorize attribute,用于角色的验证，以代替默认
 * 的Authorize attribute.
 * *******************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TianPingInformation.Models.POCO;

namespace TianPingInformation.Models.Security
{
    public class MyAuthAttribute : AuthorizeAttribute
    {
        TianPingInfoDBContext db = new TianPingInfoDBContext();

        // 只需重载此方法，模拟自定义角色授权机制
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext.User.Identity.Name != null && httpContext.User.Identity.Name != "")
            {
                string currentRole = GetRole(httpContext.User.Identity.Name);
                if (Roles.Contains(currentRole))
                    return true;
            }
            return base.AuthorizeCore(httpContext);
        }

        // 返回当前用户对应的角色
        private string GetRole(string name)
        {
            List<Role> roles = db.Users.Where(u => u.UserName.Equals(name)).FirstOrDefault().Roles.ToList();
            if (roles.Count > 0)
                return roles.First().RoleName;
            else
                return null;
        }
    }
}