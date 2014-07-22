using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

using Domain.Database;
using Domain.Models;

namespace Domain.Repositories
{
    public class PlaceRepository : IPlaceRepository
    {
        private readonly ApplicationDbContext _context = ApplicationDbContext.Create();

        public async Task<Place> Get(string id)
        {
            return await _context.Places.FindAsync(id);
        }

        public async Task<Place[]> Where(Func<Place, bool> filter)
        {
            return await _context.Places
                .Where(filter)
                .OrderBy(p => p.Name)
                .AsQueryable()
                .ToArrayAsync();
        }

        public async Task<Place> Add(Place place)
        {
            _context.Places.Add(place);
            await _context.SaveChangesAsync();

            return place;
        }

        public async Task<bool> Update(string id, Place place)
        {
            var existing = await _context.Places.FindAsync(id);

            if (existing == null)
            {
                return false;
            }

            existing.Boundary = place.Boundary;
            existing.DateModified = DateTime.UtcNow;
            existing.MaximumLatitude = place.MaximumLatitude;
            existing.MaximumLongitude = place.MaximumLongitude;
            existing.MinimumLatitude = place.MinimumLatitude;
            existing.MinimumLongitude = place.MinimumLongitude;
            existing.Name = place.Name;

            _context.Entry(existing).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Remove(string id)
        {
            var place = await _context.Places.FindAsync(id);

            if (place == null)
            {
                return false;
            }

            _context.Places.Remove(place);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
