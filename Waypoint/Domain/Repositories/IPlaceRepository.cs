using System;
using System.Threading.Tasks;

using Domain.Models;

namespace Domain.Repositories
{
    public interface IPlaceRepository
    {
        Task<Place> Get(string id);

        Task<Place[]> Where(Func<Place, bool> filter);

        Task<Place> Add(Place place);

        Task<bool> Update(string id, Place place);

        Task<bool> Remove(string id);
    }
}
