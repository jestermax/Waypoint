using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

using Domain.Configuration;
using Domain.Dto.Inbound;
using Domain.Dto.Outbound;
using Domain.Helpers;
using Domain.Models;
using Domain.Repositories;

namespace Presentation.ApiControllers
{
    public class SignInController : ApiController
    {
        private readonly ApplicationDbContext _context = ApplicationDbContext.Create();

        private readonly IApiTokenRepository _apiTokenRepository;

        public SignInController()
        {
            _apiTokenRepository = new ApiTokenRepository(_context);
        }

        [System.Web.Mvc.HttpPost]
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

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_context));

            var applicationUser = await userManager.FindAsync(loginAttempt.email, loginAttempt.password);

            if (applicationUser == null)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            var utcNow = DateTime.UtcNow;

            var apiToken = _apiTokenRepository.Add(new ApiToken
            {
                Id = Guid.NewGuid().ToString(),
                User = applicationUser,
                Token = ApiTokenGenerator.Create(_context),
                DateExpires = utcNow.AddDays(AppConfiguration.ApiTokenDaysIssued),
                DateIssued = utcNow
            });

            if (apiToken == null)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }

            return new LoginDto(applicationUser, apiToken);
        }
    }
}
