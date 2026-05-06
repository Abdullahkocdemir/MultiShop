namespace MultiShop.WebUI.Settings
{
    public class ClientSettings
    {
        public Client MultiShopVisitorClient { get; set; }
        public Client MultiShopManagerClient { get; set; }
        public Client MultiShopAdminClient { get; set; }
    }

    public class Client
    {
        public string ClientId { get; set; } = string.Empty;
        public string ClientSecret { get; set; } = string.Empty;
    }
}
