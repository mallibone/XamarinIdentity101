using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OidcSample.OidcAzure
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OidcAzureAuthView : ContentView
    {
        public OidcAzureAuthView()
        {
            InitializeComponent();
        }
    }
}