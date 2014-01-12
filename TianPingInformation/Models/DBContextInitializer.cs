using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.Validation;
using TianPingInformation.Models.POCO;

namespace TianPingInformation.Models
{
    public class DBContextInitializer : DropCreateDatabaseIfModelChanges<TianPingInfoDBContext>
    {
        protected override void Seed(TianPingInfoDBContext context)
        {
            #region Add Users
            List<User> users = new List<User> 
            {
                new User { UserName="admin", DisplayName="admin", Password="admin", Email="admin@gmail.com", Enabled=true}
            };
            foreach (User u in users)
            {
                context.Users.Add(u);
            }
            #endregion

            // add data into context and save to db
            try
            {
                context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx) //debug errors
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Console.Write("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
            }
        }
    }
}