using System.Linq;

using Domain.Configuration;
using Domain.Repositories;

namespace Domain.Helpers
{
    public class ApiTokenGenerator
    {
        public static string Create(ApplicationDbContext context)
        {
            var apiTokenRepository = new ApiTokenRepository(context);

            bool seekingCandidate;
            var candidate = new string[1];

            do
            {
                candidate[0] = RandomStringGenerator.Create(AppConfiguration.ApiTokenLength);
                var apiTokens = apiTokenRepository.Where(a => a.Token.Equals(candidate[0]));

                seekingCandidate = apiTokens.Any();

            } while (seekingCandidate);

            return candidate[0];
        }
    }
}
