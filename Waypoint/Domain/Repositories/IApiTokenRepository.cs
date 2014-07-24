using System;

using Domain.Models;

namespace Domain.Repositories
{
    public interface IApiTokenRepository : IRepository
    {
        ApiToken Get(string token);

        ApiToken[] Where(Func<ApiToken, bool> filter);

        ApiToken Add(ApiToken apiToken);

        bool Update(string id, ApiToken apiToken);

        bool Remove(string id);
    }
}
