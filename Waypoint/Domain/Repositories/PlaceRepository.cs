using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

using Domain.Configuration;
using Domain.Models;

namespace Domain.Repositories
{
    public class PlaceRepository : BaseRepository, IPlaceRepository
    {
        public PlaceRepository(ApplicationDbContext context)
            : base(context)
        { }

        public async Task<Place> Get(string id)
        {
            return await Context.Places.FindAsync(id);
        }

        public async Task<Place[]> Where(Func<Place, bool> filter)
        {
            return await Context.Places
                .Where(filter)
                .OrderBy(p => p.Name)
                .AsQueryable()
                .ToArrayAsync();
        }

        public async Task<Place> Add(Place place)
        {
            Context.Places.Add(place);
            await Context.SaveChangesAsync();

            return place;
        }

        public async Task<bool> Update(string id, Place place)
        {
            var existing = await Context.Places.FindAsync(id);

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

            Context.Entry(existing).State = EntityState.Modified;
            await Context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Remove(string id)
        {
            var place = await Context.Places.FindAsync(id);

            if (place == null)
            {
                return false;
            }

            Context.Places.Remove(place);
            await Context.SaveChangesAsync();

            return true;
        }
    }
}
