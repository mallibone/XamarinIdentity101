using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using OidcSample.Services;
using Xamarin.Forms;

namespace OidcSample
{
    public partial class MainPage : ContentPage
    {
        private readonly HttpClient _httpClient = new HttpClient();
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            var oidcIdentityService = new OidcIdentityService("gnabber", "oidcxamarin101:/authenticated", "profile offline", "");
            var credentials = await oidcIdentityService.Authenticate();
_httpClient.DefaultRequestHeaders.Authorization = credentials.IsError
    ? null
    : new AuthenticationHeaderValue("bearer", credentials.AccessToken);

            var theSecret = await _httpClient.GetAsync("https://the-endpoint-requiring-authentication.ch");
        }
    }
}
