namespace MugStore.Data.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using MugStore.Data.Models;
    using System.IO;

    public sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
            this.ContextKey = "MugStore.Data.ApplicationDbContext";
        }

        protected override void Seed(ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            //this.SeedCities(context);
        }

        private void SeedCities(ApplicationDbContext context)
        {
            var cities = new City[4];
            cities[0] = new City() { Name = "СОФИЯ", PostCode = 1000, Highlight = true };
            cities[1] = new City() { Name = "Пловдив", PostCode = 4000, Highlight = true };
            cities[2] = new City() { Name = "Варна", PostCode = 9000, Highlight = true };
            cities[3] = new City() { Name = "Асеновград", PostCode = 4230, Highlight = false };

            context.Cities.AddOrUpdate(
                c => c.PostCode,
                cities
            );

            //var reader = new StreamReader(File.OpenRead(Directory.GetCurrentDirectory() + @"speedy_sites.csv"));
            //while (!reader.EndOfStream)
            //{
            //    var line = reader.ReadLine();
            //    var values = line.Split(',');
            //}
        }
    }
}
