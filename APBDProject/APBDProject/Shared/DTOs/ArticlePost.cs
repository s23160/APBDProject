using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APBDProject.Shared.DTOs
{
    public class ArticlePost
    {
        public string Ticker { get; set; }
        public string IdentifierName { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PublishedDate { get; set; }
        public string ImageUrl { get; set; }
    }

    public class ArticleListPost
    {
        public List<ArticlePost> Articles { get; set; }
    }
}
