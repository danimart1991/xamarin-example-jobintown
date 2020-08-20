using System;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using FFImageLoading.Forms.Droid;
using Plugin.Permissions;

namespace JobInTown.Droid
{
    [Activity(Label = "JobInTown", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            AppDomain.CurrentDomain.UnhandledException += OnCurrentDomainUnhandledException;
            AndroidEnvironment.UnhandledExceptionRaiser += OnAndroidEnvironmentUnhandledExceptionRaiser;
            TaskScheduler.UnobservedTaskException += OnTaskSchedulerUnobservedTaskException;

            global::Xamarin.Forms.Forms.Init(this, bundle);

            Xamarin.FormsMaps.Init(this, bundle);
            UserDialogs.Init(() => this);
            CachedImageRenderer.Init();

            LoadApplication(new App(new Setup()));
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            AppDomain.CurrentDomain.UnhandledException -= OnCurrentDomainUnhandledException;
            AndroidEnvironment.UnhandledExceptionRaiser -= OnAndroidEnvironmentUnhandledExceptionRaiser;
            TaskScheduler.UnobservedTaskException -= OnTaskSchedulerUnobservedTaskException;
        }

        private void OnCurrentDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(e.ExceptionObject);
        }

        private void OnAndroidEnvironmentUnhandledExceptionRaiser(object sender, RaiseThrowableEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(e.Exception);
            e.Handled = true;
        }

        private void OnTaskSchedulerUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(e.Exception);
        }
    }
}