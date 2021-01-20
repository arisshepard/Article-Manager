using System;
using System.Collections.Generic;

#nullable disable

namespace article_manager.Data.Entities
{
    public partial class ArticleCategory
    {
        public ArticleCategory()
        {
            Articles = new HashSet<Article>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Article> Articles { get; set; }
    }
}
