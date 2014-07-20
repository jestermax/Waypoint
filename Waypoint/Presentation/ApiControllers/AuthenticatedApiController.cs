using System.Web.Http;

using Domain.Models;

namespace Presentation.ApiControllers
{
    public class AuthenticatedApiController : ApiController
    {
        protected ApplicationUser ApplicationUser;

        public AuthenticatedApiController()
        {
            
        }
    }
}
