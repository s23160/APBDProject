using System;
using System.Collections.Generic;

namespace APBDProject.Server.Models
{
    public class Article
    {
        public int IdArticle { get; set; }
        public string IdentifierName { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PublishedDate { get; set; }
        public string ImageUrl { get; set; }
        public virtual ICollection<StockArticle> StockArticles { get; set; }
    }
}
