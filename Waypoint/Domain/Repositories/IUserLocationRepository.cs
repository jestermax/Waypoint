using System;
using System.Threading.Tasks;

using Domain.Models;

namespace Domain.Repositories
{
    public interface IUserLocationRepository
    {
        Task<UserLocation> Get(string id);

        Task<UserLocation[]> Where(Func<UserLocation, bool> filter);

        Task<UserLocation> Add(UserLocation userLocation);

        Task<bool> Update(string id, UserLocation userLocation);

        Task<bool> Remove(string id);
    }
}
