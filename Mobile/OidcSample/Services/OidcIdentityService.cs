using System;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using IdentityModel.OidcClient;
using IdentityModel.OidcClient.Results;

namespace OidcSample.Services
{
    public class OidcIdentityService
    {
        private readonly string _authorityUrl;
        private readonly HttpClient _httpClient;
        private readonly string _clientId;
        private readonly string _redirectUrl;
        private readonly string _postLogoutRedirectUrl;
        private readonly string _scope;
        private readonly string? _clientSecret;

        public OidcIdentityService(string clientId, string redirectUrl, string postLogoutRedirectUrl, string scope, string authorityUrl, HttpClient httpClient, string? clientSecret = null)
        {
            _authorityUrl = authorityUrl;
            _httpClient = httpClient;
            _clientId = clientId;
            _redirectUrl = redirectUrl;
            _postLogoutRedirectUrl = postLogoutRedirectUrl;
            _scope = scope;
            _clientSecret = clientSecret;
        }
        
        public async Task<Credentials> Authenticate()
        {
            try
            {
                OidcClient oidcClient = CreateOidcClient();
                LoginResult loginResult = await oidcClient.LoginAsync(new LoginRequest());
                return loginResult.ToCredentials();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new Credentials {Error = ex.ToString()};
            }
        }

        public async Task<LogoutResult> Logout(Credentials credentials)
        {
            OidcClient oidcClient = CreateOidcClient();
            LogoutResult logoutResult = await oidcClient.LogoutAsync(new LogoutRequest{IdTokenHint = credentials.IdentityToken});
            
            await _httpClient.RevokeTokenAsync(new TokenRevocationRequest
            {
                Address = _authorityUrl + "/connect/revocation",
                ClientId = _clientId,
                ClientSecret = _clientSecret,
                Token = credentials.AccessToken
            });

            return logoutResult;
        }

        public async Task<Credentials> RefreshToken(string refreshToken)
        {
            try
            {
                OidcClient oidcClient = CreateOidcClient();
                RefreshTokenResult refreshTokenResult = await oidcClient.RefreshTokenAsync(refreshToken);
                return refreshTokenResult.ToCredentials();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new Credentials {Error = ex.ToString()};
            }
        }

        private OidcClient CreateOidcClient()
        {
            var options = new OidcClientOptions
            {
                Authority = _authorityUrl,
                ClientId = _clientId,
                Scope = _scope,
                RedirectUri = _redirectUrl,
                ClientSecret = _clientSecret,
                PostLogoutRedirectUri = _postLogoutRedirectUrl,
                Browser = new WebAuthenticatorBrowser()
            };

            var oidcClient = new OidcClient(options);
            return oidcClient;
        }
    }
}