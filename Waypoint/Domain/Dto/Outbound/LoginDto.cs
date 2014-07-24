using Domain.Models;

namespace Domain.Dto.Outbound
{
    public class LoginDto
    {
        public LoginDto(ApplicationUser applicationUser, ApiToken apiToken)
        {
            user = new UserDto(applicationUser);
            this.apiToken = new ApiTokenDto(apiToken);
        }

        public UserDto user { get; set; }

        public ApiTokenDto apiToken { get; set; }
    }
}
