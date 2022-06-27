using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APBDProject.Server.Models
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<ApplicationUserStock> ApplicationUserStocks { get; set; }
    }
}
