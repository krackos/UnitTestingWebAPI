using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using UnitTestingWebAPI.Domain;
using System.Threading.Tasks;
using System.IO;
using System.Net.Http;

namespace UnitTestingWebAPI.API.Core.MediaTypeFormatters
{
    public class ArticleFormatter : BufferedMediaTypeFormatter
    {
        public ArticleFormatter()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/article"));
        }
        public override bool CanReadType(Type type)
        {
            return false;
        }
        public override bool CanWriteType(Type type)
        {
            if (type == typeof(Article))
                return true;
            else
            {
                Type _type = typeof(IEnumerable<Article>);
                return _type.IsAssignableFrom(type);
            }
        }
        public override void WriteToStream(Type type, object value, Stream writeStream, HttpContent content)
        {
            using (StreamWriter write = new StreamWriter(writeStream)) {
                var articles = value as IEnumerable<Article>;
                if (articles != null)
                {
                    foreach (var article in articles)
                    {
                        write.Write(String.Format("[{0},\"{1}\",\"{2}\",\"{3}\",\"{4}\"]",
                             article.ID,
                             article.Title,
                             article.Author,
                             article.URL,
                             article.Contents));
                    }
                }
                else
                {
                    var _article = value as Article;
                    if (_article == null)
                        throw new InvalidOperationException("Cannot serialize type");
                    write.Write(String.Format("[{0},\"{1}\",\"{2}\",\"{3}\",\"{4}\"]",
                             _article.ID,
                             _article.Title,
                             _article.Author,
                             _article.URL,
                             _article.Contents));
                }
            }
        }
    }
}
