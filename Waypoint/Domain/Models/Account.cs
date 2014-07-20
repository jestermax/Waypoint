using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Account
    {
        [Key]
        [Required]
        [MaxLength(128)]
        public string Id { get; set; }

        [Required]
        [MaxLength(128)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DateCreated { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DateModified { get; set; }

        public virtual ICollection<ApplicationUser> Users { get; set; }
    }
}
