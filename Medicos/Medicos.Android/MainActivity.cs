using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Plugin.Permissions;

namespace Medicos.Droid
{
    [Activity(Label = "Medicos", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);
            //Xamarin.Forms.DependencyService.Register<SaveAndroid>();
            global::Xamarin.Forms.Forms.Init(this, bundle);
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("ODczNUAzMTM2MmUzMjJlMzBXZWFHeXpGQ2NpTVp2UGI1L0RlRlVMRlU0L283MkRPS2Vxc1JpR1RtM3RVPQ==");
            LoadApplication(new Medicos.App());
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}

