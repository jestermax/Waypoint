using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

using Domain.Authentication;
using Domain.Dto.Inbound;
using Domain.Dto.Outbound;
using Domain.Repositories;

namespace Presentation.ApiControllers
{
    public class SignInController : ApiController
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IApiTokenRepository _apiTokenRepository;
        private readonly IUserRepository _userRepsoitory;

        public SignInController()
        {
            _userRepsoitory = new UserRepository();
            _apiTokenRepository = new ApiTokenRepository();
        }

        [HttpPost]
        public async Task<LoginDto> Post(LoginAttemptDto loginAttempt)
        {
            if (loginAttempt == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            if (String.IsNullOrEmpty(loginAttempt.email) || String.IsNullOrWhiteSpace(loginAttempt.email))
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            if (String.IsNullOrEmpty(loginAttempt.password) || String.IsNullOrWhiteSpace(loginAttempt.password))
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            return new LoginDto();
        }
    }
}
