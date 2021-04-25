using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows.Input;
using OidcSample.Services;
using Xamarin.Forms;

namespace OidcSample.ViewModels
{
    public class MainViewModel
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private const string AuthorityUrl = "https://xamarinoidc-app.azurewebsites.net";
        
        public MainViewModel()
        {
            ExecuteLogin = new Command(Login);
            ExecuteGetInfo = new Command(GetInfo);
        }

        private async void GetInfo()
        {
            var url = Path.Combine(AuthorityUrl, "manage", "index");
            var theSecret = await _httpClient.GetAsync(url);
            Debug.WriteLine(theSecret);
        }

        private async void Login()
        {
            var oidcIdentityService = new OidcIdentityService("gnabbermobileclient", App.CallbackScheme, "profile offline", AuthorityUrl);
            var credentials = await oidcIdentityService.Authenticate();
            
            _httpClient.DefaultRequestHeaders.Authorization = credentials.IsError
                ? null
                : new AuthenticationHeaderValue("bearer", credentials.AccessToken);
        }

        public ICommand ExecuteLogin { get; }
        public ICommand ExecuteGetInfo { get; }
    }
}