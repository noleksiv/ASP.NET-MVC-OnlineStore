using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace OnlineStore.WebUI.Database
{
    public class ContextStoreInitializer : IDatabaseInitializer<ApplicationContext>
    {
        public void InitializeDatabase(ApplicationContext context)
        {
            if (context.Database.Exists())
            {
                if (!context.Database.CompatibleWithModel(true))
                {
                    context.Database.Delete();
                }
            }
            context.Database.Create();
        }
    }
}