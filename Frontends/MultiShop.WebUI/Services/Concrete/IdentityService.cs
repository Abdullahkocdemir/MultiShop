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
        private readonly ServiceApiSettings _serviceApiSettings;

        public IdentityService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor, IOptions<ClientSettings> clientSettings, IOptions<ServiceApiSettings> serviceApiSettings)
        {
            _httpClient = httpClient;
            this.httpContextAccessor = httpContextAccessor;
            _clientSettings = clientSettings.Value;
            _serviceApiSettings = serviceApiSettings.Value;
        }

        public async Task<bool> GetRefreshTokenAsync()
        {
            var discoveryEndpoint = await _httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
            {

                Address = _serviceApiSettings.IdentityServerUrl,
                Policy = new DiscoveryPolicy
                {
                    RequireHttps = false
                }
            });


            var refreshtokenResponse = await httpContextAccessor.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.RefreshToken);


            RefreshTokenRequest refreshTokenRequest = new RefreshTokenRequest
            {
                ClientId = _clientSettings.MultiShopManagerClient.ClientId,
                ClientSecret = _clientSettings.MultiShopManagerClient.ClientSecret,
                RefreshToken = refreshtokenResponse,
                Address = discoveryEndpoint.TokenEndpoint
            };
            var tokenResponse = await _httpClient.RequestRefreshTokenAsync(refreshTokenRequest);
            var authenticationTokens =  new List<AuthenticationToken>
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
            };
            var result = await httpContextAccessor.HttpContext.AuthenticateAsync();
            var properties = result.Properties; 
            properties.StoreTokens(authenticationTokens);
            await httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, result.Principal, properties);
            return tokenResponse.IsError ? false : true;
        }

        public async Task<bool> SignInAsync(SignInDTO signUpDTO)
        {
            var discoveryEndpoint = await _httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
            {

                Address = _serviceApiSettings.IdentityServerUrl,
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
