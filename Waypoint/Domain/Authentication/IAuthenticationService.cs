using System.Threading.Tasks;

namespace Domain.Authentication
{
    public interface IAuthenticationService
    {
        bool Login(string email, string password);

        Task<bool> LoginAsync(string email, string password);
    }
}
