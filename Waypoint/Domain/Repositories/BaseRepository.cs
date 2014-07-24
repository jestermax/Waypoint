using Domain.Configuration;

namespace Domain.Repositories
{
    public abstract class BaseRepository
    {
        public ApplicationDbContext Context;

        protected BaseRepository(ApplicationDbContext context)
        {
            Context = context;
        }

        public ApplicationDbContext GetContext()
        {
            return Context;
        }
    }
}
