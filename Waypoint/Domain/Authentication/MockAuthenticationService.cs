using System;
using System.Threading.Tasks;

using Domain.Configuration;

namespace Domain.Authentication
{
    public class MockAuthenticationService : IAuthenticationService
    {
        public bool Login(string email, string password)
        {
            return email.Equals(AppConfiguration.UnitTestsEmail) && password.Equals(AppConfiguration.UnitTestsPassword);
        }

        public Task<bool> LoginAsync(string email, string password)
        {
            throw new NotImplementedException();
        }
    }
}
