using System;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Repositories
{
    public class UserLocationRepository : IUserLocationRepository
    {
        public Task<UserLocation> Get(string id)
        {
            throw new NotImplementedException();
        }

        public Task<UserLocation[]> Where(Func<UserLocation, bool> filter)
        {
            throw new NotImplementedException();
        }

        public Task<UserLocation> Add(UserLocation userLocation)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(string id, UserLocation userLocation)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Remove(string id)
        {
            throw new NotImplementedException();
        }
    }
}
