using System.ComponentModel.DataAnnotations;

namespace Domain.Dto.Inbound
{
    public class LoginAttemptDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string password { get; set; }

        public bool rememberMe { get; set; }
    }
}
