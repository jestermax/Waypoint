using System.Threading.Tasks;

namespace Domain.Authentication
{
    public class AzureSqlAuthenticationService : IAuthenticationService
    {
        public bool Login(string email, string password)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> LoginAsync(string email, string password)
        {
            throw new System.NotImplementedException();
        }
    }
}
