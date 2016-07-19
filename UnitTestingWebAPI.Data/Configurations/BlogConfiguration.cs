using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using UnitTestingWebAPI.Domain;

namespace UnitTestingWebAPI.Data.Configurations
{
    public class BlogConfiguration : EntityTypeConfiguration<Blog>
    {
        public BlogConfiguration()
        {
            ToTable("Blog");
            Property(b => b.Name).IsRequired().HasMaxLength(100);
            Property(b => b.URL).IsRequired().HasMaxLength(200);
            Property(b => b.Owner).IsRequired().HasMaxLength(50);
            Property(b => b.DateCreated).HasColumnType("datetime2");
        }
    }

    public class BloggerEntities : System.Data.Entity.DbContext
    {
        public BloggerEntities() : base("BloggerEntities") {
            Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Article> Articles { get; set; }

        public virtual void Commit()
        {
            base.SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ArticleConfiguration());
            modelBuilder.Configurations.Add(new BlogConfiguration());
        }
    }
}
