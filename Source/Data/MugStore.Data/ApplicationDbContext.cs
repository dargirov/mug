namespace MugStore.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using Common.Models;
    using Models;

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }

        public IDbSet<Order> Orders { get; set; }

        public IDbSet<City> Cities { get; set; }

        public IDbSet<Image> Images { get; set; }

        public IDbSet<Bulletin> Bulletins { get; set; }

        public IDbSet<Product> Products { get; set; }

        public IDbSet<ProductImage> ProductImages { get; set; }

        public IDbSet<ProductTag> ProductTags { get; set; }

        public IDbSet<Category> Categories { get; set; }

        public IDbSet<DeliveryInfo> DeliveryInfoes { get; set; }

        public IDbSet<Courier> Couriers { get; set; }

        public IDbSet<Feedback> Feedbacks { get; set; }

        public IDbSet<Log> Logs { get; set; }

        public IDbSet<Post> Posts { get; set; }

        public IDbSet<PostTag> PostTags { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public override int SaveChanges()
        {
            this.ApplyAuditInfoRules();
            return base.SaveChanges();
        }

        private void ApplyAuditInfoRules()
        {
            // Approach via @julielerman: http://bit.ly/123661P
            foreach (var entry in
                this.ChangeTracker.Entries()
                    .Where(
                        e =>
                        e.Entity is IAuditInfo && ((e.State == EntityState.Added) || (e.State == EntityState.Modified))))
            {
                var entity = (IAuditInfo)entry.Entity;
                if (entry.State == EntityState.Added && entity.CreatedOn == default(DateTime))
                {
                    entity.CreatedOn = DateTime.Now;
                }
                else
                {
                    entity.ModifiedOn = DateTime.Now;
                }
            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<OrderProduct>()
            //    .HasRequired(c => c.ProductVariant)
            //    .WithMany()
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Address>()
            //    .HasRequired(c => c.User)
            //    .WithMany()
            //    .WillCascadeOnDelete(false);

            //base.OnModelCreating(modelBuilder);
        }
    }
}
