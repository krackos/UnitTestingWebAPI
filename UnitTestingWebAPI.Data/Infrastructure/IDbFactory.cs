using System;
using System.Collections.Generic;
using UnitTestingWebAPI.Data.Configurations;

namespace UnitTestingWebAPI.Data.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        BloggerEntities Init();
    }
}
