using System;
using System.Data.Entity.Spatial;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

using Domain.Configuration;

namespace Domain.Geocoding
{
    public class NominatimGeocoder : IGeocoder
    {
        private const string ForwardGeocodeUrl =
            "http://nominatim.openstreetmap.org/search?q={0}&format=jsonv2&polygon=1&addressdetails=0&limit=1";

        private const string ReverseGeocodeUrl =
            "http://nominatim.openstreetmap.org/reverse?format=json&lat={0}&lon={1}&zoom=18&addressdetails=1";

        public DbGeography Geocode(string address)
        {
            if (String.IsNullOrEmpty(address) || String.IsNullOrWhiteSpace(address))
            {
                return null;
            }

            var request = WebRequest.Create(String.Format(ForwardGeocodeUrl, address));

            using (var response = request.GetResponse() as HttpWebResponse)
            {
                if ((response == null) || (response.StatusCode != HttpStatusCode.OK))
                {
                    return null;
                }

                var jsonSerializer = new DataContractJsonSerializer(typeof(GeocodeResponse[]));
                var stream = response.GetResponseStream();

                if (stream == null)
                {
                    return null;
                }

                var objectResponse = jsonSerializer.ReadObject(stream);
                var jsonResponse = objectResponse as GeocodeResponse[];

                if (jsonResponse == null)
                {
                    return null;
                }

                if (jsonResponse.Length <= 0)
                {
                    return null;
                }

                return DbGeography.PointFromText(String.Format("POINT({1} {0})",
                    jsonResponse[0].Latitude,
                    jsonResponse[0].Longitude),
                    AppConfiguration.CoordinateSystemId);
            }
        }

        public async Task<DbGeography> GeocodeAsync(string address)
        {
            if (String.IsNullOrEmpty(address) || String.IsNullOrWhiteSpace(address))
            {
                return null;
            }

            var request = WebRequest.Create(String.Format(ForwardGeocodeUrl, address));

            using (var response = await request.GetResponseAsync() as HttpWebResponse)
            {
                if ((response == null) || (response.StatusCode != HttpStatusCode.OK))
                {
                    return null;
                }

                var jsonSerializer = new DataContractJsonSerializer(typeof(GeocodeResponse[]));
                var stream = response.GetResponseStream();

                if (stream == null)
                {
                    return null;
                }

                var objectResponse = jsonSerializer.ReadObject(stream);
                var jsonResponse = objectResponse as GeocodeResponse[];

                if (jsonResponse == null)
                {
                    return null;
                }

                if (jsonResponse.Length <= 0)
                {
                    return null;
                }

                return DbGeography.PointFromText(String.Format("POINT({1} {0})",
                    jsonResponse[0].Latitude,
                    jsonResponse[0].Longitude),
                    AppConfiguration.CoordinateSystemId);
            }
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
            if (latitude < AppConfiguration.MinimumLatitude | latitude > AppConfiguration.MaximumLatitude)
            {
                return null;
            }

            if (longitude < AppConfiguration.MinimumLongitude | longitude > AppConfiguration.MaximumLongitude)
            {
                return null;
            }

            var request = WebRequest.Create(String.Format(ReverseGeocodeUrl, latitude, longitude));

            using (var response = request.GetResponse() as HttpWebResponse)
            {
                if ((response == null) || (response.StatusCode != HttpStatusCode.OK))
                {
                    return null;
                }

                var jsonSerializer = new DataContractJsonSerializer(typeof(GeocodeResponse));
                var stream = response.GetResponseStream();

                if (stream == null)
                {
                    return null;
                }

                var objectResponse = jsonSerializer.ReadObject(stream);
                var jsonResponse = objectResponse as GeocodeResponse;

                if (jsonResponse == null || jsonResponse.Address == null)
                {
                    return null;
                }

                var stringBuilder = new StringBuilder();

                if (!String.IsNullOrEmpty(jsonResponse.Address.HouseNumber))
                {
                    stringBuilder.Append(jsonResponse.Address.HouseNumber);
                    stringBuilder.Append(" ");
                }

                if (!String.IsNullOrEmpty(jsonResponse.Address.Road))
                {
                    stringBuilder.Append(jsonResponse.Address.Road);
                    stringBuilder.Append(", ");
                }

                if (!String.IsNullOrEmpty(jsonResponse.Address.City))
                {
                    stringBuilder.Append(jsonResponse.Address.City);
                    stringBuilder.Append(", ");
                }

                if (!String.IsNullOrEmpty(jsonResponse.Address.State))
                {
                    stringBuilder.Append(jsonResponse.Address.State);
                    stringBuilder.Append(", ");
                }

                if (!String.IsNullOrEmpty(jsonResponse.Address.PostalCode))
                {
                    stringBuilder.Append(jsonResponse.Address.PostalCode);
                    stringBuilder.Append(", ");
                }

                if (!String.IsNullOrEmpty(jsonResponse.Address.Country))
                {
                    stringBuilder.Append(jsonResponse.Address.Country);
                }

                var result = stringBuilder.ToString();

                if (String.IsNullOrEmpty(result) || String.IsNullOrWhiteSpace(result))
                {
                    return null;
                }

                return result;
            }
        }

        public async Task<string> ReverseGeocodeAsync(double latitude, double longitude)
        {
            if (latitude < AppConfiguration.MinimumLatitude | latitude > AppConfiguration.MaximumLatitude)
            {
                return null;
            }

            if (longitude < AppConfiguration.MinimumLongitude | longitude > AppConfiguration.MaximumLongitude)
            {
                return null;
            }

            var request = WebRequest.Create(String.Format(ReverseGeocodeUrl, latitude, longitude));

            using (var response = await request.GetResponseAsync() as HttpWebResponse)
            {
                if ((response == null) || (response.StatusCode != HttpStatusCode.OK))
                {
                    return null;
                }

                var jsonSerializer = new DataContractJsonSerializer(typeof(GeocodeResponse));
                var stream = response.GetResponseStream();

                if (stream == null)
                {
                    return null;
                }

                var objectResponse = jsonSerializer.ReadObject(stream);
                var jsonResponse = objectResponse as GeocodeResponse;

                if (jsonResponse == null || jsonResponse.Address == null)
                {
                    return null;
                }

                var stringBuilder = new StringBuilder();

                if (!String.IsNullOrEmpty(jsonResponse.Address.HouseNumber))
                {
                    stringBuilder.Append(jsonResponse.Address.HouseNumber);
                    stringBuilder.Append(" ");
                }

                if (!String.IsNullOrEmpty(jsonResponse.Address.Road))
                {
                    stringBuilder.Append(jsonResponse.Address.Road);
                    stringBuilder.Append(", ");
                }

                if (!String.IsNullOrEmpty(jsonResponse.Address.City))
                {
                    stringBuilder.Append(jsonResponse.Address.City);
                    stringBuilder.Append(", ");
                }

                if (!String.IsNullOrEmpty(jsonResponse.Address.State))
                {
                    stringBuilder.Append(jsonResponse.Address.State);
                    stringBuilder.Append(", ");
                }

                if (!String.IsNullOrEmpty(jsonResponse.Address.PostalCode))
                {
                    stringBuilder.Append(jsonResponse.Address.PostalCode);
                    stringBuilder.Append(", ");
                }

                if (!String.IsNullOrEmpty(jsonResponse.Address.Country))
                {
                    stringBuilder.Append(jsonResponse.Address.Country);
                }

                var result = stringBuilder.ToString();

                if (String.IsNullOrEmpty(result) || String.IsNullOrWhiteSpace(result))
                {
                    return null;
                }

                return result;
            }
        }

        [DataContract]
        private class GeocodeResponse
        {
            [DataMember(Name = "place_id")]
            public string PlaceId { get; set; }

            [DataMember(Name = "licence")]
            public string Licence { get; set; }

            [DataMember(Name = "osm_type")]
            public string OsmType { get; set; }

            [DataMember(Name = "lat")]
            public string Latitude { get; set; }

            [DataMember(Name = "lon")]
            public string Longitude { get; set; }

            [DataMember(Name = "display_name")]
            public string DisplayName { get; set; }

            [DataMember(Name = "address")]
            public Address Address { get; set; }
        }

        [DataContract]
        private class Address
        {
            [DataMember(Name = "house_number")]
            public string HouseNumber { get; set; }

            [DataMember(Name = "road")]
            public string Road { get; set; }

            [DataMember(Name = "neighbourhood")]
            public string Neighbourhood { get; set; }

            [DataMember(Name = "suburb")]
            public string Suburb { get; set; }

            [DataMember(Name = "city")]
            public string City { get; set; }

            [DataMember(Name = "county")]
            public string County { get; set; }

            [DataMember(Name = "state")]
            public string State { get; set; }

            [DataMember(Name = "postcode")]
            public string PostalCode { get; set; }

            [DataMember(Name = "country")]
            public string Country { get; set; }

            [DataMember(Name = "country_code")]
            public string CountryCode { get; set; }
        }
    }
}
