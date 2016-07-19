using System;
using System.Collections.Generic;
using System.Linq;
using UnitTestingWebAPI.Services;
using UnitTestingWebAPI.Domain;
using System.Web.Http.Filters;
using System.Net.Http;

namespace UnitTestingWebAPI.API.Core.Filters
{
    public class ArticlesReversedFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            var objectContent = actionExecutedContext.Response.Content as ObjectContent;
            if (objectContent != null)
            {
                List<Article> _articles = objectContent.Value as List<Article>;
                if (_articles != null && _articles.Count > 0)
                {
                    _articles.Reverse();
                }
            }
        }
    }
}
