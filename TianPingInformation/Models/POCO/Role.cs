using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TianPingInformation.Models.POCO
{
    public class Role
    {
        public Role()
        {
            this.Users = new HashSet<User>();
        }

        public int RoleId { get; set; }
        public string RoleName { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}