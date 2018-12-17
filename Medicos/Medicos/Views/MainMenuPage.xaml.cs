using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Medicos.Views
{
    using Medicos.ViewModels;
    using Plugin.Permissions;
    using Plugin.Permissions.Abstractions;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;
    public partial class MainMenuPage : ContentPage
	{
		public MainMenuPage ()
		{
			InitializeComponent ();
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var vMainViewModel = MainViewModel.GetInstance();
            vMainViewModel.checkExistsSession();

            checkPermission();
        }

        private async void checkPermission()
        {
            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera);
                if (status != PermissionStatus.Granted)
                {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Camera))
                    {
                        await Application.Current.MainPage.DisplayAlert("Cámara requerida", "La cámara es requerida", "OK");
                    }

                    var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Camera);
                    //Best practice to always check that the key exists
                    if (results.ContainsKey(Permission.Camera))
                        status = results[Permission.Location];
                }

                /*if (status == PermissionStatus.Granted)
                {
                    await Task.Delay(3000);
                    var locator = CrossGeolocator.Current;
                    if (!locator.IsGeolocationAvailable || !locator.IsGeolocationEnabled)
                    {
                        await Application.Current.MainPage.DisplayAlert(
                            "Error",
                            "Por favor habilitar el GPS del dispositivo para el correcto funcionamiento de la aplicación.",
                            "Aceptar");

                        IGpsPermission oGpsPermission = DependencyService.Get<IGpsPermission>();
                        oGpsPermission.GetGps();
                    }
                }else if (status != PermissionStatus.Unknown)
                {
                    await DisplayAlert("Localización denegada", "No se puede continuar, intentar nuevamente.", "OK");
                    
                }*/
            }
            catch (Exception ex)
            {

            }


            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);
                if (status != PermissionStatus.Granted)
                {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Storage))
                    {
                        await Application.Current.MainPage.DisplayAlert("Almacenamiento externo requerido", "El almacenamiento externo es requerido", "OK");
                    }

                    var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Storage);
                    //Best practice to always check that the key exists
                    if (results.ContainsKey(Permission.Storage))
                        status = results[Permission.Storage];
                }

                /*if (status == PermissionStatus.Granted)
                {
                    await Task.Delay(3000);
                    var locator = CrossGeolocator.Current;
                    if (!locator.IsGeolocationAvailable || !locator.IsGeolocationEnabled)
                    {
                        await Application.Current.MainPage.DisplayAlert(
                            "Error",
                            "Por favor habilitar el GPS del dispositivo para el correcto funcionamiento de la aplicación.",
                            "Aceptar");

                        IGpsPermission oGpsPermission = DependencyService.Get<IGpsPermission>();
                        oGpsPermission.GetGps();
                    }
                }else if (status != PermissionStatus.Unknown)
                {
                    await DisplayAlert("Localización denegada", "No se puede continuar, intentar nuevamente.", "OK");
                    
                }*/
            }
            catch (Exception ex)
            {

            }
        }
    }
}