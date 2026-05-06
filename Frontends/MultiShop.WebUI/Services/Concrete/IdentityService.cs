using Duende.IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using MultiShop.DTOLayer.IdentityDTOs.LoginDTO;
using MultiShop.WebUI.Services.Interfaces;
using MultiShop.WebUI.Settings;
using System.Security.Claims;

namespace MultiShop.WebUI.Services.Concrete
{
    public class IdentityService : IIdentityService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ClientSettings _clientSettings;

        public IdentityService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor, IOptions<ClientSettings> clientSettings)
        {
            _httpClient = httpClient;
            this.httpContextAccessor = httpContextAccessor;
            _clientSettings = clientSettings.Value;
        }

        public async Task<bool> SignInAsync(SignInDTO signUpDTO)
        {
            var discoveryEndpoint = await _httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
            {

                Address = "http://localhost:5001",
                Policy = new DiscoveryPolicy
                {
                    RequireHttps = false
                }
            });

            var passwordTokenRequest = new PasswordTokenRequest
            {
                ClientId = _clientSettings.MultiShopManagerClient.ClientId,
                ClientSecret = _clientSettings.MultiShopManagerClient.ClientSecret,
                UserName = signUpDTO.UserName,
                Password = signUpDTO.Password,
                Address = discoveryEndpoint.TokenEndpoint
            };

            var tokenResponse = await _httpClient.RequestPasswordTokenAsync(passwordTokenRequest);


            var userinfoRequest = new UserInfoRequest
            {
                Address = discoveryEndpoint.UserInfoEndpoint,
                Token = tokenResponse.AccessToken
            };
            var userInfoResponse = await _httpClient.GetUserInfoAsync(userinfoRequest);

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(userInfoResponse.Claims, CookieAuthenticationDefaults.AuthenticationScheme, "name", "role");

            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            var auothProperties = new AuthenticationProperties();

            auothProperties.StoreTokens(new List<AuthenticationToken>
            {
                new AuthenticationToken
                {
                    Name=OpenIdConnectParameterNames.AccessToken,
                    Value=tokenResponse.AccessToken
                },
                new AuthenticationToken
                {
                    Name=OpenIdConnectParameterNames.RefreshToken,
                    Value=tokenResponse.RefreshToken
                },
                new AuthenticationToken
                {
                    Name=OpenIdConnectParameterNames.ExpiresIn,
                    Value=DateTime.Now.AddSeconds(tokenResponse.ExpiresIn).ToString()
                }

            });
            auothProperties.IsPersistent = false;
            await httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, auothProperties);
            return tokenResponse.IsError ? false : true;
        }
    }
}
