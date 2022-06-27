using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APBDProject.Shared.Models
{
    public class AddressDetails
    {
#nullable enable
        [JsonProperty("city")]
        public string? City { get; set; }
#nullable disable
    }
}
