using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class TimeZone
    {
        [Key]
        [Required]
        [MaxLength(128)]
        public string Id { get; set; }

        [Required]
        [MaxLength(128)]
        public string Name { get; set; }

        [Required]
        public int Offset { get; set; }

        [Required]
        public int SortOrder { get; set; }
    }
}
