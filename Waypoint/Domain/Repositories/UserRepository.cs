using System;

using Domain.Models;

namespace Domain.Repositories
{
    public class UserRepository : IUserRepository
    {
        public ApplicationUser Get(string id)
        {
            throw new NotImplementedException();
        }

        public ApplicationUser[] Where(Func<ApplicationUser, bool> filter)
        {
            throw new NotImplementedException();
        }

        public ApplicationUser Add(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public bool Update(string id, ApplicationUser userProfile)
        {
            throw new NotImplementedException();
        }

        public bool Remove(string id)
        {
            throw new NotImplementedException();
        }
    }
}
