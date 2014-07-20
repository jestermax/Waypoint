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
    public class MapQuestGeocoder : IGeocoder
    {
        private const string ForwardGeocodeUrl =
            "http://open.mapquestapi.com/geocoding/v1/address?key={0}&location={1}&maxResults=1&thumbMaps=false";

        private const string ReverseGeocodeUrl =
            "http://open.mapquestapi.com/geocoding/v1/reverse?key={0}&location={1},{2}&maxResults=1&thumbMaps=false";

        public DbGeography Geocode(string address)
        {
            if (String.IsNullOrEmpty(address) || String.IsNullOrWhiteSpace(address))
            {
                return null;
            }

            var request = WebRequest.Create(String.Format(ForwardGeocodeUrl, AppConfiguration.MapQuestApiKey, address));

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

                if (jsonResponse == null)
                {
                    return null;
                }

                if (jsonResponse.ResponseInfo.StatusCode != 0)
                {
                    return null;
                }

                if (jsonResponse.Results[0].Locations.Length <= 0)
                {
                    return null;
                }

                return DbGeography.PointFromText(String.Format("POINT({1} {0})",
                    jsonResponse.Results[0].Locations[0].LatLng.Latitude,
                    jsonResponse.Results[0].Locations[0].LatLng.Longitude),
                    AppConfiguration.CoordinateSystemId);
            }
        }

        public async Task<DbGeography> GeocodeAsync(string address)
        {
            if (String.IsNullOrEmpty(address) || String.IsNullOrWhiteSpace(address))
            {
                return null;
            }

            var request = WebRequest.Create(String.Format(ForwardGeocodeUrl, AppConfiguration.MapQuestApiKey, address));

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

                if (jsonResponse == null)
                {
                    return null;
                }

                if (jsonResponse.ResponseInfo.StatusCode != 0)
                {
                    return null;
                }

                if (jsonResponse.Results[0].Locations.Length <= 0)
                {
                    return null;
                }

                return DbGeography.PointFromText(String.Format("POINT({1} {0})",
                    jsonResponse.Results[0].Locations[0].LatLng.Latitude,
                    jsonResponse.Results[0].Locations[0].LatLng.Longitude),
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

            var request = WebRequest.Create(String.Format(ReverseGeocodeUrl, AppConfiguration.MapQuestApiKey, latitude, longitude));

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

                if (jsonResponse == null)
                {
                    return null;
                }

                if (jsonResponse.ResponseInfo.StatusCode != 0)
                {
                    return null;
                }

                if (jsonResponse.Results[0].Locations.Length <= 0)
                {
                    return null;
                }

                var stringBuilder = new StringBuilder();

                if (!String.IsNullOrEmpty(jsonResponse.Results[0].Locations[0].Street))
                {
                    stringBuilder.Append(jsonResponse.Results[0].Locations[0].Street);
                    stringBuilder.Append(", ");
                }

                if (!String.IsNullOrEmpty(jsonResponse.Results[0].Locations[0].AdminArea5))
                {
                    stringBuilder.Append(jsonResponse.Results[0].Locations[0].AdminArea5);
                    stringBuilder.Append(", ");
                }

                if (!String.IsNullOrEmpty(jsonResponse.Results[0].Locations[0].AdminArea3))
                {
                    stringBuilder.Append(jsonResponse.Results[0].Locations[0].AdminArea3);
                    stringBuilder.Append(", ");
                }

                if (!String.IsNullOrEmpty(jsonResponse.Results[0].Locations[0].PostalCode))
                {
                    stringBuilder.Append(jsonResponse.Results[0].Locations[0].PostalCode);
                    stringBuilder.Append(", ");
                }

                if (!String.IsNullOrEmpty(jsonResponse.Results[0].Locations[0].AdminArea1))
                {
                    stringBuilder.Append(jsonResponse.Results[0].Locations[0].AdminArea1);
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

            var request = WebRequest.Create(String.Format(ReverseGeocodeUrl, AppConfiguration.MapQuestApiKey, latitude, longitude));

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

                if (jsonResponse == null)
                {
                    return null;
                }

                if (jsonResponse.ResponseInfo.StatusCode != 0)
                {
                    return null;
                }

                if (jsonResponse.Results[0].Locations.Length <= 0)
                {
                    return null;
                }

                var stringBuilder = new StringBuilder();

                if (!String.IsNullOrEmpty(jsonResponse.Results[0].Locations[0].Street))
                {
                    stringBuilder.Append(jsonResponse.Results[0].Locations[0].Street);
                    stringBuilder.Append(", ");
                }

                if (!String.IsNullOrEmpty(jsonResponse.Results[0].Locations[0].AdminArea5))
                {
                    stringBuilder.Append(jsonResponse.Results[0].Locations[0].AdminArea5);
                    stringBuilder.Append(", ");
                }

                if (!String.IsNullOrEmpty(jsonResponse.Results[0].Locations[0].AdminArea3))
                {
                    stringBuilder.Append(jsonResponse.Results[0].Locations[0].AdminArea3);
                    stringBuilder.Append(", ");
                }

                if (!String.IsNullOrEmpty(jsonResponse.Results[0].Locations[0].PostalCode))
                {
                    stringBuilder.Append(jsonResponse.Results[0].Locations[0].PostalCode);
                    stringBuilder.Append(", ");
                }

                if (!String.IsNullOrEmpty(jsonResponse.Results[0].Locations[0].AdminArea1))
                {
                    stringBuilder.Append(jsonResponse.Results[0].Locations[0].AdminArea1);
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
            [DataMember(Name = "results")]
            public Result[] Results { get; set; }

            [DataMember(Name = "options")]
            public RequestOptions RequestOptions { get; set; }

            [DataMember(Name = "info")]
            public ResponseInfo ResponseInfo { get; set; }
        }

        [DataContract]
        private class Result
        {
            [DataMember(Name = "locations")]
            public Location[] Locations { get; set; }
        }

        [DataContract]
        private class Location
        {
            [DataMember(Name = "latLng")]
            public LatLng LatLng { get; set; }

            [DataMember(Name = "displayLatLng")]
            public LatLng DisplayLng { get; set; }

            [DataMember(Name = "adminArea1")]
            public string AdminArea1 { get; set; }

            [DataMember(Name = "adminArea2")]
            public string AdminArea2 { get; set; }

            [DataMember(Name = "adminArea3")]
            public string AdminArea3 { get; set; }

            [DataMember(Name = "adminArea4")]
            public string AdminArea4 { get; set; }

            [DataMember(Name = "adminArea5")]
            public string AdminArea5 { get; set; }

            [DataMember(Name = "adminArea1Type")]
            public string AdminArea1Type { get; set; }

            [DataMember(Name = "adminArea2Type")]
            public string AdminArea2Type { get; set; }

            [DataMember(Name = "adminArea3Type")]
            public string AdminArea3Type { get; set; }

            [DataMember(Name = "adminArea4Type")]
            public string AdminArea4Type { get; set; }

            [DataMember(Name = "adminArea5Type")]
            public string AdminArea5Type { get; set; }

            [DataMember(Name = "type")]
            public string Type { get; set; }

            [DataMember(Name = "linkId")]
            public int LinkId { get; set; }

            [DataMember(Name = "postalCode")]
            public string PostalCode { get; set; }

            [DataMember(Name = "street")]
            public string Street { get; set; }

            [DataMember(Name = "sideOfStreet")]
            public string SideOfStreet { get; set; }

            [DataMember(Name = "dragPoint")]
            public bool DragPoint { get; set; }

            [DataMember(Name = "geocodeQuality")]
            public string GeocodeQuality { get; set; }

            [DataMember(Name = "geocodeQualityCode")]
            public string GeocodeQualityCode { get; set; }
        }

        [DataContract]
        private class LatLng
        {
            [DataMember(Name = "lat")]
            public double Latitude { get; set; }

            [DataMember(Name = "lng")]
            public double Longitude { get; set; }
        }

        [DataContract]
        private class RequestOptions
        {
            [DataMember(Name = "ignoreLatLngInput")]
            public bool IgnoreLatLngInput { get; set; }

            [DataMember(Name = "maxResults")]
            public int MaxResults { get; set; }

            [DataMember(Name = "thumbMaps")]
            public bool ThumbMaps { get; set; }
        }

        [DataContract]
        private class ResponseInfo
        {
            [DataMember(Name = "copyright")]
            public Copyright Copyright { get; set; }

            [DataMember(Name = "statuscode")]
            public int StatusCode { get; set; }
        }

        [DataContract]
        private class Copyright
        {
            [DataMember(Name = "text")]
            public string Text { get; set; }

            [DataMember(Name = "imageUrl")]
            public string ImageUrl { get; set; }

            [DataMember(Name = "imageAltText")]
            public string ImageAltText { get; set; }
        }
    }
}
