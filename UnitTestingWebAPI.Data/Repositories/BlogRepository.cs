using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using UnitTestingWebAPI.Data.Infrastructure;
using UnitTestingWebAPI.Domain;

namespace UnitTestingWebAPI.Data.Repositories
{
    public class BlogRepository : RepositoryBase<Blog>, IBlogRepository
    {
        public BlogRepository(IDbFactory dbFactory)
             : base(dbFactory){ }

        public Blog GetBlogByName(string blogName)
        {
            var _blog = this.DbContext.Blogs.Where(b => b.Name == blogName).FirstOrDefault();
            return _blog;
        }
    }
    public interface IBlogRepository : IRepository<Blog>
    {
        Blog GetBlogByName(string blogName);
    }
}
