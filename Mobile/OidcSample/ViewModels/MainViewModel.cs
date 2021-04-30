using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using IdentityModel.OidcClient;
using OidcSample.Annotations;
using OidcSample.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace OidcSample.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private const string AuthorityUrl = "https://xamarinoidc-app.azurewebsites.net";
        private LoginResult? _loginResult;
        private readonly OidcIdentityService oidcIdentityService;

        public MainViewModel()
        {
            oidcIdentityService = new OidcIdentityService("gnabbermobileclient", App.CallbackScheme, "openid profile offline_access", AuthorityUrl);
            ExecuteLogin = new Command(Login);
            ExecuteLogout = new Command(async () =>
            {
                await oidcIdentityService.Logout();
                _loginResult = null;
                OnPropertyChanged(nameof(TokenExpirationText));
                OnPropertyChanged(nameof(AccessTokenText));
                OnPropertyChanged(nameof(IdTokenText));
                OnPropertyChanged(nameof(IsLoggedIn));
                OnPropertyChanged(nameof(IsNotLoggedIn));
            });
            ExecuteGetInfo = new Command(GetInfo);
            ExecuteCopyAccessToken = new Command(async () => await Clipboard.SetTextAsync(_loginResult?.AccessToken));
            ExecuteCopyIdentityToken = new Command(async () => await Clipboard.SetTextAsync(_loginResult?.IdentityToken));
        }

        private async void GetInfo()
        {
            var url = Path.Combine(AuthorityUrl, "manage", "index");
            var theSecret = await _httpClient.GetAsync(url);
            Debug.WriteLine(theSecret);
        }

        private async void Login()
        {
            LoginResult credentials = await oidcIdentityService.Authenticate();
            _loginResult = credentials;
            OnPropertyChanged(nameof(TokenExpirationText));
            OnPropertyChanged(nameof(AccessTokenText));
            OnPropertyChanged(nameof(IdTokenText));
            OnPropertyChanged(nameof(IsLoggedIn));
            OnPropertyChanged(nameof(IsNotLoggedIn));

            _httpClient.DefaultRequestHeaders.Authorization = credentials.IsError
                ? null
                : new AuthenticationHeaderValue("bearer", credentials.AccessToken);
        }

        public ICommand ExecuteLogin { get; }
        public ICommand ExecuteLogout { get; }
        public ICommand ExecuteGetInfo { get; }
        public ICommand ExecuteCopyAccessToken { get; }
        public ICommand ExecuteCopyIdentityToken { get; }

        public string TokenExpirationText => "Access Token expires at: " + _loginResult?.AccessTokenExpiration;
        public string AccessTokenText => "Access Token: " + _loginResult?.AccessToken;
        public string IdTokenText => "Id Token: " + _loginResult?.IdentityToken;
        public bool IsLoggedIn => _loginResult != null;
        public bool IsNotLoggedIn => _loginResult == null;

        public event PropertyChangedEventHandler? PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}