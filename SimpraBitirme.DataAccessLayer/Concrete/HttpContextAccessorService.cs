using Microsoft.AspNetCore.Http;
using SimpraBitirme.DataAccessLayer.Abstract;
using System.Security.Claims;

namespace SimpraBitirme.DataAccessLayer.Concrete
{
    public class HttpContextAccessorService : IHttpContextAccessorService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpContextAccessorService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int GetUserId()
        {
            var result = string.Empty;
            if (_httpContextAccessor.HttpContext != null)
            {
                var userNameClaim = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name);
                if (userNameClaim != null)
                {
                    var userName = userNameClaim.Value;
                    return int.Parse(userName);
                }
                return 0;
            }
            else
                return 0;
        }
    }
}
