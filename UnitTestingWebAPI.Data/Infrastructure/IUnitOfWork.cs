using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace UnitTestingWebAPI.Data.Infrastructure
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}
