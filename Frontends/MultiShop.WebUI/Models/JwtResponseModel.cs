namespace MultiShop.WebUI.Models
{
    public class JwtResponseModel
    {
        public string Token { get; set; } = string.Empty;
        public DateTime ExpireDate { get; set; }
    }
}
