using System;
using System.Collections.Generic;
using System.Text;


namespace Medicos.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using System.Windows.Input;
    using Xamarin.Forms;
    using Views;
    public class LoginViewModel
    {
        #region Constructors
        public LoginViewModel()
        {
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

        private async void Login()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new MainMenuPage());
        }

        public ICommand ReportarPacienteCommand
        {
            get
            {
                return new RelayCommand(ReportarPaciente);
            }
        }

        private async void ReportarPaciente()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new ReportarPacientePage());
        }
        #endregion


    }
}
