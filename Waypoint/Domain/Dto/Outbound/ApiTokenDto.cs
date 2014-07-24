using Domain.Models;

namespace Domain.Dto.Outbound
{
    public class ApiTokenDto
    {
        public ApiTokenDto(ApiToken apiToken)
        {
            id = apiToken.Id;
            userId = apiToken.User.Id;
            token = apiToken.Token;
            dateIssued = apiToken.DateIssued.ToString("s");
            dateExpires = apiToken.DateExpires.ToString("s");
        }

        public string id { get; set; }

        public string userId { get; set; }

        public string token { get; set; }

        public string dateIssued { get; set; }

        public string dateExpires { get; set; }
    }
}
