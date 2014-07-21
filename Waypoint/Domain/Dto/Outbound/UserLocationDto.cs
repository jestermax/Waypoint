using Domain.Models;

namespace Domain.Dto.Outbound
{
    public class UserLocationDto
    {
        public UserLocationDto(UserLocation userLocation)
        {
            id = userLocation.Id;
            userId = userLocation.User.Id;
            latitude = userLocation.Location.Latitude.HasValue ? userLocation.Location.Latitude.Value : 0;
            longitude = userLocation.Location.Longitude.HasValue ? userLocation.Location.Longitude.Value : 0;
            address = userLocation.Address;
            accuracy = userLocation.Accuracy;
            speed = userLocation.Speed;
            dateSent = userLocation.DateSent.ToString("s");
            dateReceived = userLocation.DateReceived.ToString("s");
        }

        public string id { get; set; }
        public string userId { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public string address { get; set; }
        public double accuracy { get; set; }
        public double speed { get; set; }
        public string dateSent { get; set; }
        public string dateReceived { get; set; }
    }
}
