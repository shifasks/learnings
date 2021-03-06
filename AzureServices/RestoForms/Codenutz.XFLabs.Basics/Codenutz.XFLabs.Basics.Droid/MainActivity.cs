﻿using Android.App;
using Android.Content.PM;
using Android.Graphics.Drawables;
using Android.OS;
using Autofac;
using Codenutz.XFLabs.Basics.ViewModel;
using FFImageLoading.Forms.Droid;
using Plugin.Toasts;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XLabs.Forms;
using XLabs.Ioc;
using XLabs.Ioc.Autofac;
using XLabs.Platform.Device;

namespace Codenutz.XFLabs.Basics.Droid
{
	[Activity(Label = "Reserve Table", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : XFormsApplicationDroid
    {
		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			global::Xamarin.Forms.Forms.Init(this, bundle);
            global::Xamarin.FormsMaps.Init(this, bundle);
            CachedImageRenderer.Init();

            if (!Resolver.IsSet) SetIoc();

			DependencyService.Register<ToastNotificatorImplementation>();
			ToastNotificatorImplementation.Init(this);
			LoadApplication(new App());

            ActionBar.SetIcon(new ColorDrawable(Resources.GetColor(Android.Resource.Color.Transparent)));
        }

		private void SetIoc()
		{
			var containerBuilder = new Autofac.ContainerBuilder();

			containerBuilder.Register(c => AndroidDevice.CurrentDevice).As<IDevice>();
			//containerBuilder.RegisterType<MainViewModel>().AsSelf();

			containerBuilder.RegisterType<HomeViewModel>().AsSelf();
			Resolver.SetResolver(new AutofacResolver(containerBuilder.Build()));

		}
	}
}

