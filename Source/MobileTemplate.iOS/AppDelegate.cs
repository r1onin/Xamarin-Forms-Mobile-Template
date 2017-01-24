﻿using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using Foundation;
using HockeyApp.iOS;
using MobileTemplate.Core;
using UIKit;

namespace MobileTemplate.iOS
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
            RegisterHockeyApp();
            BuildIoCContainer();

            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new App());

            RegisterTheme();

            return base.FinishedLaunching(app, options);
        }

        private void BuildIoCContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterCoreDependencies();
            builder.RegisteriOSDependencies();
            builder.Publish();
        }

        private void RegisterHockeyApp()
        {
            if (MagicStringsiOS.HockeyAppId?.Length == 32)
            {
                var manager = BITHockeyManager.SharedHockeyManager;
                manager.Configure(MagicStringsiOS.HockeyAppId);
                manager.DisableFeedbackManager = true;
                manager.DisableMetricsManager = true;
                manager.StartManager();
            }
        }

        private void RegisterTheme()
        {
            // Sometimes builders may not register certain libraries if they're only referenced in XAML.
            // To counteract this, we must add a code reference to them so the assemblies will be included.
            var lightThemeResources = typeof(Xamarin.Forms.Themes.LightThemeResources);
            var underlineEffect = typeof(Xamarin.Forms.Themes.iOS.UnderlineEffect);
        }
    }
}
