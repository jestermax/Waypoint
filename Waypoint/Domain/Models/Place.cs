using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Spatial;
using System.Text;
using Domain.Configuration;

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
        public double MinimumLatitude { get; set; }

        [Required]
        public double MinimumLongitude { get; set; }

        [Required]
        public double MaximumLatitude { get; set; }

        [Required]
        public double MaximumLongitude { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }

        [Required]
        public DateTime DateModified { get; set; }

        public static Place Create(string id, string name, Account account, List<DbGeography> coordinates)
        {
            if (coordinates.Count < 3)
            {
                throw new ArgumentException("Cannot create place with less than 3 coordinates");
            }

            var utcNow = DateTime.UtcNow;

            var stringBuilder = new StringBuilder("POLYGON((");

            var boundary = DbGeography.PolygonFromText(stringBuilder.ToString(), AppConfiguration.CoordinateSystemId);

            double
                minimumLatitude = Double.MaxValue,
                minimumLongitude = Double.MaxValue,
                maximumLatitude = Double.MinValue,
                maximumLongitude = Double.MinValue;

            for (var i = 0; i < coordinates.Count; i++)
            {
                double
                    latitude = coordinates[i].Latitude.HasValue ? coordinates[i].Latitude.Value : 0,
                    longitude = coordinates[i].Longitude.HasValue ? coordinates[i].Longitude.Value : 0;

                stringBuilder.Append(String.Format("{0} {1}", longitude, latitude));

                if (latitude < minimumLatitude)
                {
                    minimumLatitude = latitude;
                }

                if (latitude > maximumLatitude)
                {
                    maximumLatitude = latitude;
                }

                if (longitude < minimumLongitude)
                {
                    minimumLongitude = longitude;
                }

                if (longitude > maximumLongitude)
                {
                    maximumLongitude = longitude;
                }

                if (i < coordinates.Count)
                {
                    stringBuilder.Append(",");
                }
            }

            stringBuilder.Append("))");

            return new Place
            {
                Id = id,
                Account = account,
                Name = name,
                Boundary = boundary,
                MinimumLatitude = minimumLatitude,
                MinimumLongitude = minimumLongitude,
                MaximumLatitude = maximumLatitude,
                MaximumLongitude = maximumLongitude,
                DateCreated = utcNow,
                DateModified = utcNow,
            };
        }
    }
}
