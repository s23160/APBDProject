using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APBDProject.Shared.Models
{
    public class ArticleDetails
    {
        [JsonProperty("id")]
        public string IdentifierName { get; set; }

        [JsonProperty("published_utc")]
        public DateTime PublishedDate { get; set; }

#nullable enable
        [JsonProperty("author")]
        public string? Author { get; set; }

        [JsonProperty("title")]
        public string? Title { get; set; }

        [JsonProperty("description")]
        public string? Description { get; set; }

        [JsonProperty("image_url")]
        public string ImageUrl { get; set; }
#nullable disable
    }
}
