using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

using Domain.Database;
using Domain.Models;

namespace Domain.Repositories
{
    public class UserLocationRepository : IUserLocationRepository
    {
        private readonly ApplicationDbContext _context = ApplicationDbContext.Create();

        public async Task<UserLocation> Get(string id)
        {
            return await _context.UserLocations.FindAsync(id);
        }

        public async Task<UserLocation[]> Where(Func<UserLocation, bool> filter)
        {
            return await _context.UserLocations
                .Where(filter)
                .OrderByDescending(u => u.DateSent)
                .AsQueryable()
                .ToArrayAsync();
        }

        public async Task<UserLocation> Add(UserLocation userLocation)
        {
            _context.UserLocations.Add(userLocation);
            await _context.SaveChangesAsync();

            return userLocation;
        }

        public Task<bool> Update(string id, UserLocation userLocation)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Remove(string id)
        {
            var userLocation = await _context.UserLocations.FindAsync(id);

            if (userLocation == null)
            {
                return false;
            }

            _context.UserLocations.Remove(userLocation);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
