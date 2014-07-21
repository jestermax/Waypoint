using System.Threading.Tasks;
using System.Web.Mvc;
using Domain.Dto.Outbound;
using Domain.Repositories;

namespace Presentation.ApiControllers
{
    public class UserLocationController : AuthenticatedApiController
    {
        private readonly IUserLocationRepository _userLocationRepository;

        public UserLocationController(IUserLocationRepository userLocationRepository, IUserRepository userRepository, IApiTokenRepository apiTokenRepository)
            : base(userRepository, apiTokenRepository)
        {
            _userLocationRepository = userLocationRepository;
        }

        [HttpGet]
        public async Task<UserLocationDto[]> Get()
        {
            return null;
        }
    }
}
