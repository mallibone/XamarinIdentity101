using IdentityModel.OidcClient;
using IdentityModel.OidcClient.Results;

namespace OidcSample.Services
{
    public static class OidcClientExtensions
    {
        public static Credentials ToCredentials(this LoginResult loginResult)
            => new Credentials
            {
                AccessToken = loginResult.AccessToken,
                IdentityToken = loginResult.IdentityToken,
                RefreshToken = loginResult.RefreshToken,
                AccessTokenExpiration = loginResult.AccessTokenExpiration
            };
        
        public static Credentials ToCredentials(this RefreshTokenResult refreshTokenResult)
            => new Credentials
            {
                AccessToken = refreshTokenResult.AccessToken,
                IdentityToken = refreshTokenResult.IdentityToken,
                RefreshToken = refreshTokenResult.RefreshToken,
                AccessTokenExpiration = refreshTokenResult.AccessTokenExpiration
            };
    }
}