using System;
using System.Threading.Tasks;
using IdentityModel.OidcClient;
using IdentityModel.OidcClient.Results;

namespace OidcSample.Services
{
    public class OidcIdentityService
    {
        private readonly string _authorityUrl;
        private readonly string _clientId;
        private readonly string _redirectUrl;
        private readonly string _postLogoutRedirectUrl;
        private readonly string _scope;
        private readonly string? _clientSecret;

        public OidcIdentityService(string clientId, string redirectUrl, string postLogoutRedirectUrl, string scope, string authorityUrl, string? clientSecret = null)
        {
            _authorityUrl = authorityUrl;
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

        public async Task<LogoutResult> Logout(string? identityToken)
        {
                OidcClient oidcClient = CreateOidcClient();
                LogoutResult logoutResult = await oidcClient.LogoutAsync(new LogoutRequest{IdTokenHint = identityToken});
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