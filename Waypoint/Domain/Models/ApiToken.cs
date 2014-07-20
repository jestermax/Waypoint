using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class ApiToken
    {
        [Key]
        [Required]
        [MaxLength(128)]
        public string Id { get; set; }

        [Required]
        public virtual ApplicationUser User { get; set; }

        [Required]
        [MaxLength(128)]
        public string Token { get; set; }

        [Required]
        public DateTime DateIssued { get; set; }

        [Required]
        public DateTime DateExpires { get; set; }
    }
}
