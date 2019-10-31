using Microsoft.AspNet.Identity.EntityFramework;

namespace EpamFinalProject.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<EpamFinalProject.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "EpamFinalProject.Models.ApplicationDbContext";
        }

        protected override void Seed(EpamFinalProject.Models.ApplicationDbContext context)
        {
            
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
