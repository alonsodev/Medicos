using System;
using System.Collections.Generic;
using System.Text;


namespace Medicos.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using System.Windows.Input;
    using Xamarin.Forms;
    using Views;
    using Medicos.Services;

    public class LoginViewModel : BaseViewModel
    {

        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        private string usuario;
        private string password;
        private bool isRunning;
        private bool isEnabled;
        #endregion

        #region Properties
        public string Usuario
        {
            get { return this.usuario; }
            set { SetValue(ref this.usuario, value); }
        }

        public string Password
        {
            get { return this.password; }
            set { SetValue(ref this.password, value); }
        }

        public bool IsRunning
        {
            get { return this.isRunning; }
            set { SetValue(ref this.isRunning, value); }
        }

        public bool IsRemembered
        {
            get;
            set;
        }

        public bool IsEnabled
        {
            get { return this.isEnabled; }
            set { SetValue(ref this.isEnabled, value); }
        }
        #endregion

        #region Constructors
        public LoginViewModel()
        {
            this.apiService = new ApiService();

            this.IsRemembered = true;
            this.IsEnabled = true;

            this.Usuario = "gerencia@raycomsoluciones.com";
            this.Password = "Adm1n15tr4d0r*";
            /*this.Usuario = "lopez.leon@test.com.co";
            this.Password = "Axa*20182018";*/
        }
        #endregion

        #region Commands
        public ICommand LoginCommand
        {
            get
            {
                return new RelayCommand(Login);
            }
        }

        public ICommand RegistreseCommand
        {
            get
            {
                return new RelayCommand(Registrese);
            }
        }

        private async void Registrese()
        {
            var vMainViewModel = MainViewModel.GetInstance();
            vMainViewModel.Registrar = new RegistrarViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new RegistrarPage());
        }

        private async void Login()
        {
            if (string.IsNullOrEmpty(this.Usuario))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Tú debes ingresar un usuario.",
                    "Aceptar");
                return;
            }

            if (string.IsNullOrEmpty(this.Password))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Tú debes ingresar una contraseña.",
                    "Aceptar");
                return;
            }

            this.IsRunning = true;
            this.IsEnabled = false;

            var connection = await this.apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    connection.Message,
                    "Aceptar");
                return;
            }

            var token = await this.apiService.GetToken(
                this.Usuario,
                this.Password);

            if (token == null)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Algo estuvo mal, por favor intente después.",
                    "Aceptar");
                return;
            }

            if (string.IsNullOrEmpty(token.AccessToken) && !string.IsNullOrEmpty(token.ErrorDescription))
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    token.ErrorDescription,
                    "Aceptar");
                this.Password = string.Empty;
                return;
            }

            if (string.IsNullOrEmpty(token.AccessToken)){
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                     "Error",
                    "Algo estuvo mal, por favor intente después.",
                    "Aceptar");
                this.Password = string.Empty;
                return;
            }

            var vMainViewModel = MainViewModel.GetInstance();
            vMainViewModel.Token = token;
            vMainViewModel.MainMenu = new MainMenuViewModel();
            vMainViewModel.Usuario = this.Usuario;

            await Application.Current.MainPage.Navigation.PushAsync(new MainMenuPage());

            this.IsRunning = false;
            this.IsEnabled = true;

            this.Usuario = string.Empty;
            this.Password = string.Empty;
        }

        #endregion


    }
}
