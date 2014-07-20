using System;

using Domain.Models;

namespace Domain.Repositories
{
    public interface IUserRepository
    {
        ApplicationUser Get(string id);

        ApplicationUser[] Where(Func<ApplicationUser, bool> filter);

        ApplicationUser Add(ApplicationUser user);

        bool Update(string id, ApplicationUser userProfile);

        bool Remove(string id);
    }
}
