using System.Data.Entity.Spatial;
using System.Threading.Tasks;

namespace Domain.Geocoding
{
    public interface IGeocoder
    {
        DbGeography Geocode(string address);

        Task<DbGeography> GeocodeAsync(string address);

        string ReverseGeocode(DbGeography location);

        Task<string> ReverseGeocodeAsync(DbGeography location);

        string ReverseGeocode(double latitude, double longitude);

        Task<string> ReverseGeocodeAsync(double latitude, double longitude);
    }
}
