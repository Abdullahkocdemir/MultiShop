using Duende.AccessTokenManagement;
using IdentityModel.Client;
using Microsoft.Extensions.Options;
using MultiShop.WebUI.Services.Interfaces;
using MultiShop.WebUI.Settings;

namespace MultiShop.WebUI.Services.Concrete
{
    public class ClientCredentialTokenService : IClientCredentialTokenService
    {
        private readonly ServiceApiSettings _serviceApiSettings;
        private readonly HttpClient _httpClient;
        private readonly IClientCredentialsTokenCache _clientAccessTokenCache;
        private readonly ClientSettings _clientSettings;

        public ClientCredentialTokenService(
            IOptions<ServiceApiSettings> serviceApiSettings,
            HttpClient httpClient,
            IClientCredentialsTokenCache clientAccessTokenCache,
            IOptions<ClientSettings> clientSettings)
        {
            _serviceApiSettings = serviceApiSettings.Value;
            _httpClient = httpClient;
            _clientAccessTokenCache = clientAccessTokenCache;
            _clientSettings = clientSettings.Value;
        }

        public async Task<string> GetTokenAsync()
        {
            // Hata 1 Çözümü: GetAsync metoduna ikinci parametre olarak 'null' (TokenRequestParameters) ekliyoruz.
            var token = await _clientAccessTokenCache.GetAsync("multishoptoken", null);

            if (token != null)
            {
                return token.AccessToken;
            }

            var discoveryEndPoint = await _httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
            {
                Address = _serviceApiSettings.IdentityServerUrl,
                Policy = new DiscoveryPolicy
                {
                    RequireHttps = false
                }
            });

            if (discoveryEndPoint.IsError)
            {
                throw new Exception("Discovery document alınamadı.");
            }

            var clientCredentialTokenRequest = new ClientCredentialsTokenRequest
            {
                ClientId = _clientSettings.MultiShopVisitorClient.ClientId,
                ClientSecret = _clientSettings.MultiShopVisitorClient.ClientSecret,
                Address = discoveryEndPoint.TokenEndpoint
            };

            var tokenResponse = await _httpClient.RequestClientCredentialsTokenAsync(clientCredentialTokenRequest);

            if (tokenResponse.IsError)
            {
                throw new Exception("Token alınırken hata oluştu.");
            }

            // Hata 2 Çözümü: 'AccessToken' yerine 'ClientCredentialsToken' kullanıyoruz.
            // Ayrıca SetAsync metoduna üçüncü parametre olarak 'null' ekliyoruz.
            await _clientAccessTokenCache.SetAsync("multishoptoken", new ClientCredentialsToken
            {
                AccessToken = tokenResponse.AccessToken,
                Expiration = DateTime.Now.AddSeconds(tokenResponse.ExpiresIn)
            }, null);

            return tokenResponse.AccessToken;
        }
    }
}