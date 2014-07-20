using System;
using System.Data.Entity;
using System.Linq;
using Domain.Database;
using Domain.Models;

namespace Domain.Repositories
{
    public class ApiTokenRepository : IApiTokenRepository
    {
        private readonly ApplicationDbContext _context = ApplicationDbContext.Create();

        public ApiToken Get(string token)
        {
            return _context.ApiTokens.FirstOrDefault(a => a.Token.Equals(token));
        }

        public ApiToken[] Where(Func<ApiToken, bool> filter)
        {
            return _context.ApiTokens.Where(filter).OrderBy(a => a.DateIssued).ToArray();
        }

        public ApiToken Add(ApiToken apiToken)
        {
            _context.ApiTokens.Add(apiToken);
            _context.SaveChanges();

            return apiToken;
        }

        public bool Update(string id, ApiToken apiToken)
        {
            var existing = _context.ApiTokens.Find(id);

            if (existing == null)
            {
                return false;
            }

            existing.DateExpires = apiToken.DateExpires;
            existing.DateIssued = apiToken.DateIssued;
            existing.Token = apiToken.Token;

            _context.Entry(existing).State = EntityState.Modified;
            _context.SaveChanges();

            return true;
        }

        public bool Remove(string id)
        {
            var apiToken = _context.ApiTokens.Find(id);

            if (apiToken == null)
            {
                return false;
            }

            _context.ApiTokens.Remove(apiToken);
            _context.SaveChanges();

            return true;
        }
    }
}
