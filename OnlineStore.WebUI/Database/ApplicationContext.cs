using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

using OnlineStore.Domain.Models;
using OnlineStore.WebUI.Models;

namespace OnlineStore.WebUI.Database
{
    public class ApplicationContext:DbContext
    {
        public DbSet<ShopItem> ShopItems { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<ItemImage> Images { get; set; }

        public DbSet<UserAccount> Users { get; set; }
        public DbSet<WishList> WhishList { get; set; }
        public DbSet<SelectedClothes> Selected  { get; set; }       


        public ApplicationContext():base("DBConnection")    {}

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

        }

    }
}