using System;
using System.Web;

namespace Domain.Configuration
{
    public class HttpContextFactory
    {
        private static HttpContextBase _context;

        public static HttpContextBase Current
        {
            get
            {
                if (_context != null)
                {
                    return _context;
                }

                if (HttpContext.Current == null)
                {
                    throw new InvalidOperationException("HttpContext is unavailable");
                }

                return new HttpContextWrapper(HttpContext.Current);
            }
        }

        public static void SetCurrentContext(HttpContextBase httpContext)
        {
            _context = httpContext;
        }
    }
}
