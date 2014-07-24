using Domain.Models;

namespace Domain.Dto.Outbound
{
    public class UserDto
    {
        public UserDto(ApplicationUser applicationUser)
        {
            id = applicationUser.Id;
            accountId = applicationUser.Account.Id;
            email = applicationUser.Email;
        }

        public string id { get; set; }

        public string accountId { get; set; }

        public string email { get; set; }
    }
}
