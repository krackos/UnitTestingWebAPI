using System;
using System.Collections.Generic;
using System.Data.Entity;
using UnitTestingWebAPI.Data.Configurations;

namespace UnitTestingWebAPI.Data.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbFactory dbFactory;
        private BloggerEntities dbContext;

        public UnitOfWork(IDbFactory dbFactory) {
            this.dbFactory = dbFactory;
        }
        public BloggerEntities DbContext
        {
            get { return dbContext ?? (dbContext = dbFactory.Init()); }
        }
        public void Commit()
        {
            DbContext.Commit();
        }
    }
}
