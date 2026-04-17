using System.Security.Claims;

namespace MultiShop.Basket.LoginService
{
    public class LoginService : ILoginService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoginService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        // Daha güvenli: Zincirleme null kontrolü yapıyoruz.
        // ClaimsTypes.NameIdentifier genelde "sub" claim'ine karşılık gelir.
        public string GetUserId => _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value
                                   ?? _httpContextAccessor.HttpContext?.User?.FindFirst("sub")?.Value!;
    }
}