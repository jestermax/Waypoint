using System.ComponentModel.DataAnnotations;

namespace Domain.Dto.Inbound
{
    public class LocationUpdateDto
    {
        [Required]
        [Display(Name = "Latitude")]
        public double latitude { get; set; }

        [Required]
        [Display(Name = "Longitude")]
        public double longitude { get; set; }

        [Required]
        [Display(Name = "Accuracy")]
        public double accuracy { get; set; }

        [Required]
        [Display(Name = "Speed")]
        public double speed { get; set; }

        [Required]
        [Display(Name = "Timestamp")]
        public string timestamp { get; set; }
    }
}
