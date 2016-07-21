using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTestingWebAPI.Data;
using UnitTestingWebAPI.Data.Infrastructure;
using UnitTestingWebAPI.Data.Repositories;
using UnitTestingWebAPI.Domain;
using UnitTestingWebAPI.Services;

namespace UnitTestingWebAPI.Tests
{
    [TestFixture]
    public class ServicesTest
    {
        IArticleService _articleService;
        IArticleRepository _articleRepository;
        IUnitOfWork _unitOfWork;
        List<Article> _randomArticles;

        [SetUp]
        public void Setup()
        {
            _randomArticles = SetupArticles();

            _articleRepository = SetupArticleRepository();
            _unitOfWork = new Mock<IUnitOfWork>().Object;
            _articleService = new ArticleService(_articleRepository, _unitOfWork);
        }

        private List<Article> SetupArticles()
        {
            int _counter = new int();
            List<Article> _articles = BloggerInitializer.GetAllArticles();
            foreach (Article _article in _articles)
                _article.ID = ++_counter;

            return _articles;
        }

        private IArticleRepository SetupArticleRepository()
        {
            var repo = new Mock<IArticleRepository>();
            repo.Setup(r => r.GetAll()).Returns(_randomArticles);

            repo.Setup(r => r.GetById(It.IsAny<int>()))
                .Returns(new Func<int, Article>(
                    id => _randomArticles.Find(a => a.ID.Equals(id))));

            repo.Setup(r => r.Add(It.IsAny<Article>()))
                .Callback(new Action<Article>(newArticle => {
                    dynamic maxArticleID = _randomArticles.Last().ID;
                    dynamic nextArticleID = maxArticleID + 1;
                    newArticle.ID = nextArticleID;
                    newArticle.DateCreated = DateTime.Now;
                    _randomArticles.Add(newArticle);
                }));
            repo.Setup(r => r.Update(It.IsAny<Article>()))
                .Callback(new Action<Article>(x => {
                    var _articleToRemove = _randomArticles.Find(a => a.ID == x.ID);
                    if (_articleToRemove != null)
                        _randomArticles.Remove(_articleToRemove);
                }));
            repo.Setup(r => r.Delete(It.IsAny<Article>()))
                .Callback(new Action<Article>(x => {
                    var _articleToRemove = _randomArticles.Find(a => a.ID == x.ID);
                    if (_articleToRemove != null)
                        _randomArticles.Remove(_articleToRemove);
                }));
            return repo.Object;
        }

        [Test]
        public void ServiceShouldReturnAllArticles() {
            var articles = _articleService.GetArticles();
            Assert.That(articles, Is.EqualTo(_randomArticles));
        }
    }
}
