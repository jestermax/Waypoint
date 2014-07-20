using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Country
    {
        [Key]
        [Required]
        [MaxLength(128)]
        public string Id { get; set; }

        [Required]
        [MaxLength(128)]
        public string Name { get; set; }
    }
}
