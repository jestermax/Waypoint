using System;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;

using Microsoft.AspNet.Identity;

using Domain.Configuration;
using Domain.Models;
using Domain.Repositories;

namespace Presentation.ApiControllers
{
    public class AuthenticatedApiController : ApiController
    {
        protected ApplicationUser ApplicationUser;

        protected IUserRepository UserRepository;
        protected IApiTokenRepository ApiTokenRepository;

        public AuthenticatedApiController(IUserRepository userRepository, IApiTokenRepository apiTokenRepository)
        {
            UserRepository = userRepository;
            ApiTokenRepository = apiTokenRepository;

            if (HttpContext.Current.Request.IsAuthenticated)
            {
                ApplicationUser = UserRepository.Get(User.Identity.GetUserId());

                if (ApplicationUser == null)
                {
                    throw new HttpResponseException(HttpStatusCode.Unauthorized);
                }
            }
            else
            {
                string apiToken;

                try
                {
// ReSharper disable once AssignNullToNotNullAttribute
                    apiToken = HttpContextFactory.Current.Request.Headers.GetValues(AppConfiguration.ApiTokenHeader).First();
                }
                catch
                {
                    throw new HttpResponseException(HttpStatusCode.Unauthorized);
                }

                var authenticatedUser = ValidateApiToken(apiToken);

                if (authenticatedUser == null)
                {
                    throw new HttpResponseException(HttpStatusCode.Unauthorized);
                }

                ApplicationUser = authenticatedUser;
            }
        }

        private ApplicationUser ValidateApiToken(string candidateApiToken)
        {
            if (String.IsNullOrEmpty(candidateApiToken))
            {
                return null;
            }

            var apiToken = ApiTokenRepository.Get(candidateApiToken);

            if (apiToken == null)
            {
                return null;
            }

            if (apiToken.DateExpires < DateTime.UtcNow)
            {
                return null;
            }

            apiToken.DateExpires = DateTime.UtcNow.AddDays(AppConfiguration.ApiTokenDaysIssued);

            var result = ApiTokenRepository.Update(apiToken.Id, apiToken);

            return !result ? null : apiToken.User;
        }
    }
}
