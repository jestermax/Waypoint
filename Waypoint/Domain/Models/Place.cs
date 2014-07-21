using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Spatial;

namespace Domain.Models
{
    public class Place
    {
        [Key]
        [Required]
        [MaxLength(128)]
        public string Id { get; set; }

        [Required]
        [MaxLength(128)]
        public virtual Account Account { get; set; }

        [Required]
        [MaxLength(256)]
        public string Name { get; set; }

        [Required]
        public DbGeography Boundary { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }

        [Required]
        public DateTime DateModified { get; set; }
    }
}
