using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace TianPingInformation.Models.POCO
{
    public class User
    {
        public User()
        {
            this.Roles = new HashSet<Role>();
        }

        public int UserId { get; set; }

        [Required]
        [DisplayName("用户名")]
        public string UserName { get; set; }

        [Required]
        [DisplayName("显示名")]
        public string DisplayName { get; set; }

        [Required]
        [DisplayName("密码")]
        public string Password { get; set; }

        [DisplayName("记住密码?")]
        public bool Enabled { get; set; }

        [Required]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage="邮箱格式不正确")]
        public string Email { get; set; }

        public virtual ICollection<Role> Roles { get; set; }
    }
}