using System;
using System.Collections.Generic;

#nullable disable

namespace article_manager.Data.Entities
{
    public partial class Article
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Idcategory { get; set; }
        public string Content { get; set; }

        public virtual ArticleCategory IdcategoryNavigation { get; set; }
    }
}
