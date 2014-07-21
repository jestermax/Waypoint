using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Threading.Tasks;

using Domain.Configuration;

namespace Domain.Geocoding
{
    public class AggregatedGeocoder : IGeocoder
    {
        private readonly List<IGeocoder> _geocoders;

        public AggregatedGeocoder()
        {
            _geocoders = new List<IGeocoder>
            {
                new MapQuestGeocoder(),
                new NominatimGeocoder()
            };
        }

        public DbGeography Geocode(string address)
        {
            if (!IsValidForwardGeocodeRequest(address))
            {
                return null;
            }

            DbGeography result = null;

            while (result == null && _geocoders.Count > 0)
            {
                var geocoder = GetRandomGeocoder();

                result = geocoder.Geocode(address);
            }

            return result;
        }

        private IGeocoder GetRandomGeocoder()
        {
            var index = new Random(DateTime.UtcNow.Millisecond).Next(0, _geocoders.Count);

            var geocoder = _geocoders[index];
            _geocoders.RemoveAt(index);

            return geocoder;
        }

        public async Task<DbGeography> GeocodeAsync(string address)
        {
            if (!IsValidForwardGeocodeRequest(address))
            {
                return null;
            }

            DbGeography result = null;

            while (result == null && _geocoders.Count > 0)
            {
                var geocoder = GetRandomGeocoder();

                result = await geocoder.GeocodeAsync(address);
            }

            return result;
        }

        public string ReverseGeocode(DbGeography location)
        {
            if (location == null)
            {
                return null;
            }

            if (!location.Latitude.HasValue || !location.Longitude.HasValue)
            {
                return null;
            }

            return ReverseGeocode(location.Latitude.Value, location.Longitude.Value);
        }

        public async Task<string> ReverseGeocodeAsync(DbGeography location)
        {
            if (location == null)
            {
                return null;
            }

            if (!location.Latitude.HasValue || !location.Longitude.HasValue)
            {
                return null;
            }

            return await ReverseGeocodeAsync(location.Latitude.Value, location.Longitude.Value);
        }

        public string ReverseGeocode(double latitude, double longitude)
        {
            if (!IsValidReverseGeocodeRequest(latitude, longitude))
            {
                return null;
            }

            string result = null;

            while (result == null && _geocoders.Count > 0)
            {
                var geocoder = GetRandomGeocoder();

                result = geocoder.ReverseGeocode(latitude, longitude);
            }

            return result;
        }

        public async Task<string> ReverseGeocodeAsync(double latitude, double longitude)
        {
            if (!IsValidReverseGeocodeRequest(latitude, longitude))
            {
                return null;
            }

            string result = null;

            while (result == null && _geocoders.Count > 0)
            {
                var geocoder = GetRandomGeocoder();

                result = await geocoder.ReverseGeocodeAsync(latitude, longitude);
            }

            return result;
        }

        private static bool IsValidForwardGeocodeRequest(string address)
        {
            return (!String.IsNullOrEmpty(address) || !String.IsNullOrWhiteSpace(address));
        }

        private static bool IsValidReverseGeocodeRequest(double latitude, double longitude)
        {
            if (latitude < AppConfiguration.MinimumLatitude | latitude > AppConfiguration.MaximumLatitude)
            {
                return false;
            }

            if (longitude < AppConfiguration.MinimumLongitude | longitude > AppConfiguration.MaximumLongitude)
            {
                return false;
            }

            return (!(Math.Abs(latitude) < Double.Epsilon)) || (!(Math.Abs(longitude) < Double.Epsilon));
        }
    }
}
