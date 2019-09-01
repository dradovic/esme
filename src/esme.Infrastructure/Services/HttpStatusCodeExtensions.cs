using System.Net;

namespace esme.Infrastructure.Services
{
    public static class HttpStatusCodeExtensions
    {
        public static bool IsSuccess(this HttpStatusCode code)
        {
            return ((int)code >= 200) && ((int)code <= 299);
        }
    }
}
