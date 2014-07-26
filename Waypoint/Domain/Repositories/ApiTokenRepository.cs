using System;
using System.Data.Entity;
using System.Linq;

using Domain.Configuration;
using Domain.Models;

namespace Domain.Repositories
{
    public class ApiTokenRepository : BaseRepository, IApiTokenRepository
    {
        public ApiTokenRepository(ApplicationDbContext context)
            : base(context)
        { }

        public ApiToken Get(string token)
        {
            return Context.ApiTokens.First(a => a.Token.Equals(token));
        }

        public ApiToken[] Where(Func<ApiToken, bool> filter)
        {
            return Context.ApiTokens
                .Where(filter)
                .OrderBy(a => a.DateIssued)
                .ToArray();
        }

        public ApiToken Add(ApiToken apiToken)
        {
            Context.ApiTokens.Add(apiToken);
            Context.SaveChanges();

            return apiToken;
        }

        public bool Update(string id, ApiToken apiToken)
        {
            var existing = Context.ApiTokens.Find(id);

            if (existing == null)
            {
                return false;
            }

            existing.DateExpires = apiToken.DateExpires;
            existing.DateIssued = apiToken.DateIssued;
            existing.Token = apiToken.Token;
            existing.User = apiToken.User;

            Context.Entry(existing).State = EntityState.Modified;
            Context.SaveChanges();
            
            return true;
        }

        public bool Remove(string id)
        {
            var apiToken = Context.ApiTokens.Find(id);

            if (apiToken == null)
            {
                return false;
            }

            Context.ApiTokens.Remove(apiToken);
            Context.SaveChanges();

            return true;
        }
    }
}
