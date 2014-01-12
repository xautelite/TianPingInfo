using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Infrastructure;
using TianPingInformation.Models.POCO;

namespace TianPingInformation.Models
{
    public class TianPingInfoDBContext:DbContext
    {
        // 天平驾校学员信息系统连接字符串
        private readonly static string TianPingInfo = "TianPingInfo";

        public TianPingInfoDBContext()
            : base(TianPingInfo)
        { 
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Student> Students { get; set; }

        // 重写方法，可以在这移除一些配置数据库映射关系的契约
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();     // 移除复数表名的契约
            modelBuilder.Conventions.Remove<IncludeMetadataConvention>();         // 防止黑幕交易，否则每次都要访问EdmMetadata这个表
        }

        public DbSet<Role> Role { get; set; }
    }
}