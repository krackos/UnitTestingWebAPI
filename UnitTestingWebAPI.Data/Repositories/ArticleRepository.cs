using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using UnitTestingWebAPI.Data.Infrastructure;
using UnitTestingWebAPI.Domain;

namespace UnitTestingWebAPI.Data.Repositories
{
    public interface IArticleRepository : IRepository<Article> {
        Article GetArticleByTitle(string articleTitle);
    }
    public class ArticleRepository : RepositoryBase<Article>, IArticleRepository
    {
        public ArticleRepository(IDbFactory dbFactory)
              : base(dbFactory) {}

        public Article GetArticleByTitle(string articleTitle)
        {
            var _article = this.DbContext.Articles.Where(b => b.Title == articleTitle).FirstOrDefault();
            return _article;
        }
    }
}
