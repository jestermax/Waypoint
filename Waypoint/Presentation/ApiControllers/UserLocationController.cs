using System;
using System.Data.Entity.Spatial;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

using Domain.Configuration;
using Domain.Dto.Inbound;
using Domain.Dto.Outbound;
using Domain.Geocoding;
using Domain.Models;
using Domain.Repositories;

namespace Presentation.ApiControllers
{
    public class UserLocationController : AuthenticatedApiController
    {
        private readonly IUserLocationRepository _userLocationRepository;
        private readonly IPlaceRepository _placeRepository;

        public UserLocationController(IUserLocationRepository userLocationRepository, IPlaceRepository placeRepository, IUserRepository userRepository, IApiTokenRepository apiTokenRepository)
            : base(userRepository, apiTokenRepository)
        {
            _userLocationRepository = userLocationRepository;
            _placeRepository = placeRepository;
        }

        [System.Web.Mvc.HttpGet]
        public async Task<UserLocationDto[]> Get()
        {
            var userLocations = await _userLocationRepository.Where(u => u.User.Id.Equals(ApplicationUser.Id));

            var result = new UserLocationDto[userLocations.Length];

            for (var i = 0; i < result.Length; i++)
            {
                result[i] = new UserLocationDto(userLocations[i]);
            }

            return result;
        }

        [System.Web.Mvc.HttpGet]
        public async Task<UserLocationDto> Get(string id)
        {
            var userLocation = await _userLocationRepository.Get(id);

            if (userLocation == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            if (!userLocation.User.Account.Id.Equals(ApplicationUser.Account.Id))
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            return new UserLocationDto(userLocation);
        }

        [System.Web.Mvc.HttpPost]
        public async Task<UserLocationDto> Post(LocationUpdateDto locationUpdate)
        {
            if (locationUpdate == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            if ((Math.Abs(locationUpdate.latitude) < Double.Epsilon) | (Math.Abs(locationUpdate.longitude) < Double.Epsilon))
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            if (locationUpdate.latitude < AppConfiguration.MinimumLatitude | locationUpdate.latitude > AppConfiguration.MaximumLatitude)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            if (locationUpdate.longitude < AppConfiguration.MinimumLongitude | locationUpdate.longitude > AppConfiguration.MaximumLongitude)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            DateTime locationUpdateTimeStamp;

            if (!DateTime.TryParse(locationUpdate.timestamp, out locationUpdateTimeStamp))
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            if (locationUpdate.accuracy <= 0)
            {
                locationUpdate.accuracy = AppConfiguration.MinimumLocationAccuracy;
            }

            if (locationUpdate.speed < AppConfiguration.MinimumSpeed)
            {
                locationUpdate.speed = AppConfiguration.MinimumSpeed;
            }

            if (locationUpdate.speed > AppConfiguration.MaximumSpeed)
            {
                locationUpdate.speed = AppConfiguration.MaximumSpeed;
            }

            var geocoder = new AggregatedGeocoder();

            var address = await geocoder.ReverseGeocodeAsync(locationUpdate.latitude, locationUpdate.longitude);

            if (String.IsNullOrEmpty(address) || String.IsNullOrWhiteSpace(address))
            {
                address = AppConfiguration.AddressUnavailableMessage;
            }

            if (address.Length > AppConfiguration.UserLocationAddressMaximumLength)
            {
                address = String.Format("{0}...", address.Substring(0, AppConfiguration.UserLocationAddressMaximumLength - 3));
            }

            var userLocation = await _userLocationRepository.Add(new UserLocation
            {
                Id = Guid.NewGuid().ToString(),
                User = ApplicationUser,
                Accuracy = locationUpdate.accuracy,
                Address = address,
                Speed = locationUpdate.speed,
                DateReceived = DateTime.UtcNow,
                DateSent = DateTime.Parse(locationUpdate.timestamp).ToUniversalTime(),
                Location = DbGeography.PointFromText(String.Format("POINT({1} {0})",
                    locationUpdate.latitude,
                    locationUpdate.longitude),
                    AppConfiguration.CoordinateSystemId)
            });

            //if (userLocation == null)
            //{
            //    throw new HttpResponseException(HttpStatusCode.InternalServerError);
            //}

            //if (!ApplicationUser.UserLocationCache.PreviousLocationId.HasValue || !UserProfile.UserLocationCache.CurrentLocationId.HasValue)
            //{
            //    return new UserLocationDto(userLocation);
            //}

            //var places = await _placeRepository.Where(p => p.AccountId.Equals(UserProfile.AccountId));

            //foreach (var place in places)
            //{
            //    if (UserProfile.UserLocationCache.CurrentLocation.Location.Buffer(userLocation.Accuracy).Intersects(place.Boundary) &&
            //        !UserProfile.UserLocationCache.PreviousLocation.Location.Buffer(userLocation.Accuracy).Intersects(place.Boundary))
            //    {
            //        RecordEvent(new Event
            //        {
            //            Id = Guid.NewGuid(),
            //            UserId = UserProfile.UserId,
            //            EventTypeId = KnownId.EventTypeArrivedAtPlace,
            //            DateCreated = userLocation.DateSent,
            //            Description = String.Format("{0} {1} has arrived at {2}", UserProfile.FirstName, UserProfile.LastName, place.Name)
            //        });
            //    }

            //    if (!UserProfile.UserLocationCache.CurrentLocation.Location.Buffer(userLocation.Accuracy).Intersects(place.Boundary) &&
            //        UserProfile.UserLocationCache.PreviousLocation.Location.Buffer(userLocation.Accuracy).Intersects(place.Boundary))
            //    {
            //        RecordEvent(new Event
            //        {
            //            Id = Guid.NewGuid(),
            //            UserId = UserProfile.UserId,
            //            EventTypeId = KnownId.EventTypeLeftPlace,
            //            DateCreated = userLocation.DateSent,
            //            Description = String.Format("{0} {1} has left {2}", UserProfile.FirstName, UserProfile.LastName, place.Name)
            //        });
            //    }
            //}

            return new UserLocationDto(userLocation);
        }
    }
}
