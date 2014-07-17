using System;
using System.Threading.Tasks;
using System.Data.Entity.Spatial;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Domain;
using Domain.Geocoding;
using Domain.Helpers;

namespace Tests.Geocoding
{
    [TestClass]
    public class NominatimGeocoderTest
    {
        private readonly string forwardGeocodeRequest = "CN Tower, Toronto, ON";
        private readonly string reverseGeocodeResponse = "2590 Armour Crescent, Burlington, Ontario, L7M4A9, Canada";

        [TestMethod]
        public void ForwardGeocodeLoad()
        {
            var geocoder = new NominatimGeocoder();

            for (var i = 0; i < 10; i++)
            {
                var result = geocoder.Geocode(forwardGeocodeRequest);

                Assert.IsNotNull(result);
                Assert.AreEqual(43.64256, Math.Round(result.Latitude.Value, 5));
                Assert.AreEqual(-79.38709, Math.Round(result.Longitude.Value, 5));
                Assert.AreEqual(0, result.Length);
                Assert.AreEqual("Point", result.SpatialTypeName);
                Assert.AreEqual(1, result.ElementCount);
            }
        }

        [TestMethod]
        public void ForwardGeocodeLoadAsync()
        {
            var geocoder = new NominatimGeocoder();

            Parallel.For(0, 10, async i =>
            {
                var result = await geocoder.GeocodeAsync(forwardGeocodeRequest);

                Assert.IsNotNull(result);
                Assert.AreEqual(43.64256, Math.Round(result.Latitude.Value, 5));
                Assert.AreEqual(-79.38709, Math.Round(result.Longitude.Value, 5));
                Assert.AreEqual(0, result.Length);
                Assert.AreEqual("Point", result.SpatialTypeName);
                Assert.AreEqual(1, result.ElementCount);
            });
        }

        [TestMethod]
        public void ForwardGeocodeValidAddress()
        {
            var geocoder = new NominatimGeocoder();

            var result = geocoder.Geocode(forwardGeocodeRequest);

            Assert.IsNotNull(result);
            Assert.AreEqual(43.64256, Math.Round(result.Latitude.Value, 5));
            Assert.AreEqual(-79.38709, Math.Round(result.Longitude.Value, 5));
            Assert.AreEqual(0, result.Length);
            Assert.AreEqual("Point", result.SpatialTypeName);
            Assert.AreEqual(1, result.ElementCount);
        }

        [TestMethod]
        public async void ForwardGeocodeValidAddressAsync()
        {
            var geocoder = new NominatimGeocoder();

            var result = await geocoder.GeocodeAsync(forwardGeocodeRequest);

            Assert.IsNotNull(result);
            Assert.AreEqual(43.64256, Math.Round(result.Latitude.Value, 5));
            Assert.AreEqual(-79.38709, Math.Round(result.Longitude.Value, 5));
            Assert.AreEqual(0, result.Length);
            Assert.AreEqual("Point", result.SpatialTypeName);
            Assert.AreEqual(1, result.ElementCount);
        }

        [TestMethod]
        public void ForwardGeocodeInvalidAddress()
        {
            var geocoder = new NominatimGeocoder();
            
            var result = geocoder.Geocode(RandomStringGenerator.Create(100));

            Assert.IsNull(result);
        }

        [TestMethod]
        public void ForwardGeocodeBlankAddress()
        {
            var geocoder = new NominatimGeocoder();

            var result = geocoder.Geocode(String.Empty);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void ForwardGeocodeWhitespaceAddress()
        {
            var geocoder = new NominatimGeocoder();

            var result = geocoder.Geocode("     ");

            Assert.IsNull(result);
        }

        [TestMethod]
        public void ForwardGeocodeNullAddress()
        {
            var geocoder = new NominatimGeocoder();

            var result = geocoder.Geocode(null);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void ReverseGeocodeValidLatitudeAndLongitude()
        {
            var geocoder = new NominatimGeocoder();

            var result = geocoder.ReverseGeocode(43.4046593, -79.8091507);

            Assert.IsNotNull(result);
            Assert.AreEqual(reverseGeocodeResponse, result);
        }

        [TestMethod]
        public void ReverseGeocodeValidDbGeography()
        {
            var geocoder = new NominatimGeocoder();

            var result = geocoder.ReverseGeocode(DbGeography.PointFromText(
                String.Format("POINT({1} {0})", 43.4046593, -79.8091507),
                AppConfiguration.CoordinateSystemId));

            Assert.IsNotNull(result);
            Assert.AreEqual(reverseGeocodeResponse, result);
        }

        [TestMethod]
        public async void ReverseGeocodeValidLatitudeAndLongitudeAsync()
        {
            var geocoder = new NominatimGeocoder();

            var result = await geocoder.ReverseGeocodeAsync(43.4046593, -79.8091507);

            Assert.IsNotNull(result);
            Assert.AreEqual(reverseGeocodeResponse, result);
        }

        [TestMethod]
        public async void ReverseGeocodeValidDbGeographyAsync()
        {
            var geocoder = new NominatimGeocoder();

            var result = await geocoder.ReverseGeocodeAsync(DbGeography.PointFromText(
                String.Format("POINT({1} {0})", 43.4046593, -79.8091507),
                AppConfiguration.CoordinateSystemId));

            Assert.IsNotNull(result);
            Assert.AreEqual(reverseGeocodeResponse, result);
        }

        [TestMethod]
        public void ReverseGeocodeInvalidLatitude()
        {
            var geocoder = new NominatimGeocoder();

            var result = geocoder.ReverseGeocode(-91, 0);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void ReverseGeocodeInvalidLongitude()
        {
            var geocoder = new NominatimGeocoder();

            var result = geocoder.ReverseGeocode(0, -181);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void ReverseGeocodeNullDbGeography()
        {
            var geocoder = new NominatimGeocoder();

            var result = geocoder.ReverseGeocode(null);

            Assert.IsNull(result);
        }
    }
}
