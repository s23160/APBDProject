using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APBDProject.Shared.Models
{
    public class BrandingDetails
    {
#nullable enable
        [JsonProperty("logo_url", NullValueHandling = NullValueHandling.Ignore)]
        public string? LogoUrl { get; set; }
#nullable disable
    }
}
