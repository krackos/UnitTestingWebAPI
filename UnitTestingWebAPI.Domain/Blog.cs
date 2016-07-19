using System;
using System.Collections.Generic;

namespace UnitTestingWebAPI.Domain
{
   public class Blog
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
        public string Owner { get; set; }
        public DateTime DateCreated { get; set; }
        public virtual ICollection<Article> Articles { get; set; }

        public Blog()
        {
            Articles = new HashSet<Article>();
        }
    }
}
