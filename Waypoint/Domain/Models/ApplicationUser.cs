using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Domain.Models
{
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> userManager)
        {
            return await userManager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
        }

        [Required]
        public virtual Account Account { get; set; }

        public virtual TimeZone TimeZone { get; set; }

        public virtual ICollection<ApiToken> ApiTokens { get; set; }
    }
}
