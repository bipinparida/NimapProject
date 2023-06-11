using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace NimapProject.Models
{
    public class StoreDbContext:DbContext
    {
        public StoreDbContext():base("StoreDbContext")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<StoreDbContext>());
        }
        public DbSet<Category> Categories { get; set;}
        public DbSet<Product> Products { get; set; }
    }
}