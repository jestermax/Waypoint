using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Spatial;

namespace Domain.Models
{
    public class UserLocation
    {
        [Key]
        [Required]
        [MaxLength(128)]
        public string Id { get; set; }

        [Required]
        public virtual ApplicationUser User { get; set; }

        [Required]
        [MaxLength(128)]
        public virtual UserLocationReason UserLocationReason { get; set; }

        [Required]
        public DbGeography Location { get; set; }

        [Required]
        [MaxLength(256)]
        public string Address { get; set; }

        [Required]
        public double Accuracy { get; set; }

        [Required]
        public double Speed { get; set; }

        [Required]
        public DateTime DateSent { get; set; }

        [Required]
        public DateTime DateReceived { get; set; }
    }
}
