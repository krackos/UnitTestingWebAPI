using Microsoft.Owin.Hosting;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using UnitTestingWebAPI.API.Core.Controllers;
using UnitTestingWebAPI.API.Core.MessageHandlers;
using UnitTestingWebAPI.Data;
using UnitTestingWebAPI.Data.Infrastructure;
using UnitTestingWebAPI.Data.Repositories;
using UnitTestingWebAPI.Domain;
using UnitTestingWebAPI.Service;
using UnitTestingWebAPI.Tests.Hosting;

namespace UnitTestingWebAPI.Tests
{
    [TestFixture]
    public class MessageHandlerTest {
        private EndRequestHandler _endRequestHandler;
        private HeaderAppenderHandler _headerAppenderHandler;

        [SetUp]
        public void Setup()
        {
            _endRequestHandler = new EndRequestHandler();
            _headerAppenderHandler = new HeaderAppenderHandler()
            {
                InnerHandler = _endRequestHandler
            };
        }
        [Test]
        public async void ShouldAppendCustomHandler() {
            var invoker = new HttpMessageInvoker(_headerAppenderHandler);
            var result = await invoker.SendAsync(new HttpRequestMessage(HttpMethod.Get, 
              new Uri("http://localhost/api/test/")), CancellationToken.None);

            Assert.That(result.Headers.Contains("X-WebAPI-Heander"), Is.True);
            Assert.That(result.Content.ReadAsStreamAsync().Result, Is.EqualTo("Unit testing message handlers!"));
        }

    }
}