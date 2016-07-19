using System.Data.Entity;
using UnitTestingWebAPI.Domain;

namespace UnitTestingWebAPI.Data.Configurations
{
    public class BloggerEntities : System.Data.Entity.DbContext
    {
        public BloggerEntities() : base("BloggerEntities")
        {
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
