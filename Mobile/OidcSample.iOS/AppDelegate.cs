using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;

namespace OidcSample.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
#if ENABLE_TEST_CLOUD
            Xamarin.Calabash.Start();
#endif
            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
        
        public override bool OpenUrl(UIApplication app, NSUrl url, NSDictionary options) =>
            Xamarin.Essentials.Platform.OpenUrl(app, url, options) 
            || base.OpenUrl(app, url, options);

        public override bool ContinueUserActivity(UIApplication application, NSUserActivity userActivity, UIApplicationRestorationHandler completionHandler) =>
            Xamarin.Essentials.Platform.ContinueUserActivity(application, userActivity, completionHandler) 
            || base.ContinueUserActivity(application, userActivity, completionHandler);
    }
}
