using Domain.Configuration;

namespace Domain.Repositories
{
    public interface IRepository
    {
        ApplicationDbContext GetContext();
    }
}
