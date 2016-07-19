using System;
using System.Collections.Generic;
using UnitTestingWebAPI.Data.Repositories;
using UnitTestingWebAPI.Data.Infrastructure;
using UnitTestingWebAPI.Domain;
using System.Linq;

namespace UnitTestingWebAPI.Services
{
    public interface IArticleService
    {
        IEnumerable<Article> GetArticles(string name = null);
        Article GetArticle(int id);
        Article GetArticle(string name);
        void CreateArticle(Article article);
        void UpdateArticle(Article article);
        void DeleteArticle(Article article);
        void SaveArticle();
    }
    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository articlesRepository;
        private readonly IUnitOfWork unitOfWork;

        public ArticleService(IArticleRepository articlesRepository, IUnitOfWork unitOfWork)
        {
            this.articlesRepository = articlesRepository;
            this.unitOfWork = unitOfWork;
        }
        public IEnumerable<Article> GetArticles(string title = null)
        {
            if (string.IsNullOrEmpty(title))
                return articlesRepository.GetAll();
            else
                return articlesRepository.GetAll().Where(c => c.Title.ToLower().Contains(title.ToLower()));
        }
        public Article GetArticle(int id)
        {
            var article = articlesRepository.GetById(id);
            return article;
        }
        public Article GetArticle(string title)
        {
            var article = articlesRepository.GetArticleByTitle(title);
            return article;
        }
        public void CreateArticle(Article article)
        {
            articlesRepository.Add(article);
        }
        public void UpdateArticle(Article article)
        {
            articlesRepository.Update(article);
        }
        public void DeleteArticle(Article article)
        {
            articlesRepository.Delete(article);
        }
        public void SaveArticle()
        {
            unitOfWork.Commit();
        }
    }
}
