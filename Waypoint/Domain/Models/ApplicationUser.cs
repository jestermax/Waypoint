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

        [Required]
        public virtual TimeZone TimeZone { get; set; }

        [Required]
        [MaxLength(128)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(128)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(128)]
        public string ProfileImage { get; set; }

        [Required]
        public bool Metric { get; set;  }

        public virtual ICollection<ApiToken> ApiTokens { get; set; }
    }
}
