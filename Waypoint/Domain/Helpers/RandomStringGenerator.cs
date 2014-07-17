using System;
using System.Text;

namespace Domain.Helpers
{
    public static class RandomStringGenerator
    {
        public static string Create(int length)
        {
            var random = new Random(DateTime.UtcNow.Millisecond);
            var builder = new StringBuilder();

            for (var i = 0; i < length; i++)
            {
                builder.Append(Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65))));
            }

            return builder.ToString();
        }
    }
}
