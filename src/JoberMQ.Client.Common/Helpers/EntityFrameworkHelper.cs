using Microsoft.EntityFrameworkCore;
using System;

namespace JoberMQ.Client.Common.Helpers
{
    internal class EntityFrameworkHelper
    {
        internal static bool Migrate(DbContext context)
        {
            //using (var context = new LocalSqliteDbContext())
            //{
            //    //context.Database.EnsureCreated();
            //    context.Database.SetCommandTimeout(999999);
            //    context.Database.Migrate();
            //}

            try
            {
                context.Database.SetCommandTimeout(999999);
                context.Database.Migrate();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
