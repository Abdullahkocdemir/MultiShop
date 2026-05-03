namespace MultiShop.IdentityServer.Tools
{
    public class JwtTokenDefaults
    {
        // Token'ın hangi URL'ler için geçerli olduğu (Alıcı)
        public const string ValidAudience = "https://localhost:5001";

        // Token'ı yayınlayan sunucu URL'si (Yayıncı)
        public const string ValidIssuer = "https://localhost:5001";

        // EN UZUN VE EN GÜVENLİ KEY (512-bit / 64 Karakter)
        // Bu anahtar şifreleme algoritmasının (HmacSha256/512) tam kapasite çalışmasını sağlar.
        public const string Key = "MultiShop_Project_Security_Key_2026_01020304050607080910_Jwt_Auth_System_Ultra_Secret_Key_64_Char";

        // Token ömrü (Dakika cinsinden)
        public const int ExpireDate = 60;
    }
}