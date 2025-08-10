namespace MultiShop.Basket.LoginServices
{
    // Kullanıcı giriş bilgilerini (özellikle UserId) almak için servis
    public class LoginService : ILoginService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        // HTTP istek bağlamına (HttpContext) erişmek için kullanılır.
        // Middleware dışında veya Controller dışındaki yerlerden oturum bilgisine ulaşmamızı sağlar.

        // Constructor: IHttpContextAccessor, Dependency Injection (DI) ile enjekte edilir
        public LoginService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        // Kullanıcı ID'sini döndüren property
        // "sub" → JWT token içinde "subject" alanı, genellikle kullanıcı kimliği tutulur
        // Eğer kullanıcı giriş yapmamışsa boş string döner
        public string GetUserId =>
            _httpContextAccessor.HttpContext?     // Geçerli HTTP isteği var mı?
            .User?                                // Kullanıcı bilgisi var mı?
            .FindFirst("sub")?                    // JWT token içinde "sub" claim'i var mı?
            .Value                                // Varsa değerini al
            ?? string.Empty;                      // Yoksa boş string döndür
    }
}
