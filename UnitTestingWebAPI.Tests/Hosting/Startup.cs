
using Autofac;
using Autofac.Integration.WebApi;
using Moq;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Http.SelfHost;
using UnitTestingWebAPI.API.Core;
using UnitTestingWebAPI.API.Core.Controllers;
using UnitTestingWebAPI.API.Core.Filters;
using UnitTestingWebAPI.API.Core.MessageHandlers;
using UnitTestingWebAPI.Data;
using UnitTestingWebAPI.Data.Infrastructure;
using UnitTestingWebAPI.Data.Repositories;
using UnitTestingWebAPI.Domain;
using UnitTestingWebAPI.Service;

public class Startup
{
    public void Configuration(IAppBuilder appBuilder){
        var config = new HttpConfiguration();
        config.MessageHandlers.Add(new HeaderAppenderHandler());
        config.MessageHandlers.Add(new EndRequestHandler());
        config.Filters.Add(new ArticlesReversedFilter());
        config.Services.Replace(typeof(IAssembliesResolver), new CustomAssembliesResolver());

        config.Routes.MapHttpRoute(
            name: "DefaultApi",
            routeTemplate: "api/{controller}/{id}",
            defaults: new {id = RouteParameter.Optional}
        );
        config.MapHttpAttributeRoutes();

        var builder = new ContainerBuilder();
        builder.RegisterApiControllers(typeof(ArticlesController).Assembly);

        var _unitOfWork = new Mock<IUnitOfWork>();
        builder.RegisterInstance(_unitOfWork.Object).As<IUnitOfWork>();

        var _articlesRespository = new Mock<IArticleRepository>();
        _articlesRespository.Setup(x => x.GetAll()).Returns(
            BloggerInitializer.GetAllArticles()
        );
        builder.RegisterInstance(_articlesRespository.Object).As<IBlogRepository>();

        builder.RegisterAssemblyTypes(typeof(ArticleService).Assembly)
           .Where(t => t.Name.EndsWith("Service"))
           .AsImplementedInterfaces().InstancePerRequest();

        builder.RegisterInstance(new ArticleService(_articlesRespository.Object, _unitOfWork.Object));
        builder.RegisterInstance(new BlogService(_blogsRepository.Object, _unitOfWork.Object));

        IContainer container = builder.Build();
        config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

        appBuilder.UserWebApi(config);
    }
    [Test]
    public void ShouldCallToControllerActionAppendCustomHeader() {
        var address = "http://localhost:9000/";
        using(WebApp.Start<Startup>(address)){
           HttpClient _client = new HttpClient();
           var respond =
        }
    }
}