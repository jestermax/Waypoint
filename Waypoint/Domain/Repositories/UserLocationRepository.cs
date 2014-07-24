using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

using Domain.Configuration;
using Domain.Models;

namespace Domain.Repositories
{
    public class UserLocationRepository : BaseRepository, IUserLocationRepository
    {
        public UserLocationRepository(ApplicationDbContext context)
            : base(context)
        { }

        public async Task<UserLocation> Get(string id)
        {
            return await Context.UserLocations.FindAsync(id);
        }

        public async Task<UserLocation[]> Where(Func<UserLocation, bool> filter)
        {
            return await Context.UserLocations
                .Where(filter)
                .OrderByDescending(u => u.DateSent)
                .AsQueryable()
                .ToArrayAsync();
        }

        public async Task<UserLocation> Add(UserLocation userLocation)
        {
            Context.UserLocations.Add(userLocation);
            await Context.SaveChangesAsync();

            return userLocation;
        }

        public Task<bool> Update(string id, UserLocation userLocation)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Remove(string id)
        {
            var userLocation = await Context.UserLocations.FindAsync(id);

            if (userLocation == null)
            {
                return false;
            }

            Context.UserLocations.Remove(userLocation);
            await Context.SaveChangesAsync();

            return true;
        }
    }
}
